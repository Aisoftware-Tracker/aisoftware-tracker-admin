using System;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Microsoft.AspNetCore.Routing;
using Aisoftware.Tracker.Admin.Common.Util;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IDeviceUseCase _useCase;
        private readonly ILogger _logger;
        private readonly ILogUtil _logUtil;
        private RouteData _context;

        public DevicesController(IDeviceUseCase useCase, ILogger<DevicesController> logger, ILogUtil logUtil)
        {
            _useCase = useCase;
            _logger = logger;
            _logUtil = logUtil;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<Device> response = new List<Device>();

            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName =_context.Values[ActionName.CONTROLLER];

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

        public ActionResult Create()
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return AccessDenied();
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateDevice(Device request)
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return AccessDenied();
            }

            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName =_context.Values[ActionName.CONTROLLER];

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
        public async Task<ActionResult> Delete(int id)
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return AccessDenied();
            }

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
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return AccessDenied();
            }

            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName =_context.Values[ActionName.CONTROLLER];

            Device response = new Device();

            try
            {
                response = await _useCase.FindById(id);

                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
            }
            catch (Exception e)
            {
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            }

            return View(response);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDevice(Device request)
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return AccessDenied();
            }

            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName =_context.Values[ActionName.CONTROLLER];

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

        private ActionResult AccessDenied()
        {
            _context = this.ControllerContext.RouteData;
            _logger.LogWarning(_logUtil.Unauthorized(GetType().FullName, 
            _context.Values[ActionName.ACTION].ToString()));
            return RedirectToAction(ActionName.INDEX, ControllerName.HOME);
        }
    }
}