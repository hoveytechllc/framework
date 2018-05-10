using System;

namespace HoveyTech.Core.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToTimeAgoDescription(this TimeSpan timeSpan)
        {
            string Build(double count, string label)
            {
                var intCount = Convert.ToInt32(count);

                return $"{intCount} {label}"
                       + (intCount > 1 ? "s" : string.Empty)
                       + " ago";
            }

            var daysInMonth = 365 / 12;

            if (timeSpan.TotalSeconds < 60)
                return "a few seconds ago";
            if (timeSpan.TotalMinutes < 60)
                return Build(timeSpan.TotalMinutes, "minute");
            if (timeSpan.TotalHours < 24)
                return Build(timeSpan.TotalHours, "hour");
            if (timeSpan.TotalDays < daysInMonth)
                return Build(timeSpan.TotalDays, "day");
            if (timeSpan.TotalDays < 365)
                return Build(timeSpan.TotalDays / daysInMonth, "month");

            return Build(timeSpan.TotalDays / 365, "year");
        }
    }
}
