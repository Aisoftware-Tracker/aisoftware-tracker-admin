using System;
using Microsoft.AspNetCore.Mvc;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using System.Text;
using System.Linq;

namespace Aisoftware.Tracker.Admin.Common.Util
{
    public static class ExportFileUtil
    {
        private const string CSV = "csv";
        private const string XLSX = "xlsx";
        public static FileContentResult ExportToCsv(int? deviceId, int? groupId, DateTime from, DateTime to,
             ReportRouteViewModel viewModel)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Id; Placa; Protocolo; Horario do Dispositivo; Horario Corrigido; Horario do Servidor; Vencimento; Valido; Latitude; Longitude; Altitude; Velociadade; Endereco; Irregularidade; Ignicao; Status; Distancia; Distancia Total /Km; Movimentação; Horas");

            foreach (var item in viewModel.Routes)
            {
                string outdated = item.Outdated ? "Desatualizado" : "Atualizado";
                string valid = item.Valid ? "Sim" : "Nao";
                string ignition = item.Attributes.Ignition ? "Ligado" : "Desligado";
                string motion = item.Attributes.Motion ? "Em Movimento" : "Parado";
                string placa = viewModel.Devices.Where(x => x.Id == item.DeviceId).FirstOrDefault().Name;

                builder.AppendLine($"{item.Id}; {placa}; {item.Protocol}; {item.DeviceTimeStr}; {item.FixTimeStr}; {item.ServerTimeStr}; {outdated}; {valid}; {item.LatitudeStr}; {item.LongitudeStr}; {item.Altitude}; {item.Speed}; {item.Address}; {item.Accuracy}; {ignition}; {item.Attributes.Status}; {item.Attributes.Distance}; {item.Attributes.TotalDistance}; {motion}; {item.Attributes.Hours}");
            }

            FileContentResult result = new FileContentResult(Encoding.UTF8.GetBytes(builder.ToString()), ContentType.TEXT_CSV)
            {
                FileDownloadName = $"ReportRoute_{DateTime.Now.ToString(FormatString.FORMAT_DATE_YYYY_MM_DD_HH_MM)}.{CSV}"
            };

            return result;
        }

        public static FileContentResult ExportToCsv(int? deviceId, int? groupId, DateTime from, DateTime to,
             ReportEventViewModel viewModel)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Id; Dispositivos; Hora do Servidor; Tipo; Endereço; Geoference Name; Maintenance Name; Attributes;");

            foreach (var item in viewModel.Events)
            {
                string licensePlate = viewModel.Devices.Where(x => x.Id == item.DeviceId).FirstOrDefault().Name;
                string address = viewModel.Positions.Where(x => x.Id == item.PositionId).FirstOrDefault().Address;
                string geofenceName = viewModel.Geoferences.Where(x => x.Id == item.GeofenceId).FirstOrDefault().Name;
                string maintenanceName = viewModel.Maintenances.Where(x => x.Id == item.MaintenanceId).FirstOrDefault().Name;

                builder.AppendLine($"{item.Id}; {licensePlate}; {item.ServerTime}; {address}; {geofenceName}; {maintenanceName}; {item.Attributes};");
            }

            FileContentResult result = new FileContentResult(Encoding.UTF8.GetBytes(builder.ToString()), ContentType.TEXT_CSV)
            {
                FileDownloadName = $"ReportEvents_{DateTime.Now.ToString(FormatString.FORMAT_DATE_YYYY_MM_DD_HH_MM)}.{CSV}"
            };

            return result;
        }

        public static FileContentResult ExportToCsv(int? deviceId, int? groupId, DateTime from, DateTime to,
             ReportSummaryViewModel viewModel)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Placa; Nome do Dispositivo; Distancia; Velocidade Media; Velocidade Maxima; Combustivel; Odometro Inicial; Odometro Final; Tempo de Motor");

            foreach (var item in viewModel.Summaries)
            {
                string licensePlate = viewModel.Devices.Where(x => x.Id == item.DeviceId).FirstOrDefault().Name;

                builder.AppendLine($"{licensePlate}; {item.DeviceName}; {item.Distance}; {item.AverageSpeed}; {item.MaxSpeed}; {item.SpentFuel}; {item.StartOdometer}; {item.EndOdometer}; {item.EngineHours}");
            }

            FileContentResult result = new FileContentResult(Encoding.UTF8.GetBytes(builder.ToString()), ContentType.TEXT_CSV)
            {
                FileDownloadName = $"ReportSummary_{DateTime.Now.ToString(FormatString.FORMAT_DATE_YYYY_MM_DD_HH_MM)}.{CSV}"
            };

            return result;
        }
    }

}