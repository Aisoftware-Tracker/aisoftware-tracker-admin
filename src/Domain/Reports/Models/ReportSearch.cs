using Newtonsoft.Json;

namespace Aisoftware.Tracker.Admin.Domain.Reports.Model
{
    public class ReportSearch
    {
        private int? _deviceId;
        private int? _groupId;
        private string _from;
        private string _to;

        [JsonProperty("deviceId")]
        public int? DeviceId { get => _deviceId; set => _deviceId = value; }
        
        [JsonProperty("groupId")]
        public int? GroupId { get => _groupId; set => _groupId = value; }
        
        [JsonProperty("from")]
        public string From { get => _from; set => _from = value; }
        
        [JsonProperty("to")]
        public string To { get => _to; set => _to = value; }
    }
}
