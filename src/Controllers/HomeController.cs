using Aisoftware.Tracker.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Positions.UseCases;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;
using Microsoft.AspNetCore.Routing;
using Aisoftware.Tracker.Admin.Common.Util;
using System;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPositionUseCase _positionUseCase;
        private readonly IDeviceUseCase _deviceUseCase;
        private readonly ILogger _logger;
        private readonly ILogUtil _logUtil;
        private RouteData _context;

        public HomeController(IPositionUseCase positionUseCase,
            IDeviceUseCase deviceUseCase,
            ILogger<HomeController> logger, ILogUtil logUtil)
        {
            _positionUseCase = positionUseCase;
            _deviceUseCase = deviceUseCase;
            _logger = logger;
            _logUtil = logUtil;
        }

        public async Task<IActionResult> Index()
        {
            _context = this.ControllerContext.RouteData;

            if (HttpContext.Session.GetString(CookieName.JSESSIONID) == null)
            {
                _logger.LogInformation(_logUtil.Info(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), "Session expired"));
                return RedirectToAction(ActionName.LOGIN, ControllerName.ACCOUNT);
            }

            DashboardViewModel dashboard = new DashboardViewModel();

            try
            {
                dashboard = new DashboardViewModel
                {
                    Devices = await _deviceUseCase.FindAll(),
                    Positions = await _positionUseCase.FindAll()
                };

                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));

                return View(dashboard);
            }
            catch (Exception e)
            {
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
                return View(dashboard);
            }
        }

    }
}