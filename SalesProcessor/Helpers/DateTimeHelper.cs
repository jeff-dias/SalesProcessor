using System;

namespace SalesProcessor.Helpers
{
    public static class DateTimeHelper
    {
        internal static DateTime DateTimeBrazil()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        }

        internal static string DateTimeBrazil(string mask)
        {
            var datetimeNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            return datetimeNow.ToString(mask);
        }
    }
}
