using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.Borders.ViewModels;
public class ReportRouteViewModel
{
    public ReportRoute? Route { get; set; }
    public IEnumerable<ReportRoute>? Routes { get; set; }
    public Device? Device { get; set; }
    public IEnumerable<Device>? Devices { get; set; }
}
