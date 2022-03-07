using Aisoftware.Tracker.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Positions.UseCases;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPositionUseCase _positionUseCase;
        private readonly IDeviceUseCase _deviceUseCase;
        private readonly ILogger _logger;
        public HomeController(IPositionUseCase positionUseCase,
            IDeviceUseCase deviceUseCase,
            ILogger<HomeController> logger)
        {
            _positionUseCase = positionUseCase;
            _deviceUseCase = deviceUseCase;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString(CookieName.JSESSIONID) == null)
            {
                return Redirect($"/{ControllerName.ACCOUNT}/{ActionName.LOGIN}");
            }

            DashboardViewModel dashboard = new DashboardViewModel
            {
                doctors_count = 10,//db.Doctors.Count()
                nurses_count = 10,//db.Nurses.Count()
                patients_count = 5,//db.Patients.Count()
                Devices = await _deviceUseCase.FindAll(),
                Positions = await _positionUseCase.FindAll()
            };

            return View(dashboard);
        }
    }
}