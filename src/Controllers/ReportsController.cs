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
using Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IBaseReportUseCase<ReportSummary> _summaryUseCase;
        private readonly IBaseReportUseCase<ReportRoute> _routeUseCase;
        private readonly IGroupUseCase _groupUseCase;
        private readonly IDeviceUseCase _deviceUseCase;
        private readonly ILogger _logger;
        private const string DATE_FORMAT = "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'";

        public ReportsController(IBaseReportUseCase<ReportSummary> useCase,
            IGroupUseCase group,
            IDeviceUseCase device,
            ILogger<GroupsController> logger)
        {
            _summaryUseCase = useCase;
            _logger = logger;
            _groupUseCase = group;
            _deviceUseCase = device;
        }

        [HttpGet]
        public ActionResult Index(string report)
        {

            ReportViewModel viewModel = new ReportViewModel
            {
                Groups = _groupUseCase.FindAll().Result,
                Devices = _deviceUseCase.FindAll().Result
            };

            ViewBag.Title = Title()[report];
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

            string strFrom = from.ToString(DATE_FORMAT);
            string strTo = to.ToString(DATE_FORMAT);

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
                response = await _summaryUseCase.FindAll(queryParams);

                _logger.LogInformation($"SUCCESS: { GetType().FullName }::{ context.Values[ActionName.ACTION] }");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR: { GetType().FullName }::{ context.Values[ActionName.ACTION] }\nEXCEPTION:{ExceptionHelper.InnerException(e).Message}");
            }

            return View(response);
        }

        [HttpGet]
        public async Task<ActionResult> Route(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to
        )
        {
            IEnumerable<ReportRoute> response = new List<ReportRoute>();

            string strFrom = from.ToString(DATE_FORMAT);
            string strTo = to.ToString(DATE_FORMAT);

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
                response = await _routeUseCase.FindAll(queryParams);

                _logger.LogInformation($"SUCCESS: { GetType().FullName }::{ context.Values[ActionName.ACTION] }");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR: { GetType().FullName }::{ context.Values[ActionName.ACTION] }\nEXCEPTION:{ExceptionHelper.InnerException(e).Message}");
            }

            return View(response);
        }

        [HttpPost]
        public ActionResult Cancel(string report)
        {
            var context = this.ControllerContext.RouteData;
            return RedirectToAction(ActionName.INDEX, $"{context.Values[ActionName.CONTROLLER]}?report={report}");
        }

        private ActionResult AccessDenied()
        {
            _logger.LogWarning($"TENTATIVA DE ACESSO: { GetType().FullName }\n{ HttpContext.Session.GetString(SessionKey.USER_EMAIL) }");

            return RedirectToAction(ActionName.INDEX, ControllerName.HOME);
        }

        private IDictionary<string, string> Title()
        {
            return new Dictionary<string, string>
            {
                {Endpoints.SUMMARY, "Resumo"},
                {Endpoints.ROUTE, "Rota"},
                {Endpoints.EVENTS, "Eventos"},
            };
        }
    }
}