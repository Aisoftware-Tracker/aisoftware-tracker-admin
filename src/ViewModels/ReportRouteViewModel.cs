using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Models
{
    public class ReportRouteViewModel
    {
        public ReportRoute Route { get; set; }
        public IEnumerable<ReportRoute> Routes { get; set; }
        public Device Device { get; set; }
        public IEnumerable<Device> Devices { get; set; }
    }
}