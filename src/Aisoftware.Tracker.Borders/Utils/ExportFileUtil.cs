using Aisoftware.Tracker.Borders.Constants;
using Aisoftware.Tracker.Borders.Models.Reports;
using Aisoftware.Tracker.Borders.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Borders.Services;

public static class ExportFileUtil
{
    private const string CSV = "csv";
    private const string XLSX = "xlsx";

    public async static Task<FileContentResult> ExportToCsv(int? deviceId, int? groupId, DateTime from, DateTime to,
            ReportRouteViewModel viewModel)
    {
        var builder = new StringBuilder();

        builder.AppendLine("Placa; Protocolo; Horario do Dispositivo; Horario Corrigido; Horario do Servidor; Vencimento; Valido; Latitude; Longitude; Altitude; Velociadade; Endereco; Irregularidade; Ignicao; Status; Distancia; Distancia Total /Km; Movimentação; Horas");

        foreach (var item in viewModel.Routes)
        {
            string outdated = item.Outdated ? "Desatualizado" : "Atualizado";
            string valid = item.Valid ? "Sim" : "Nao";
            string ignition = item.Attributes.Ignition ? "Ligado" : "Desligado";
            string motion = item.Attributes.Motion ? "Em Movimento" : "Parado";
            string? licensePlate = viewModel.Devices?.Where(x => x.Id == item?.DeviceId)?.FirstOrDefault()?.Name;

            builder.AppendLine($"{licensePlate}; {item.Protocol}; {item.DeviceTimeStr}; {item.FixTimeStr}; {item.ServerTimeStr}; {outdated}; {valid}; {item.LatitudeStr}; {item.LongitudeStr}; {item.Altitude}; {item.Speed}; {StringUtil.RemoveAccent(item.Address)}; {item.Accuracy}; {ignition}; {item.Attributes.Status}; {item.Attributes.Distance}; {item.Attributes.TotalDistance}; {motion}; {item.Attributes.Hours}");
        }

        return await FileContentResultBuild(builder, "RelatorioRotas");

    }

    public async static Task<FileContentResult> ExportToCsv(int? deviceId, int? groupId, DateTime from, DateTime to,
            ReportEventViewModel viewModel)
    {
        var builder = new StringBuilder();

        builder.AppendLine("Placa; Hora do Servidor; Tipo; Endereco;");

        foreach (var item in viewModel.Events)
        {
            string? licensePlate = viewModel.Devices?.Where(x => x.Id == item?.DeviceId)?.FirstOrDefault()?.Name;
            var dictionary = EventType.Get();
            string? type = EventType.GetValueOrDefault(dictionary, item.Type, "Nao Identificado");
            string? address = viewModel.Positions?.Where(x => x.Id == item?.PositionId)?.FirstOrDefault()?.Address;

            type = StringUtil.RemoveAccent(type);

            builder.AppendLine($"{licensePlate}; {item.ServerTimeStr}; {type}; {StringUtil.RemoveAccent(address)};");
        }

        return await FileContentResultBuild(builder, "RelatorioEventos");

    }

    public async static Task<FileContentResult> ExportToCsv(int? deviceId, int? groupId, DateTime from, DateTime to,
            ReportSummaryViewModel viewModel)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Placa; Nome do Dispositivo; Distancia; Velocidade Media; Velocidade Maxima; Combustivel; Odometro Inicial; Odometro Final; Tempo de Motor");

        foreach (var item in viewModel.Summaries)
        {
            string? licensePlate = viewModel.Devices?.Where(x => x.Id == item?.DeviceId)?.FirstOrDefault()?.Name;

            builder.AppendLine($"{licensePlate}; {item.DeviceName}; {item.Distance}; {item.AverageSpeed}; {item.MaxSpeed}; {item.SpentFuel}; {item.StartOdometer}; {item.EndOdometer}; {item.EngineHours}");
        }

