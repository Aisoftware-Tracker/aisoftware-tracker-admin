using Aisoftware.Tracker.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(CookieName.JSESSIONID) == null)
            {
                return Redirect($"/{ControllerName.ACCOUNT}/{ActionName.LOGIN}");
            }

            DashboardViewModel dashboard = new DashboardViewModel
            {
                doctors_count = 10,//db.Doctors.Count()
                nurses_count = 10,//db.Nurses.Count()
                patients_count = 5//db.Patients.Count()
            };

            return View(dashboard);
        }
    }
}