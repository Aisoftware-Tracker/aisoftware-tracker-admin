using Aisoftware.Tracker.Admin.Domain.Groups.UseCases;
using Aisoftware.Tracker.Borders.Constants;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Borders.Services;
using Aisoftware.Tracker.Borders.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Admin.Controllers
{
    [Authorize(Roles = Roles.ALL)]
    public class GroupsController : Controller
    {
        private readonly IGroupUseCase _useCase;
        private readonly ILogger _logger;
        private readonly ILogUtil _logUtil;
        private RouteData _context;

        public GroupsController(IGroupUseCase useCase, ILogger<GroupsController> logger, ILogUtil logUtil)
        {
            _useCase = useCase;
            _logger = logger;
            _logUtil = logUtil;
        }

        public async Task<ActionResult> Index()
        {
            IEnumerable<Group> response = new List<Group>();

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
            GroupViewModel viewModel = new GroupViewModel
            {
                Groups = _useCase.FindAll().Result
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ADMIN)]
        public async Task<ActionResult> CreateGroup(Group request)
        {
            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

            try
            {
                await _useCase.Save(request);

                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
            }
            catch (Exception e)
            {
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
                return View("Error");
            }

            return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);

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

        public async Task<ActionResult> Update(int id)
        {
            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

            GroupViewModel viewModel = new GroupViewModel();

            try
            {
                viewModel.Group = await _useCase.FindById(id);
                viewModel.Groups = await _useCase.FindAll();

                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
            }
            catch (Exception e)
            {
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateGroup(Group request)
        {
            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

            try
            {
                Group response = await _useCase.Update(request);

                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));

            }
            catch (Exception e)
            {
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
                return View("Error");

            }

            return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
        }

        [HttpPost]
        public ActionResult Cancel()
        {
            return RedirectToAction(ActionName.INDEX, ViewBag.ControllerName);
        }

        private ActionResult Forbidden()
        {
            _context = this.ControllerContext.RouteData;
            _logger.LogWarning(_logUtil.Forbidden(GetType().FullName,
            _context.Values[ActionName.ACTION].ToString()));
            return RedirectToAction(ActionName.INDEX, ControllerName.HOME);
        }
    }
}