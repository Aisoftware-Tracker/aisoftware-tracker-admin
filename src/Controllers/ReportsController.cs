using System;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Aisoftware.Tracker.Admin.Domain.Groups.UseCases;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;
using Aisoftware.Tracker.Admin.Domain.Reports.UseCases;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportSummaryUseCase _useCase;
        private readonly IGroupUseCase _group;
        private readonly IDeviceUseCase _device;
        private readonly ILogger _logger;

        public ReportsController(IReportSummaryUseCase useCase,
            IGroupUseCase group,
            IDeviceUseCase device,
            ILogger<GroupsController> logger)
        {
            _useCase = useCase;
            _logger = logger;
            _group = group;
            _device = device;
        }

        [HttpGet]
        public ActionResult Index(string report)
        {

            ReportViewModel viewModel = new ReportViewModel
            {
                Groups = _group.FindAll().Result,
                Devices = _device.FindAll().Result
            };

            ViewBag.Title = "Resumo";
            ViewBag.reportName = report;

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Summary(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to
        )
        {
            IEnumerable<ReportSummary> response = new List<ReportSummary>();

            string strFrom = from.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'");
            string strTo = from.ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'");

            IDictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "deviceId", deviceId.ToString() },
                { "groupId", groupId.ToString() },
                { "from", strFrom },
                { "to", strTo }
            };

            var context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = context.Values[ActionName.CONTROLLER];

            try
            {
                response = await _useCase.FindAll(queryParams);

                _logger.LogInformation($"SUCCESS: { GetType().FullName }::{ context.Values[ActionName.ACTION] }");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR: { GetType().FullName }::{ context.Values[ActionName.ACTION] }\nEXCEPTION:{ExceptionHelper.InnerException(e).Message}");
            }

            return View(response);
        }

        [HttpPost]
        public ActionResult Cancel()
        {
            return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
        }

        private ActionResult AccessDenied()
        {
            _logger.LogWarning($"TENTATIVA DE ACESSO: { GetType().FullName }\n{ HttpContext.Session.GetString(SessionKey.USER_EMAIL) }");

            return RedirectToAction(ActionName.INDEX, ControllerName.HOME);
        }
    }
}