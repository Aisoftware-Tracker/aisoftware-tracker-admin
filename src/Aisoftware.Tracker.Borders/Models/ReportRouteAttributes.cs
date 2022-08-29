using Newtonsoft.Json;

namespace Aisoftware.Tracker.Borders.Models
{
    public class ReportRouteAttributes
    {
        private bool _ignition;
        private long _status;
        private decimal _distance;
        private decimal _totalDistance;
        private bool _motion;
        private long _hours;

        [JsonProperty("ignition")]
        public bool Ignition { get => _ignition; set => _ignition = value; }

        [JsonProperty("status")]
        public long Status { get => _status; set => _status = value; }

        [JsonProperty("distance")]
        public decimal Distance { get => _distance; set => _distance = value; }

        [JsonProperty("totalDistance")]
        public decimal TotalDistance { get => _totalDistance; set => _totalDistance = value; }

        [JsonProperty("motion")]
        public bool Motion { get => _motion; set => _motion = value; }

        [JsonProperty("hours")]
        public long Hours { get => _hours; set => _hours = value; }
    }
}