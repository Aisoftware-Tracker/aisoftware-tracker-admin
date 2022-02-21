using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Models
{
    public class ReportViewModel
    {
        public Group Group { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public Device Device { get; set; }
        public IEnumerable<Device> Devices { get; set; }

    }
}