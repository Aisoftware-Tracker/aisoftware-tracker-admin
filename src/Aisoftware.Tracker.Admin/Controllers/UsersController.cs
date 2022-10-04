using Aisoftware.Tracker.UseCases.Users.UseCases;
using Aisoftware.Tracker.Borders.Constants;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Borders.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Admin.Controllers;
[Authorize(Roles = Roles.ADMIN)]
public class UsersController : Controller
{
    private readonly IUserUseCase _useCase;
    private readonly ILogger _logger;
    private readonly ILogUtil _logUtil;
    private RouteData _context;

    public UsersController(IUserUseCase useCase, ILogger<UsersController> logger, ILogUtil logUtil)
    {
        _useCase = useCase;
        _logger = logger;
        _logUtil = logUtil;
    }

    public async Task<ActionResult> Index()
    {
        IEnumerable<User> users = new List<User>();
        _context = this.ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            users = await _useCase.FindAll();
            _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
        }


        return View(users);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(User user)
    {
        _context = this.ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            await _useCase.Save(user);
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

    public async Task<ActionResult> Update(int id)
    {
        bool isReadOnly = Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_DEVICE_READ_ONLY));
        bool isNotMyUser = HttpContext.Session.GetInt32(SessionKey.USER_ID) != id;

        if (isReadOnly && isNotMyUser)
        {
            return Forbidden();
        }

        _context = this.ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        User response = new User();

        try
        {
            response = await _useCase.FindById(id);
            _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            return View("Error");
        }

        return View(response);
    }

    [HttpPost]
    public async Task<ActionResult> UpdateUser(User request)
    {
        _context = this.ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        try
        {
            var response = await _useCase.Update(request);
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
        _context = this.ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];
        _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
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
