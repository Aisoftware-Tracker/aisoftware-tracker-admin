using System;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Users.UseCases;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserUseCase _useCase;

        public UsersController(IUserUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<ActionResult> Index()
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return AccessDenied();
            }

            IEnumerable<User> users = await _useCase.FindAll();

            ViewBag.ControllerName = this.ControllerContext.RouteData.Values[ActionName.CONTROLLER];

            return View(users);
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
        public async Task<ActionResult> CreateUser(User user)
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString(SessionKey.USER_READ_ONLY)))
            {
                return AccessDenied();
            }

            ViewBag.ControllerName = this.ControllerContext.RouteData.Values[ActionName.CONTROLLER];

            try
            {
                var response = await _useCase.Save(user);

                return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
            }
            catch (Exception e)
            {
                //TODO ver como retornar msg de erro return Json(new { status = false, message = "Erro ao tentar salvar o novo usuário" });
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
                _useCase.Delete(id);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public async Task<ActionResult> Update(int id)
        {
            if (Convert.ToInt32(HttpContext.Session.GetString(SessionKey.USER_ID)) != id )
            {
                return AccessDenied();
            }

            User response = await _useCase.FindById(id);

            ViewBag.ControllerName = this.ControllerContext.RouteData.Values[ActionName.CONTROLLER];

            return View(response);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUser(User request)
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