using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Sessions.UseCases;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISessionUseCase _useCase;

        public AccountController(ISessionUseCase useCase)
        {
            _useCase = useCase;
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> Validate(Login login)
        {
            try
            {
                var response = await _useCase.Create(login);

                this.SetSessions(response);

                return Json(new { status = true, message = "Login Realizado com Sucesso!" });

            }
            catch (Exception e)
            {
                string message = e.Message == "Unauthorized" ?
                "Login ou senha inválido" :
                "Erro inesperado!\n Tente novamente em instantes";
                return Json(new { status = false, message = message });
            }

        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            _useCase.Delete();
            return RedirectToAction("Login", "Account");
        }

        private void SetSessions(Session session)
        {
            string cookieValue = _useCase.GetCookieValue();

            HttpContext.Session.SetString(CookieName.JSESSIONID, cookieValue);
            HttpContext.Session.SetString(SessionKey.USER_NAME, session.Name);
            HttpContext.Session.SetString(SessionKey.USER_EMAIL, session.Email);
            HttpContext.Session.SetString(SessionKey.USER_PHOTO, 
                string.IsNullOrEmpty(session.Photo) ? 
                "/dist/img/user-default.png" : session.Photo
            );
        }
    }

}