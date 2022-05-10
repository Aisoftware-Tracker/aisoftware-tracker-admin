using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Models
{
    public class ReportSummaryViewModel
    {
        public ReportSummary Summary { get; set; }
        public IEnumerable<ReportSummary> Summaries { get; set; }
        public Device Device { get; set; }
        public IEnumerable<Device> Devices { get; set; }
    }
}