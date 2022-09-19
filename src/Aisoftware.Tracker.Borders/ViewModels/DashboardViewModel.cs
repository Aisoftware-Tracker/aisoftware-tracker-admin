using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.Borders.ViewModels
{
    public class DashboardViewModel
    {
        public string LatLong { get; set; }
        public Position Position { get; set; }
        public IEnumerable<Position> Positions { get; set; }

        public Device Device { get; set; }
        public IEnumerable<Device> Devices { get; set; }

        public Group Group { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}