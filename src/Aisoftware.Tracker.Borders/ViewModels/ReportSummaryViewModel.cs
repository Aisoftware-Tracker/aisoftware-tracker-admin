using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.Borders.ViewModels;
public class ReportSummaryViewModel
{
    public ReportSummary? Summary { get; set; }
    public IEnumerable<ReportSummary> Summaries { get; set; }
    public Device? Device { get; set; }
    public IEnumerable<Device>? Devices { get; set; }
}
