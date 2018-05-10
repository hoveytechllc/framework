using System;
using HoveyTech.Core.Contracts;

namespace HoveyTech.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTimeOffset ToUniversalTime(this DateTime? dateTime, IDateTimeFactory dateTimeFactory)
        {
            return dateTime.HasValue
                ? new DateTimeOffset(dateTime.Value).ToUniversalTime()
                : dateTimeFactory.UtcNowWithOffset;
        }
    }
}
