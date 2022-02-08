using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Domain.Users.UseCases;

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
            IEnumerable<User> users = await _useCase.FindAll();

            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            try
            {
                var response = await _useCase.Save(user);

                return RedirectToAction("Index", "Users");
            }
            catch (Exception e)
            {
               // ver como retornar msg de erro return Json(new { status = false, message = "Erro ao tentar salvar o novo usuário" });
            }

        }

        [HttpPost]
        public bool Delete(int id)
        {
            try
            {


                //db.User.Where(s => s.Id == id).First();
                //db.User.Remove(user);
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
            User user = await _useCase.FindById(id);

            return View(user);
            //return View(db.User.Where(s => s.Id == id).First());
        }

        [HttpPost]
        public ActionResult UpdateUser(User user)
        {


            return RedirectToAction("Index", "User");
        }
    }
}