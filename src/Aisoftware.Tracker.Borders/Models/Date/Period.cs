using System.Collections.Generic;

namespace Aisoftware.Tracker.Borders.Models.Date;

public struct Period
{
    public static Dictionary<int, IDictionary<DateTime, DateTime>> GetMap(FromTo fromTo)
    {
       return new Dictionary<int, IDictionary<DateTime, DateTime>> {
            { 1, fromTo.Today },
            { 2, fromTo.Yesterday },
            { 3, fromTo.ThisWeek },
            { 4, fromTo.LastWeek },
            { 5, fromTo.ThisMonth },
            { 6, fromTo.LastMonth },
        };
    } 
}
