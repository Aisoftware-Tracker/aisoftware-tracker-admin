using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Models
{
    public class DeviceViewModel
    {
        public Device Device { get; set; }

        public IEnumerable<Device> Devices { get; set; }

        public Group Group { get; set; }

        public IEnumerable<Group> Groups { get; set; }

    }
}