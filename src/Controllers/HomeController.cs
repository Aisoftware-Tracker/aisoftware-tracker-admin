
using System.Linq;
using Aisoftware.Tracker.Admin.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class HomeController : Controller
    {
        //private MyDbContext db = new MyDbContext();
        // public IActionResult Index()
        // {
        //     DashboardViewModel dashboard = new DashboardViewModel();
            
        //     dashboard.doctors_count = 10;//db.Doctors.Count();
        //     dashboard.nurses_count = 10;//db.Nurses.Count();
        //     dashboard.patients_count = 5;//db.Patients.Count();

        //     return View(dashboard);
        // }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return Redirect("/Account/Login");
            }
            
            DashboardViewModel dashboard = new DashboardViewModel();
            
            dashboard.doctors_count = 10;//db.Doctors.Count();
            dashboard.nurses_count = 10;//db.Nurses.Count();
            dashboard.patients_count = 5;//db.Patients.Count();

            return View(dashboard);
        }
    }
}