using Aisoftware.Tracker.Borders.Constants;
using Microsoft.AspNetCore.Http;

namespace Aisoftware.Tracker.Borders.Utils;

public static class CalcDate
{

    public static IDictionary<DateTime, DateTime> GetToday(DateTime date) => OneDay(date);

    public static IDictionary<DateTime, DateTime> GetYesterday(DateTime date) => OneDay(date.AddDays(-1));

    public static IDictionary<DateTime, DateTime> GetThisWeek(DateTime date) => OneWeek(date.AddDays(-(int) date.DayOfWeek));

    public static IDictionary<DateTime, DateTime> GetLastWeek(DateTime date) => OneWeek(date.AddDays(-((int) date.DayOfWeek + 7)));

    public static IDictionary<DateTime, DateTime> GetThisMonth(DateTime date) => OneMonth(date.Year, date.Month);

    public static Dictionary<DateTime, DateTime> GetLastMonth(DateTime date) => OneMonth(date.Year, date.AddMonths(-1).Month);

    private static IDictionary<DateTime, DateTime> OneDay(DateTime date)
    {
        DateTime firstHour = date;
		DateTime lastHour = firstHour.AddHours(23).AddMinutes(59).AddSeconds(59);

        var period = new Dictionary<DateTime, DateTime>();
		period.Add(firstHour, lastHour);
		
		return period;
    }

    private static IDictionary<DateTime, DateTime> OneWeek(DateTime date)
    {
        var firstDayWeek = date;
		var lastDayWeek = firstDayWeek.AddDays((int) DayOfWeek.Saturday);
        var lastDayWeekAndHours = lastDayWeek.AddHours(23).AddMinutes(59).AddSeconds(59);

        var period = new Dictionary<DateTime, DateTime>();
		period.Add(firstDayWeek, lastDayWeekAndHours);
		
		return period;
    }

    private static Dictionary<DateTime, DateTime> OneMonth(int year, int month)
    {
        var firstDayMonth = new DateTime(year, month, 1);
		var lastDayMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
		var lastDayMonthAndHours = lastDayMonth.AddHours(23).AddMinutes(59).AddSeconds(59);

        var period = new Dictionary<DateTime, DateTime>();
		period.Add(firstDayMonth, lastDayMonthAndHours);
		
		return period;
    }
}
