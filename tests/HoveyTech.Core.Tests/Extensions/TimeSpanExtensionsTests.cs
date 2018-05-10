using System;
using HoveyTech.Core.Extensions;
using Xunit;

namespace HoveyTech.Core.Tests.Extensions
{
    public class TimeSpanExtensionsTests
    {
        [Fact]
        public void ToTimeAgoDescription_does_return_a_few_seconds_ago_if_less_than_minute()
        {
            var sut = TimeSpan.FromSeconds(59);
            Assert.Equal("a few seconds ago", sut.ToTimeAgoDescription());
        }

        [Fact]
        public void ToTimeAgoDescription_does_return_a_5_minutes_ago()
        {
            var sut = TimeSpan.FromMinutes(5);
            Assert.Equal("5 minutes ago", sut.ToTimeAgoDescription());
        }

        [Fact]
        public void ToTimeAgoDescription_does_return_10_days_ago()
        {
            var sut = TimeSpan.FromDays(10);
            Assert.Equal("10 days ago", sut.ToTimeAgoDescription());
        }

        [Fact]
        public void ToTimeAgoDescription_does_return_1_month_ago()
        {
            var sut = TimeSpan.FromDays(35);
            Assert.Equal("1 month ago", sut.ToTimeAgoDescription());
        }

        [Fact]
        public void ToTimeAgoDescription_does_return_4_months_ago()
        {
            var sut = TimeSpan.FromDays(4 * (365 / 12) + 1);
            Assert.Equal("4 months ago", sut.ToTimeAgoDescription());
        }

        [Fact]
        public void ToTimeAgoDescription_does_return_1_year_ago()
        {
            var sut = TimeSpan.FromDays(370);
            Assert.Equal("1 year ago", sut.ToTimeAgoDescription());
        }
    }
}
