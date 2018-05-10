using System;
using HoveyTech.Core.Extensions;
using Xunit;

namespace HoveyTech.Core.Tests.Extensions
{
    public class DateTimeOffsetExtensionsTests
    {
        [Fact]
        public void ToBeginningOfDay_does_set_hour_minute_and_second_to_zeros()
        {
            var middleOfDay = new DateTimeOffset(2017, 7, 31, 1, 2, 3, TimeSpan.FromHours(5));
            Assert.Equal(new DateTimeOffset(2017, 7, 31, 0, 0, 0, TimeSpan.FromHours(5)), middleOfDay.ToBeginningOfDay());
        }

        [Fact]
        public void ToEndOfDay_does_set_hour_minute_and_second_23_59_59()
        {
            var middleOfDay = new DateTimeOffset(2017, 7, 31, 1, 2, 3, TimeSpan.FromHours(5));
            Assert.Equal(new DateTimeOffset(2017, 7, 31, 23, 59, 59, TimeSpan.FromHours(5)), middleOfDay.ToEndOfDay());
        }
    }
}
