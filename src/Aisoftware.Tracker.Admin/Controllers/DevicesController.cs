using Aisoftware.Tracker.UseCases.Devices.UseCases;
using Aisoftware.Tracker.UseCases.Groups.UseCases;
using Aisoftware.Tracker.Borders.Constants;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Borders.Services;
using Aisoftware.Tracker.Borders.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Admin.Controllers;
[Authorize(Roles = Roles.ALL)]
public class DevicesController : Controller
{
    private readonly IDeviceUseCase _useCase;
    private readonly IGroupUseCase _groupUseCase;
    private readonly ILogger _logger;
    private readonly ILogUtil _logUtil;
    private RouteData _context;

    public DevicesController(IDeviceUseCase useCase, IGroupUseCase groupUseCase, ILogger<DevicesController> logger, ILogUtil logUtil)
    {
        _useCase = useCase;
        _groupUseCase = groupUseCase;
        _logger = logger;
        _logUtil = logUtil;
    }

    public async Task<ActionResult> Index()
    {
        IEnumerable<Device> response = new List<Device>();

        _context = this.ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            response = await _useCase.FindAll();

            _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
        }

        return View(response);
    }

    [Authorize(Roles = Roles.ADMIN)]
    public ActionResult Create()
    {
        DeviceViewModel viewModel = new DeviceViewModel
        {
            Groups = _groupUseCase.FindAll().Result
        };

        return View(viewModel);
    }

    [HttpPost]
    [Authorize(Roles = Roles.MASTER)]
    public async Task<ActionResult> CreateDevice(Device request)
    {
        _context = this.ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            await _useCase.Save(request);

            _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            return View("Error");

        }

        return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);

    }

    [HttpDelete]
    [Authorize(Roles = Roles.MASTER)]
    public async Task<ActionResult> Delete(int id)
    {
        _context = this.ControllerContext.RouteData;

        try
        {
            var response = await _useCase.Delete(id);
            _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
            return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
        }
        catch (Exception e)
        {
            _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            return View("Error");
        }
    }

    [Authorize(Roles = Roles.MASTER)]
    public async Task<ActionResult> Update(int id)
    {
        _context = this.ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        DeviceViewModel response = new DeviceViewModel();

        try
        {
            response.Device = await _useCase.FindById(id);
            response.Groups = await _groupUseCase.FindAll();
            response.Group = response.Groups.Where(x => x.Id == response.Device.GroupId).FirstOrDefault();

            _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
        }

        return View(response);

    }

    [HttpPost]
    [Authorize(Roles = Roles.MASTER)]
    public async Task<ActionResult> UpdateDevice(Device request)
    {
        _context = this.ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            Device response = await _useCase.Update(request);

            _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));

        }
        catch (Exception e)
        {
            _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            return View("Error");
        }

        return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
    }

    [HttpPost]
    public ActionResult Cancel()
    {
        return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
    }

    private ActionResult Forbidden()
    {
        _context = this.ControllerContext.RouteData;
        _logger.LogWarning(_logUtil.Forbidden(GetType().FullName,
        _context.Values[ActionName.ACTION].ToString()));
        return RedirectToAction(ActionName.INDEX, ControllerName.HOME);
    }
}
