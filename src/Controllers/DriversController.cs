using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Domain.Drivers.UseCases;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class DriversController : Controller
    {
        private readonly IDriverUseCase _useCase;

        public DriversController(IDriverUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<Driver> response = await _useCase.FindAll();

            return View(response);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateDriver(Driver request)
        {
            try
            {
                var response = await _useCase.Save(request);

                return RedirectToAction(ActionName.INDEX, RouteValues.DRIVERS);
            }
            catch (Exception e)
            {
                //TODO ver como retornar msg de erro return Json(new { status = false, message = "Erro ao tentar salvar o novo usuário" });
                return RedirectToAction(ActionName.INDEX, RouteValues.DRIVERS);

            }

        }

        [HttpPost]
        public bool Delete(int id)
        {
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
            Driver response = await _useCase.FindById(id);

            return View(response);
        }

        [HttpPost]
        public ActionResult UpdateDriver(Driver request)
        {
            _useCase.Update(request);

            return RedirectToAction(ActionName.INDEX, RouteValues.DRIVERS);
        }
    }
}