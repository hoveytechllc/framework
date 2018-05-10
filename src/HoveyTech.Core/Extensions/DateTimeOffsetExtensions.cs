using System;

namespace HoveyTech.Core.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset ToBeginningOfDay(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year,
                date.Month,
                date.Day,
                0, 
                0, 
                0,
                date.Offset);
        }

        public static DateTimeOffset ToEndOfDay(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year,
                date.Month,
                date.Day,
                23,
                59,
                59,
                date.Offset);
        }
    }
}
