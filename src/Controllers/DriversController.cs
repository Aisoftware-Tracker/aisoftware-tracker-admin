using System;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Aisoftware.Tracker.Admin.Common.Util;
using Aisoftware.Tracker.Admin.Domain.Drivers.UseCases;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Microsoft.AspNetCore.Routing;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class DriversController : Controller
    {
        private readonly IDriverUseCase _useCase;
        private readonly ILogger _logger;
        private RouteData _context;

        public DriversController(IDriverUseCase useCase, ILogger<DevicesController> logger)
        {
            _useCase = useCase;
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<Driver> response = new List<Driver>();
            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

            try
            {
                response = await _useCase.FindAll();

                _logger.LogInformation(LogUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
            }
            catch (Exception e)
            {
                _logger.LogError(LogUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
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
        public async Task<ActionResult> CreateDriver(Driver request)
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return AccessDenied();
            }

            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

            try
            {
                var response = await _useCase.Save(request);

                _logger.LogInformation(LogUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));

                return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
            }
            catch (Exception e)
            {
                //TODO ver como retornar msg de erro return Json(new { status = false, message = "Erro ao tentar salvar o novo usuário" });
                _logger.LogError(LogUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));

                return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
            }

        }

        [HttpPost]
        public bool Delete(int id)
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return false;
            }

            try
            {


                //db.Driver.Where(s => s.Id == id).First();
                //db.Driver.Remove(Driver);
                //db.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<ActionResult> Update(int id)
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return AccessDenied();
            }
            Driver response = await _useCase.FindById(id);

            ViewBag.ControllerName = this.ControllerContext.RouteData.Values[ActionName.CONTROLLER];

            return View(response);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDriver(Driver request)
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return AccessDenied();
            }

            await _useCase.Update(request);

            ViewBag.ControllerName = this.ControllerContext.RouteData.Values[ActionName.CONTROLLER];

            return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
        }

        [HttpPost]
        public ActionResult Cancel()
        {
            ViewBag.ControllerName = this.ControllerContext.RouteData.Values[ActionName.CONTROLLER];

            return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
        }

        private ActionResult AccessDenied()
        {
            return RedirectToAction(ActionName.INDEX, ControllerName.HOME);
        }
    }
}