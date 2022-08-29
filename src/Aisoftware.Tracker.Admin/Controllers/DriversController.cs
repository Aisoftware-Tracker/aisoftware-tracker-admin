using System;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Borders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Aisoftware.Tracker.Borders.Services;
using Aisoftware.Tracker.Admin.Domain.Drivers.UseCases;
using Aisoftware.Tracker.Borders.Constants;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;

namespace Aisoftware.Tracker.Admin.Controllers
{
    [Authorize(Roles = Roles.ALL)]
    public class DriversController : Controller
    {
        private readonly IDriverUseCase _useCase;
        private readonly ILogger _logger;
        private readonly ILogUtil _logUtil;
        private RouteData _context;

        public DriversController(IDriverUseCase useCase, ILogger<DevicesController> logger, ILogUtil logUtil)
        {
            _useCase = useCase;
            _logger = logger;
            _logUtil = logUtil;
        }

        public async Task<ActionResult> Index()
        {

            IEnumerable<Driver> response = new List<Driver>();
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
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<ActionResult> CreateDriver(Driver request)
        {
            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

            try
            {
                await _useCase.Save(request);

                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));

                return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
            }
            catch (Exception e)
            {
                //TODO ver como retornar msg de erro return Json(new { status = false, message = "Erro ao tentar salvar o novo usuário" });
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
                return View("Error");
            }

        }

        [HttpDelete]
        [Authorize(Roles = Roles.ADMIN)]
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

        [Authorize(Roles = Roles.ADMIN)]
        public async Task<ActionResult> Update(int id)
        {
            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

            Driver response = new Driver();

            try
            {
                response = await _useCase.FindById(id);
                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
            }
            catch (Exception e)
            {
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
                return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
            }

            return View(response);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<ActionResult> UpdateDriver(Driver request)
        {
            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

            try
            {
                await _useCase.Update(request);
                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
            }
            catch (Exception e)
            {
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
                return View("Error");

            }

            ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

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

    }
}