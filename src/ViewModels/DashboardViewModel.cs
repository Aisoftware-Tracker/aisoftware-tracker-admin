using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Models
{
    public class DashboardViewModel
    {
        public string LatLong { get; set; }
        public Position Position { get; set; }
        public IEnumerable<Position> Positions { get; set; }

        public Device Device { get; set; }
        public IEnumerable<Device> Devices { get; set; }
    }
}