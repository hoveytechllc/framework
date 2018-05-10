using System;
using HoveyTech.Core.Services;
using Xunit;

namespace HoveyTech.Core.Tests.Services
{
    public class DateTimeFactoryTests
    {
        [Fact]
        public void UtcNowWithOffset_offset_is_zero()
        {
            var result = new DateTimeFactory().UtcNowWithOffset;
            Assert.Equal(0, result.Offset.Hours);
        }

        [Fact]
        public void NowWithOffset_offset_is_from_active_timeZone()
        {
            var result = new DateTimeFactory().NowWithOffset;
            Assert.Equal(DateTimeOffset.Now.Offset.Hours, result.Offset.Hours);
        }

        [Fact]
        public void UtcNowWithOffset_does_match_DateTimeOffset_UtcNow()
        {
            var result = new DateTimeFactory().UtcNowWithOffset;
            Assert.InRange(DateTimeOffset.UtcNow.Subtract(result), TimeSpan.Zero, TimeSpan.FromMilliseconds(5));
        }

        [Fact]
        public void NowWithOffset_does_match_DateTimeOffset_UtcNow()
        {
            var result = new DateTimeFactory().NowWithOffset;
            Assert.InRange(DateTimeOffset.Now.Subtract(result), TimeSpan.Zero, TimeSpan.FromMilliseconds(5));
        }

        [Fact]
        public void UtcNow_does_match_DateTime_UtcNow()
        {
            var result = new DateTimeFactory().UtcNow;
            Assert.InRange(DateTime.UtcNow.Subtract(result), TimeSpan.Zero, TimeSpan.FromMilliseconds(5));
        }

        [Fact]
        public void Now_does_match_DateTime_Now()
        {
            var result = new DateTimeFactory().Now;
            Assert.InRange(DateTime.Now.Subtract(result), TimeSpan.Zero, TimeSpan.FromMilliseconds(5));
        }
    }
}
