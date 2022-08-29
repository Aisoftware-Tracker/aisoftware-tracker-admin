using System;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Borders.Models.Reports;
using Aisoftware.Tracker.Borders.Constants;
using System.Text;
using System.Linq;
using Aisoftware.Tracker.Borders.ViewModels;

namespace Aisoftware.Tracker.Borders.Services
{
    public static class ExportFileUtil
    {
        private const string CSV = "csv";
        private const string XLSX = "xlsx";

        public static FileContentResult ExportToCsv(int? deviceId, int? groupId, DateTime from, DateTime to,
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

            return FileContentResultBuild(builder, "RelatorioRotas");

        }

        public static FileContentResult ExportToCsv(int? deviceId, int? groupId, DateTime from, DateTime to,
             ReportEventViewModel viewModel)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Placa; Hora do Servidor; Tipo; Endereco;");

            foreach (var item in viewModel.Events)
            {
                string? licensePlate = viewModel.Devices?.Where(x => x.Id == item?.DeviceId)?.FirstOrDefault()?.Name;
                string? type = EventType.Get()[item.Type];
                string? address = viewModel.Positions?.Where(x => x.Id == item?.PositionId)?.FirstOrDefault()?.Address;

                type = StringUtil.RemoveAccent(type);

                builder.AppendLine($"{licensePlate}; {item.ServerTimeStr}; {type}; {StringUtil.RemoveAccent(address)};");
            }

            return FileContentResultBuild(builder, "RelatorioEventos");

        }

        public static FileContentResult ExportToCsv(int? deviceId, int? groupId, DateTime from, DateTime to,
             ReportSummaryViewModel viewModel)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Placa; Nome do Dispositivo; Distancia; Velocidade Media; Velocidade Maxima; Combustivel; Odometro Inicial; Odometro Final; Tempo de Motor");

            foreach (var item in viewModel.Summaries)
            {
                string? licensePlate = viewModel.Devices?.Where(x => x.Id == item?.DeviceId)?.FirstOrDefault()?.Name;

                builder.AppendLine($"{licensePlate}; {item.DeviceName}; {item.Distance}; {item.AverageSpeed}; {item.MaxSpeed}; {item.SpentFuel}; {item.StartOdometer}; {item.EndOdometer}; {item.EngineHours}");
            }

            return FileContentResultBuild(builder, "RelatorioResumo");

        }

        private static FileContentResult FileContentResultBuild(StringBuilder builder, string reportName)
        {    
            return new FileContentResult(Encoding.UTF8.GetBytes(builder.ToString()), ContentType.TEXT_CSV)
            {
                FileDownloadName = $"{reportName}_{DateTime.Now.ToString(FormatString.FORMAT_DATE_YYYY_MM_DD_HH_MM)}.{CSV}"
            };
        }
    }

}