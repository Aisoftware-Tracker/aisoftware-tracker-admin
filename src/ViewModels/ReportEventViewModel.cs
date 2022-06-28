using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Models
{
    public class ReportEventViewModel
    {
        public ReportEvent Event { get; set; }
        public IEnumerable<ReportEvent>? Events { get; set; }
        public Device Device { get; set; }
        public IEnumerable<Device>? Devices { get; set; }
        public Position Position { get; set; }
        public IEnumerable<Position>? Positions { get; set; }
        public Geoference Geoference { get; set; }
        public IEnumerable<Geoference>? Geoferences { get; set; }
        public Maintenance Maintenance { get; set; }
        public IEnumerable<Maintenance>? Maintenances { get; set; }

    }
}