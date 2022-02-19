using System;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IDeviceUseCase _useCase;
        private readonly ILogger _logger;
        public DevicesController(IDeviceUseCase useCase, ILogger<DevicesController> logger)
        {
            _useCase = useCase;
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<Device> response = new List<Device>();

            var context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = context.Values[ActionName.CONTROLLER];

            try
            {
                response = await _useCase.FindAll();

                _logger.LogInformation($"SUCCESS: { GetType().FullName }::{ context.Values[ActionName.ACTION] }");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR: { GetType().FullName }::{ context.Values[ActionName.ACTION] }\nEXCEPTION:{ExceptionHelper.InnerException(e).Message}");
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

            var context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = context.Values[ActionName.CONTROLLER];

            try
            {
                await _useCase.Save(request);

                _logger.LogInformation($"SUCCESS: { GetType().FullName }::{ context.Values[ActionName.ACTION] }");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR: { GetType().FullName }::{ context.Values[ActionName.ACTION] }\nEXCEPTION:{ExceptionHelper.InnerException(e).Message}");
            }

            return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);

        }

        [HttpPost]
        public async Task<bool> Delete(int id)
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return false;
            }

            var context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = context.Values[ActionName.CONTROLLER];

            try
            {
                await _useCase.Delete(id);

                _logger.LogInformation($"SUCCESS: { GetType().FullName }::{ context.Values[ActionName.ACTION] }");

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR: { GetType().FullName }::{ context.Values[ActionName.ACTION] }\nEXCEPTION:{ExceptionHelper.InnerException(e).Message}");
                return false;
            }
        }

        public async Task<ActionResult> Update(int id)
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return AccessDenied();
            }

            var context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = context.Values[ActionName.CONTROLLER];

            Device response = new Device();

            try
            {
                response = await _useCase.FindById(id);

                _logger.LogInformation($"SUCCESS: { GetType().FullName }::{ context.Values[ActionName.ACTION] }");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR: { GetType().FullName }::{ context.Values[ActionName.ACTION] }\nEXCEPTION:{ExceptionHelper.InnerException(e).Message}");
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

            var context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = context.Values[ActionName.CONTROLLER];

            try
            {
                Device response = await _useCase.Update(request);
                
                _logger.LogInformation($"SUCCESS: { GetType().FullName }::{ context.Values[ActionName.ACTION] }");

            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR: { GetType().FullName }::{ context.Values[ActionName.ACTION] }\nEXCEPTION:{ExceptionHelper.InnerException(e).Message}");
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
            _logger.LogWarning($"TENTATIVA DE ACESSO: { GetType().FullName }\n{ HttpContext.Session.GetString(SessionKey.USER_EMAIL) }");

            return RedirectToAction(ActionName.INDEX, ControllerName.HOME);
        }
    }
}