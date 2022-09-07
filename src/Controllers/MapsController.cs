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
using Aisoftware.Tracker.Admin.Common.Util;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class MapsController : Controller
    {
        private readonly IPositionUseCase _positionUseCase;
        private readonly IDeviceUseCase _deviceUseCase;
        private readonly IBaseReportUseCase<ReportRoute> _routeUseCase;
        private readonly ILogger _logger;
        private readonly ILogUtil _logUtil;
        private RouteData _context;

        public MapsController(IPositionUseCase positionUseCase,
            IDeviceUseCase deviceUseCase,
            IBaseReportUseCase<ReportRoute> routeUseCase,
            ILogger<HomeController> logger, ILogUtil logUtil)
        {
            _positionUseCase = positionUseCase;
            _deviceUseCase = deviceUseCase;
            _routeUseCase = routeUseCase;
            _logger = logger;
            _logUtil = logUtil;
        }

        public async Task<IActionResult> Index(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to)
        {
            if (HttpContext.Session.GetString(CookieName.JSESSIONID) == null)
            {
                return RedirectToAction(ActionName.LOGIN, ControllerName.ACCOUNT);
            }

            DashboardViewModel dashboard = new DashboardViewModel();

            if (deviceId is null)
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
            _context = this.ControllerContext.RouteData;

            string strFrom = from.ToString(FormatString.FORMAT_DATE_YYYY_MM_DD_T_HH_MM_SS_Z);
            string strTo = to.ToString(FormatString.FORMAT_DATE_YYYY_MM_DD_T_HH_MM_SS_Z);

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

                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
            }
            catch (Exception e)
            {
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
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