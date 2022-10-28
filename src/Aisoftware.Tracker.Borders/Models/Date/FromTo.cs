using System.Collections.Generic;
using Aisoftware.Tracker.Borders.Utils;

namespace Aisoftware.Tracker.Borders.Models.Date;

public struct FromTo
{
    public IDictionary<DateTime, DateTime> Today { get; set; }
    public IDictionary<DateTime, DateTime> Yesterday { get; set; }
    public IDictionary<DateTime, DateTime> ThisMonth { get; set; }
    public IDictionary<DateTime, DateTime> LastMonth { get; set; }
    public IDictionary<DateTime, DateTime> ThisWeek { get; set; }
    public IDictionary<DateTime, DateTime> LastWeek { get; set; }

}
