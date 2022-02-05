using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Admin.Models;
using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class DoctorsController : Controller
    {
        public ActionResult Index()
        {

            Doctors dr = new Doctors
            {
                Id = 1,
                Name = "Dr. Nome",
                Email = "dr@email.com",
                Specialist = "O Cara",
                Gender = 2
            };

            var doctors = new List<Doctors>();
            doctors.Add(dr);

            return View(doctors);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDoctor(Doctors doctor)
        {
            //db.Doctors.Add(doctor);
            //db.SaveChanges();
            return RedirectToAction("Index", "Doctors");
        }

        [HttpPost]
        public bool Delete(int id)
        {
            try
            {
                Doctors doctor = new Doctors
                {
                    Id = 1,
                    Name = "Dr. Nome",
                    Email = "dr@email.com",
                    Specialist = "O Cara",
                    Gender = 2
                };

                //db.Doctors.Where(s => s.Id == id).First();
                //db.Doctors.Remove(doctor);
                //db.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public ActionResult Update(int id)
        {
            Doctors dr = new Doctors
            {
                Id = 1,
                Name = "Dr. Nome",
                Email = "dr@email.com",
                Specialist = "O Cara",
                Gender = 2
            };

            return View(dr);
        }

        [HttpPost]
        public ActionResult UpdateDoctor(Doctors doctor)
        {

            doctor = new Doctors
            {
                Id = 1,
                Name = "Dr. Nome",
                Email = "dr@email.com",
                Specialist = "O Cara",
                Gender = 2
            };

            return RedirectToAction("Index", "Doctors");
        }
    }
}