        return await FileContentResultBuild(builder, "RelatorioResumo");

    }

    public async static Task<FileContentResult> ExportToCsv(List<List<String>>? reports, string typeReport)
    {
        switch (typeReport)
        {
            case Endpoints.SUMMARY:
                return await ExportCsvSummary(reports);
            case Endpoints.ROUTE:
               return await ExportCsvRoutes(reports);
            case Endpoints.EVENTS:
                return await ExportCsvEvents(reports);
            default:
                return null;
        }
    }

    private async static Task<FileContentResult> ExportCsvRoutes(List<List<String>>? reports)
    {
        const string uncheck = "<input class=\"check-box\" disabled=\"disabled\" type=\"checkbox\">";
        const string check = "<input checked=\"checked\" class=\"check-box\" disabled=\"disabled\" type=\"checkbox\">";
        
        var builder = new StringBuilder();

        builder.AppendLine("Placa; Protocolo; Horario do Dispositivo; Horario Corrigido; Horario do Servidor; Vencimento; Valido; Latitude; Longitude; Altitude; Velociadade; Endereco; Irregularidade; Ignicao; Status; Distancia; Distancia Total /Km; Movimentacao; Horas");

        foreach (var item in reports)
        {
            string licensePlate = item[1];
            string protocol = item[2];
            string deviceTimeStr = item[3];
            string fixTimeStr = item[4];
            string serverTimeStr = item[5];
            string outdated = item[6] == uncheck ? "Desatualizado" : "Atualizado";
            string valid = item[7]  == check ? "Sim" : "Nao";
            string latitudeStr = item[8];
            string longitudeStr = item[9];
            string altitude = item[10];
            string speed = item[11];
            string address = StringUtil.RemoveAccent(item[12]);
            string accuracy = item[13];
            string ignition = item[14] == check ? "Ligado" : "Desligado";
            string attributesStatus = item[15];
            string attributesDistance = item[16];
            string attributesTotalDistance = item[17];
            string motion = item[18] == check ? "Em Movimento" : "Parado";
            string attributesHours = item[19];

            builder.AppendLine($"{licensePlate}; {protocol}; {deviceTimeStr}; {fixTimeStr}; {serverTimeStr}; {outdated}; {valid}; {latitudeStr}; {longitudeStr}; {altitude}; {speed}; {address}; {accuracy}; {ignition}; {attributesStatus}; {attributesDistance}; {attributesTotalDistance}; {motion}; {attributesHours}");
        }

        return await FileContentResultBuild(builder, "RelatorioRotas");
    }

    private async static Task<FileContentResult> ExportCsvSummary(List<List<String>>? reports)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Nome do Dispositivo; Placa; Velocidade Maxima; Velocidade Media; Distancia; Combistivel; Tempo de Motor; Odometro Inicial; Odometro Final");

        foreach (var item in reports)
        {
            string deviceName = item[0];
            string licensePlate = item[1];
            string maxSpeed = item[2];
            string averageSpeed = item[3];
            string distance = item[4];
            string spentFuel = item[5];
            string engineHours = item[6];
            string startOdometer = item[7];
            string endOdometer = item[8];

            builder.AppendLine($"{deviceName}; {licensePlate}; {maxSpeed}; {averageSpeed}; {distance}; {spentFuel}; {engineHours}; {startOdometer}; {endOdometer}");
        }

        return await FileContentResultBuild(builder, "RelatorioResumo");
    }

    private async static Task<FileContentResult> ExportCsvEvents(List<List<String>>? reports)
    {
        var builder = new StringBuilder();
        builder.AppendLine("Placa; Hora do Servidor; Tipo; Endereco");

        foreach (var item in reports)
        {
            string? licensePlate = item[1];
            string serverTime = item[2];
            var dictionary = EventType.Get();

            string keyValue = getKeyName(dictionary.Keys, item[3]);

            string? type = EventType.GetValueOrDefault(dictionary, keyValue, "Nao Identificado");
            string? address = StringUtil.RemoveAccent(item[4]);
            type = StringUtil.RemoveAccent(type);

            builder.AppendLine($"{licensePlate}; {serverTime}; {type}; {address}");
        }

        return await FileContentResultBuild(builder, "RelatorioEventos");
    }

    private static string getKeyName(IEnumerable<string> keys, string item)
    {
        foreach(var key in keys)
        {
            if(item.Contains(key))
            {
                return key;
            }
        }

        return String.Empty;
    }

    private async static Task<FileContentResult> FileContentResultBuild(StringBuilder builder, string reportName)
    {
        return new FileContentResult(Encoding.UTF8.GetBytes(builder.ToString()), ContentType.TEXT_CSV)
        {
            FileDownloadName = $"{reportName}_{DateTime.Now.ToString(FormatString.FORMAT_DATE_YYYY_MM_DD_HH_MM)}.{CSV}"
        };
    }
}
