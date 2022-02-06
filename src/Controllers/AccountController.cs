using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Aisoftware.Tracker.Admin.Models;
using Microsoft.AspNetCore.Http;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class AccountController : Controller
    {
        
        public IActionResult Login()
        {
            return View();
        }

        public ActionResult Validate(Admins admin)
        {
            
            var _admin = new Admins()
            {
                Email = "teste@teste.com",
                Password = "123"
            };

            if(_admin.Email == admin.Email)
            {
                if(_admin.Password == admin.Password)
                {
                    HttpContext.Session.SetString("email", _admin.Email);
                    return Json(new { status = true, message = "Login Successfull!"});
                }
                else
                {
                    return Json(new { status = false, message = "Invalid Password!"});
                }
            }
            else
            {
                return Json(new { status = false, message = "Invalid Email!"});
            }
        }
    }

}