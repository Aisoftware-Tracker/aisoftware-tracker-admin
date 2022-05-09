using System;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Groups.UseCases;
using Aisoftware.Tracker.Admin.Domain.Devices.UseCases;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases;
using Aisoftware.Tracker.Admin.Common.Util;
using Microsoft.AspNetCore.Routing;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace Aisoftware.Tracker.Admin.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IBaseReportUseCase<ReportSummary> _summaryUseCase;
        private readonly IBaseReportUseCase<ReportRoute> _routeUseCase;
        private readonly IBaseReportUseCase<ReportEvent> _eventUseCase;
        private readonly IGroupUseCase _groupUseCase;
        private readonly IDeviceUseCase _deviceUseCase;
        private readonly ILogger _logger;
        private readonly ILogUtil _logUtil;
        private RouteData _context;

        public ReportsController(
            IBaseReportUseCase<ReportSummary> summaryUseCase,
            IBaseReportUseCase<ReportRoute> routeUseCase,
            IBaseReportUseCase<ReportEvent> eventUseCase,
            IGroupUseCase group,
            IDeviceUseCase device,
            ILogger<GroupsController> logger,
            ILogUtil logUtil)
        {
            _summaryUseCase = summaryUseCase;
            _routeUseCase = routeUseCase;
            _eventUseCase = eventUseCase;
            _logger = logger;
            _logUtil = logUtil;
            _groupUseCase = group;
            _deviceUseCase = device;
        }

        [HttpGet]
        public ActionResult Index(string report)
        {
            _context = this.ControllerContext.RouteData;
            ReportViewModel viewModel = new ReportViewModel();

            try
            {
                viewModel = new ReportViewModel
                {
                    Groups = _groupUseCase.FindAll().Result,
                    Devices = _deviceUseCase.FindAll().Result
                };

                ViewBag.Title = Title()[report];
                ViewBag.reportName = report;
                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));

            }
            catch (Exception e)
            {
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e));
            }

            return View(viewModel);

        }


        [HttpGet]
        public async Task<ActionResult> Summary(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to
        )
        {
            _context = this.ControllerContext.RouteData;
            IEnumerable<ReportSummary> response = new List<ReportSummary>();
            ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

            try
            {
                response = await _summaryUseCase.FindAll(GetQueryParameters(deviceId, groupId, from, to));
                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
            }
            catch (Exception e)
            {
                string message = messageParams(deviceId, groupId, from, to);
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e, message));
            }

            return View(response);
        }

        [HttpGet]
        public async Task<ActionResult> Events(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to
        )
        {
            _context = this.ControllerContext.RouteData;
            IEnumerable<ReportEvent> response = new List<ReportEvent>();
            IEnumerable<Device> devices = new List<Device>();
            ReportEventViewModel viewModel = new ReportEventViewModel();
            ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

            try
            {
                viewModel.Events = await _eventUseCase.FindAll(GetQueryParameters(deviceId, groupId, from, to));
                viewModel.Devices = await _deviceUseCase.FindAll();

                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
            }
            catch (Exception e)
            {
                string message = messageParams(deviceId, groupId, from, to);
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e, message));
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Route(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to
        )
        {
            var response = await GetReportRoute(deviceId, groupId, from, to);
            ViewBag.deviceId = deviceId;
            ViewBag.groupId = groupId;
            ViewBag.from = from;
            ViewBag.to = to;

            return View(response);
        }

        [HttpGet]
        public IActionResult TrackRouteMap(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to
        )
        {
            _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()), messageParams(deviceId, groupId, from, to));
            return RedirectToAction(ActionName.INDEX, ControllerName.MAPS, new { deviceId, groupId, from, to });
        }

        [HttpPost]
        public ActionResult Cancel(string report)
        {
            _context = this.ControllerContext.RouteData;
            return RedirectToAction(ActionName.INDEX, $"{_context.Values[ActionName.CONTROLLER]}?report={report}");
        }

        private async Task<IEnumerable<ReportRoute>> GetReportRoute(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to
        )
        {
            _context = this.ControllerContext.RouteData;
            ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

            IEnumerable<ReportRoute> response = new List<ReportRoute>();

            try
            {
                response = await _routeUseCase.FindAll(GetQueryParameters(deviceId, groupId, from, to));

                _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
            }
            catch (Exception e)
            {
                string message = messageParams(deviceId, groupId, from, to);
                _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e, message));
            }

            return response;
        }

        ///TODO Criar classe generica 
        [HttpGet]
        public async Task<IActionResult> ExportToCsv(
            [FromQuery] int? deviceId,
            [FromQuery] int? groupId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to
        )
        {
            _context = this.ControllerContext.RouteData;

            var reportRoutes = await _routeUseCase.FindAll(GetQueryParameters(deviceId, groupId, from, to));

            var devices = await _deviceUseCase.FindAll();

            var builder = new StringBuilder();
            builder.AppendLine("Id; Id Dispositivo; Protocolo; Horario do Dispositivo; Horario Corrigido; Horario do Servidor; Vencimento; Valido; Latitude; Longitude; Altitude; Velociadade; Endereco; Irregularidade; Ignicao; Status; Distancia; Distancia Total /Km; Movimentação; Horas");

            foreach (var item in reportRoutes)
            {
                string outdated = item.Outdated ? "Desatualizado" : "Atualizado";
                string valid = item.Valid ? "Sim" : "Nao";
                string ignition = item.Attributes.Ignition ? "Ligado" : "Desligado";
                string motion = item.Attributes.Motion ? "Em Movimento" : "Parado";
                string placa = devices.Where(x => x.Id == item.DeviceId).FirstOrDefault().Name;

                builder.AppendLine($"{item.Id}; {placa}; {item.Protocol}; {item.DeviceTimeStr}; {item.FixTimeStr}; {item.ServerTimeStr}; {outdated}; {valid}; {item.LatitudeStr}; {item.LongitudeStr}; {item.Altitude}; {item.Speed}; {item.Address}; {item.Accuracy}; {ignition}; {item.Attributes.Status}; {item.Attributes.Distance}; {item.Attributes.TotalDistance}; {motion}; {item.Attributes.Hours}");
            }

            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", $"ReportRoute_{DateTime.Now.ToString(FormatString.FORMAT_DATE_YYYY_MM_DD_HH_MM)}.csv");
        }

        private IDictionary<string, string> GetQueryParameters(
                [FromQuery] int? deviceId,
                [FromQuery] int? groupId,
                [FromQuery] DateTime from,
                [FromQuery] DateTime to
        )
        {
            string strFrom = from.ToString(FormatString.FORMAT_DATE_YYYY_MM_DD_T_HH_MM_SS_Z);
            string strTo = to.ToString(FormatString.FORMAT_DATE_YYYY_MM_DD_T_HH_MM_SS_Z);

            IDictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "from", strFrom },
                { "to", strTo }
            };

            if (deviceId != null) { queryParams.Add("deviceId", deviceId.ToString()); }
            if (groupId != null) { queryParams.Add("groupId", groupId.ToString()); }

            return queryParams;
        }

        private string messageParams(int? deviceId, int? groupId, DateTime? from, DateTime? to)
        {
            string message = "NAO INFORMADO";
            return $"deviceId: {deviceId?.ToString() ?? message} - groupId: {groupId?.ToString() ?? message} - from: {from?.ToString() ?? message} - to: {to?.ToString() ?? message}";
        }

        private IDictionary<string, string> Title()
        {
            return new Dictionary<string, string>
            {
                {Endpoints.SUMMARY, "Resumo"},
                {Endpoints.ROUTE, "Rota"},
                {Endpoints.EVENTS, "Eventos"},
            };
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