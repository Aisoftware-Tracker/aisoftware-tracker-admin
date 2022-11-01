using Aisoftware.Tracker.UseCases.Base;
using Aisoftware.Tracker.UseCases.Devices.UseCases;
using Aisoftware.Tracker.UseCases.Geoferences.UseCases;
using Aisoftware.Tracker.UseCases.Groups.UseCases;
using Aisoftware.Tracker.UseCases.Maintenances.UseCases;
using Aisoftware.Tracker.UseCases.Positions.UseCases;
using Aisoftware.Tracker.Borders.Constants;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Borders.Services;
using Aisoftware.Tracker.Borders.ViewModels;
using Aisoftware.Tracker.Borders.Utils;
using Aisoftware.Tracker.Borders.Models.Date;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Admin.Controllers;
[Authorize]
public class ReportsController : Controller
{
    private readonly IBaseReportUseCase<ReportSummary> _summaryUseCase;
    private readonly IBaseReportUseCase<ReportRoute> _routeUseCase;
    private readonly IBaseReportUseCase<ReportEvent> _eventUseCase;
    private readonly IGroupUseCase _groupUseCase;
    private readonly IDeviceUseCase _deviceUseCase;
    private readonly IPositionUseCase _positionUseCase;
    private readonly IGeoferenceUseCase _geoferenceUseCase;
    private readonly IMaintenanceUseCase _maintenanceUseCase;

    private readonly ILogger _logger;
    private readonly ILogUtil _logUtil;
    private RouteData _context;

    public ReportsController(
        IBaseReportUseCase<ReportSummary> summaryUseCase,
        IBaseReportUseCase<ReportRoute> routeUseCase,
        IBaseReportUseCase<ReportEvent> eventUseCase,
        IPositionUseCase positionUseCase,
        IGeoferenceUseCase geoferenceUseCase,
        IMaintenanceUseCase maintenanceUseCase,
        IGroupUseCase group,
        IDeviceUseCase device,
        ILogger<GroupsController> logger,
        ILogUtil logUtil)
    {
        _summaryUseCase = summaryUseCase;
        _routeUseCase = routeUseCase;
        _eventUseCase = eventUseCase;
        _positionUseCase = positionUseCase;
        _geoferenceUseCase = geoferenceUseCase;
        _maintenanceUseCase = maintenanceUseCase;
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
            [FromQuery] DateTime to,
            [FromQuery] int? period
    )
    {
        GetFromTo(period, ref from, ref to);

        _context = this.ControllerContext.RouteData;
        ReportSummaryViewModel viewModel = new ReportSummaryViewModel();

        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];
        ViewBag.deviceId = deviceId;
        ViewBag.groupId = groupId;
        ViewBag.from = from;
        ViewBag.to = to;

