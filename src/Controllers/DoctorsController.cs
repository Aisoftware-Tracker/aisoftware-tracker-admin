using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Admin.Models;
using System.Collections.Generic;
using Aisoftware.Tracker.Admin.Domain.Doctor.UseCases;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorsUseCase _useCase;

        public DoctorsController(IDoctorsUseCase useCase){
            _useCase = useCase;
        }

        public ActionResult Index()
        {
            List<Doctors> doctors = _useCase.Index();

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
            Doctors doctor = _useCase.Find(id); 

            return View(doctor);
            //return View(db.Doctors.Where(s => s.Id == id).First());
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