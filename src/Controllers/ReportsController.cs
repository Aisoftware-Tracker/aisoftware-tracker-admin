using System;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Aisoftware.Tracker.Admin.Domain.Groups.UseCases;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases;
using Aisoftware.Tracker.Admin.Common.Util;
using Microsoft.AspNetCore.Components;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IBaseReportUseCase<ReportSummary> _summaryUseCase;
        private readonly IBaseReportUseCase<ReportRoute> _routeUseCase;
        private readonly IBaseReportUseCase<ReportEvent> _eventUseCase;
        private readonly IGroupUseCase _groupUseCase;
        private readonly IDeviceUseCase _deviceUseCase;
        private readonly ILogger _logger;
        private readonly ILogUtil _logUtil;
        private RouteData _context;
        private const string DATE_FORMAT = "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'";

        public ReportsController(
            IBaseReportUseCase<ReportSummary> summaryUseCase,
            IBaseReportUseCase<ReportRoute> routeUseCase,
            IBaseReportUseCase<ReportEvent> eventUseCase,
            IGroupUseCase group,
            IDeviceUseCase device,
            ILogger<GroupsController> logger)
        {
            _summaryUseCase = summaryUseCase;
            _routeUseCase = routeUseCase;
            _eventUseCase = eventUseCase;
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
                [FromQuery] DateTime? from,
                [FromQuery] DateTime? to
        )
        {

            if (from == null || to == null)
            {
                return Json(new { status = true, message = "Campos de data, De e Até, são obrigatórios" });
            }

            IEnumerable<ReportSummary> response = new List<ReportSummary>();

            string strFrom = from?.ToString(DATE_FORMAT);
            string strTo = to?.ToString(DATE_FORMAT);

            IDictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "from", strFrom },
                { "to", strTo }
            };

            if (deviceId != null) { queryParams.Add("deviceId", deviceId.ToString()); }
            if (groupId != null) { queryParams.Add("groupId", groupId.ToString()); }

            var context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = context.Values[ActionName.CONTROLLER];

            try
            {
                response = await _summaryUseCase.FindAll(queryParams);

                _logger.LogInformation($"SUCCESS: {GetType().FullName}::{context.Values[ActionName.ACTION]}");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR: {GetType().FullName}::{context.Values[ActionName.ACTION]}\nEXCEPTION:{ExceptionHelper.InnerException(e).Message}");
            }

            return View(response);
        }

        [HttpGet]
        public async Task<ActionResult> Events(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to
        )
        {
            IEnumerable<ReportEvent> response = new List<ReportEvent>();
            IEnumerable<Device> devices = new List<Device>();
            ReportEventViewModel viewModel = new ReportEventViewModel();

            string strFrom = from.ToString(DATE_FORMAT);
            string strTo = to.ToString(DATE_FORMAT);

            IDictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "from", strFrom },
                { "to", strTo }
            };

            if (deviceId != null) { queryParams.Add("deviceId", deviceId.ToString()); }
            if (groupId != null) { queryParams.Add("groupId", groupId.ToString()); }

            var context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = context.Values[ActionName.CONTROLLER];

            try
            {
                viewModel.Events = await _eventUseCase.FindAll(queryParams);
                viewModel.Devices = await _deviceUseCase.FindAll();

                _logger.LogInformation($"SUCCESS: {GetType().FullName}::{context.Values[ActionName.ACTION]}");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR: {GetType().FullName}::{context.Values[ActionName.ACTION]}\nEXCEPTION:{ExceptionHelper.InnerException(e).Message}");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Route(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to
        )
        {
            var response = await GetReportRoute(deviceId, groupId, from, to);
            ViewBag.deviceId = deviceId;
            ViewBag.groupId = groupId;
            ViewBag.from = from;
            ViewBag.to = to;

            return View(response);
        }

        [HttpGet]
        public IActionResult TrackRouteMap(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to
        )
        {
            return RedirectToAction(ActionName.INDEX, ControllerName.MAPS, new { deviceId, groupId, from, to });
        }

        [HttpPost]
        public ActionResult Cancel(string report)
        {
            var context = this.ControllerContext.RouteData;
            return RedirectToAction(ActionName.INDEX, $"{context.Values[ActionName.CONTROLLER]}?report={report}");
        }

        private ActionResult AccessDenied()
        {
            _logger.LogWarning($"TENTATIVA DE ACESSO: {GetType().FullName}\n{HttpContext.Session.GetString(SessionKey.USER_EMAIL)}");

            return RedirectToAction(ActionName.INDEX, ControllerName.HOME);
        }

        private async Task<IEnumerable<ReportRoute>> GetReportRoute(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to
        )
        {
            string strFrom = from.ToString(DATE_FORMAT);
            string strTo = to.ToString(DATE_FORMAT);

            IDictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "from", strFrom },
                { "to", strTo }
            };

            if (deviceId != null) { queryParams.Add("deviceId", deviceId.ToString()); }
            if (groupId != null) { queryParams.Add("groupId", groupId.ToString()); }


            var context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = context.Values[ActionName.CONTROLLER];

            IEnumerable<ReportRoute> response = new List<ReportRoute>();

            try
            {
                response = await _routeUseCase.FindAll(queryParams);

                _logger.LogInformation($"SUCCESS: {GetType().FullName}::{context.Values[ActionName.ACTION]}");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR: {GetType().FullName}::{context.Values[ActionName.ACTION]}\nEXCEPTION:{ExceptionHelper.InnerException(e).Message}");
            }

            return response;
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