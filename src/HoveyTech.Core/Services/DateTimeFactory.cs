using System;
using HoveyTech.Core.Contracts;

namespace HoveyTech.Core.Services
{
    public class DateTimeFactory : IDateTimeFactory
    {
        public DateTimeOffset UtcNowWithOffset => DateTimeOffset.UtcNow;

        public DateTimeOffset NowWithOffset => DateTimeOffset.Now;

        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime Now => DateTime.Now;
    }
}
