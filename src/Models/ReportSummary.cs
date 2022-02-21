using Newtonsoft.Json;

namespace Aisoftware.Tracker.Admin.Models
{
    public class ReportSummary
    {
        private int _deviceId;
        private string _deviceName;
        private decimal _distance;
        private decimal _averageSpeed;
        private decimal _maxSpeed;
        private decimal _spentFuel;
        private decimal _startOdometer;
        private decimal _endOdometer;
        private long _engineHours;

        [JsonProperty("deviceId")]
        public int DeviceId { get => _deviceId; set => _deviceId = value; }
        
        [JsonProperty("deviceName")]
        public string DeviceName { get => _deviceName; set => _deviceName = value; }
        
        [JsonProperty("distance")]
        public decimal Distance { get => _distance; set => _distance = value; }
        
        [JsonProperty("averageSpeed")]
        public decimal AverageSpeed { get => _averageSpeed; set => _averageSpeed = value; }
        
        [JsonProperty("maxSpeed")]
        public decimal MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }
        
        [JsonProperty("spentFuel")]
        public decimal SpentFuel { get => _spentFuel; set => _spentFuel = value; }
        
        [JsonProperty("startOdometer")]
        public decimal StartOdometer { get => _startOdometer; set => _startOdometer = value; }
        
        [JsonProperty("endOdometer")]
        public decimal EndOdometer { get => _endOdometer; set => _endOdometer = value; }
        
        [JsonProperty("engineHours")]
        public long EngineHours { get => _engineHours; set => _engineHours = value; }
    }
}

