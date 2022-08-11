using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Models
{
    public class ReportEventViewModel
    {
        public ReportEvent Event { get; set; }
        #nullable enable
        public IEnumerable<ReportEvent>? Events { get; set; }
        public Device? Device { get; set; }
        #nullable enable
        public IEnumerable<Device>? Devices { get; set; }
        public Position? Position { get; set; }
        #nullable enable
        public IEnumerable<Position>? Positions { get; set; }
        public Geoference? Geoference { get; set; }
        #nullable enable
        public IEnumerable<Geoference>? Geoferences { get; set; }
        public Maintenance? Maintenance { get; set; }
        #nullable enable
        public IEnumerable<Maintenance>? Maintenances { get; set; }

    }
}