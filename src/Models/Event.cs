using System;
using Newtonsoft.Json;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;

namespace Aisoftware.Tracker.Admin.Models
{
    public class ReportEvent
    {
        private int _id;
        private string _type;
        private DateTime? _serverTime;
        private int? _deviceId;
        private int? _positionId;
        private int? _geofenceId;
        private int? _maintenanceId;
        private object _attributes;

        [JsonProperty("id")]
        public int Id { get => _id; set => _id = value; }

        [JsonProperty("type")]
        public string Type { get => _type; set => _type = value; }

        [JsonProperty("serverTime")]
        public DateTime? ServerTime { get => _serverTime; set => _serverTime = value; }

        [JsonProperty("deviceId")]
        public int? DeviceId { get => _deviceId; set => _deviceId = value; }

        [JsonProperty("positionId")]
        public int? PositionId { get => _positionId; set => _positionId = value; }

        [JsonProperty("geofenceId")]
        public int? GeofenceId { get => _geofenceId; set => _geofenceId = value; }

        [JsonProperty("maintenanceId")]
        public int? MaintenanceId { get => _maintenanceId; set => _maintenanceId = value; }

        [JsonProperty("attributes")]
        public object Attributes { get => _attributes; set => _attributes = value; }

         public string ServerTimeStr { get => _serverTime?.ToString(FormatString.FORMAT_DATE_TIME_BR); }
    }
}