        try
        {
            viewModel = await ReportSummaryViewModelBuild(deviceId, groupId, from, to);
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
    public async Task<ActionResult> Events(
            [FromQuery] int? deviceId,
            [FromQuery] int? groupId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            [FromQuery] int? period
    )
    {
        GetFromTo(period, ref from, ref to);

        _context = this.ControllerContext.RouteData;
        ReportEventViewModel viewModel = new ReportEventViewModel();

        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];
        ViewBag.deviceId = deviceId;
        ViewBag.groupId = groupId;
        ViewBag.from = from;
        ViewBag.to = to;

        try
        {
            viewModel = await ReportEventViewModelBuild(deviceId, groupId, from, to);

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
            [FromQuery] DateTime to,
            [FromQuery] int? period
    )
    {
        GetFromTo(period, ref from, ref to);
        
        var response = await GetReportRoute(deviceId, groupId, from, to);
        ViewBag.deviceId = deviceId;
        ViewBag.groupId = groupId;
        ViewBag.from = from;
        ViewBag.to = to;

        ViewBag.deviceId = deviceId;

        System.Console.WriteLine(from);
        System.Console.WriteLine(to);

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

    private async Task<ReportRouteViewModel> GetReportRoute(
            [FromQuery] int? deviceId,
            [FromQuery] int? groupId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to
    )
    {
        _context = this.ControllerContext.RouteData;
        ViewBag.ControllerName = _context.Values[ActionName.CONTROLLER];

        ReportRouteViewModel viewModel = new ReportRouteViewModel();

        try
        {
            viewModel = await ReportRouteViewModelBuild(deviceId, groupId, from, to);
            _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString()));
        }
        catch (Exception e)
        {
            string message = messageParams(deviceId, groupId, from, to);
            _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e, message));
        }

        return viewModel;
    }

    ///TODO Criar classe generica 
    [HttpGet]
    public async Task<IActionResult> ExportToCsv(
        [FromQuery] int? deviceId,
        [FromQuery] int? groupId,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to,
        [FromQuery] string typeReport
    )
    {
        _context = this.ControllerContext.RouteData;

        System.Console.WriteLine(".....ExportToCsv......");
        System.Console.WriteLine(from);
        System.Console.WriteLine(to);

        try
        {
            _logger.LogInformation(_logUtil.Succes(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), typeReport));

            switch (typeReport)
            {
                case Endpoints.SUMMARY:
                    _logger.LogInformation(_logUtil.Info(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), $"de: {from} - até: {to}, tipo:{typeReport}, grupo: {groupId}, device: {deviceId}" ));
                    return await ExportFileUtil.ExportToCsv(deviceId, groupId, from, to, new ReportSummaryViewModel
                    {
                        Summaries = await _summaryUseCase.FindAll(GetQueryParameters(deviceId, groupId, from, to)),
                        Devices = await _deviceUseCase.FindAll()
                    });
                case Endpoints.ROUTE:
                    _logger.LogInformation(_logUtil.Info(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), $"de: {from} - até: {to}, tipo:{typeReport}, grupo: {groupId}, device: {deviceId}" ));
                    return await ExportFileUtil.ExportToCsv(deviceId, groupId, from, to, new ReportRouteViewModel
                    {
                        Routes = await _routeUseCase.FindAll(GetQueryParameters(deviceId, groupId, from, to)),
                        Devices = await _deviceUseCase.FindAll()
                    });
                case Endpoints.EVENTS:
                    _logger.LogInformation(_logUtil.Info(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), $"de: {from} - até: {to}, tipo:{typeReport}, grupo: {groupId}, device: {deviceId}" ));
                    var eventView = await ReportEventViewModelBuild(deviceId, groupId, from, to);
                    return await ExportFileUtil.ExportToCsv(deviceId, groupId, from, to, eventView);
                default:
                    return View();
            }
        }
        catch (Exception e)
        {
            string message = messageParams(deviceId, groupId, from, to);
            _logger.LogError(_logUtil.Error(GetType().FullName, _context.Values[ActionName.ACTION].ToString(), e, message));
        }

        return null;

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

        _logger.LogInformation(_logUtil.Succes(GetType().FullName, "GetQueryParameters",
        $"de: {queryParams["from"]} até: {queryParams["to"]} - formatado"));

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

    private async Task<ReportEventViewModel> ReportEventViewModelBuild(int? deviceId, int? groupId, DateTime from, DateTime to)
    {
        return new ReportEventViewModel
        {
            Events = await _eventUseCase.FindAll(GetQueryParameters(deviceId, groupId, from, to)),
            Devices = await _deviceUseCase.FindAll(),
            Positions = await _positionUseCase.FindAll(),
        };
    }

    private async Task<ReportSummaryViewModel> ReportSummaryViewModelBuild(int? deviceId, int? groupId, DateTime from, DateTime to)
    {
        return new ReportSummaryViewModel
        {
            Summaries = await _summaryUseCase.FindAll(GetQueryParameters(deviceId, groupId, from, to)),
            Devices = await _deviceUseCase.FindAll()
        };
    }

    private async Task<ReportRouteViewModel> ReportRouteViewModelBuild(int? deviceId, int? groupId, DateTime from, DateTime to)
    {
        return new ReportRouteViewModel
        {
            Routes = await _routeUseCase.FindAll(GetQueryParameters(deviceId, groupId, from, to)),
            Devices = await _deviceUseCase.FindAll()
        };
    }

    private FromTo BuildFromTo()
    {
        var date = DateTime.Today;

        return new FromTo
        {
            Today = CalcDate.GetToday(date),
            Yesterday = CalcDate.GetYesterday(date),
            ThisMonth = CalcDate.GetThisMonth(date),
            LastMonth = CalcDate.GetLastMonth(date),
            ThisWeek = CalcDate.GetThisWeek(date),
            LastWeek = CalcDate.GetLastWeek(date)
        };
    }

    private void GetFromTo(int? period, ref DateTime from, ref DateTime to)
    {
        if(period != 0) 
        {
            var formTo = BuildFromTo();
            var periodMap = Period.GetMap(formTo);
        
            foreach (var item in periodMap[(int) period])
            {
                from = item.Key;
                to = item.Value;

                _logger.LogInformation(_logUtil.Succes(GetType().FullName, "GetFromTo",
                $"de: {from} até: {to}"));

            }
        }
    }

}
