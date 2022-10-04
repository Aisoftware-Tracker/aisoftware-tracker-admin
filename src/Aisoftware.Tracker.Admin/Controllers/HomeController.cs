using Aisoftware.Tracker.UseCases.Devices.UseCases;
using Aisoftware.Tracker.UseCases.Groups.UseCases;
using Aisoftware.Tracker.UseCases.Positions.UseCases;
using Aisoftware.Tracker.Borders.Constants;
using Aisoftware.Tracker.Borders.Services;
using Aisoftware.Tracker.Borders.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Admin.Controllers;
public class HomeController : Controller
{
    private readonly IPositionUseCase _positionUseCase;
    private readonly IDeviceUseCase _deviceUseCase;
    private readonly IGroupUseCase _groupUseCase;
    private readonly ILogger _logger;
    private readonly ILogUtil _logUtil;
    private RouteData _context;

    public HomeController(IPositionUseCase positionUseCase,
        IDeviceUseCase deviceUseCase,
        IGroupUseCase groupUseCase,
        ILogger<HomeController> logger, ILogUtil logUtil)
    {
        _positionUseCase = positionUseCase;
        _deviceUseCase = deviceUseCase;
        _groupUseCase = groupUseCase;
        _logger = logger;
        _logUtil = logUtil;
    }

    public async Task<IActionResult> Index()
    {
        _context = this.ControllerContext.RouteData;

        var token = HttpContext.Session.GetString("Token");
        if (string.IsNullOrEmpty(token))
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
                Positions = await _positionUseCase.FindAll(),
                Groups = await _groupUseCase.FindAll()
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
