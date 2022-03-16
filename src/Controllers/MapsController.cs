using Aisoftware.Tracker.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Positions.UseCases;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;
using System.Collections.Generic;
using System;
using Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases;
using Aisoftware.Tracker.Admin.Domain.Reports.UseCases;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class MapsController : Controller
    {
        private readonly IPositionUseCase _positionUseCase;
        private readonly IDeviceUseCase _deviceUseCase;
        private readonly IBaseReportUseCase<ReportRoute> _routeUseCase;
        private readonly ILogger _logger;
        private const string DATE_FORMAT = "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'";
        public MapsController(IPositionUseCase positionUseCase,
            IDeviceUseCase deviceUseCase,
            IBaseReportUseCase<ReportRoute> routeUseCase,
            ILogger<HomeController> logger)
        {
            _positionUseCase = positionUseCase;
            _deviceUseCase = deviceUseCase;
            _routeUseCase = routeUseCase;
            _logger = logger;
        }

        public async Task<IActionResult> Index(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to)
        {
            if (HttpContext.Session.GetString(CookieName.JSESSIONID) == null)
            {
                return Redirect($"/{ControllerName.ACCOUNT}/{ActionName.LOGIN}");
            }

            DashboardViewModel dashboard = new DashboardViewModel();

            if (deviceId == null)
            {
                dashboard.Devices = await _deviceUseCase.FindAll();
                dashboard.Positions = await _positionUseCase.FindAll();
            }
            else
            {
                var response = await GetReportRoute(deviceId, groupId, from, to);
                dashboard.LatLong = BuildLatLong(response);
            }

            return View(dashboard);
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

                _logger.LogInformation($"SUCCESS: { GetType().FullName }::{ context.Values[ActionName.ACTION] }");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR: { GetType().FullName }::{ context.Values[ActionName.ACTION] }\nEXCEPTION:{ExceptionHelper.InnerException(e).Message}");
            }

            return response;
        }

        private string BuildLatLong(IEnumerable<ReportRoute> routes)
        {
            List<decimal[]> latLongs = new ExternalMapsTool().GetRoutes(routes);

            string positions = "[";

            foreach (var item in latLongs)
            {
                positions += $"[{item[0]},{item[1]}],";
            }

            positions += "]";

            return positions;
        }

    }
}