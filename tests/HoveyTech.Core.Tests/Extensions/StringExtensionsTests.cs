using HoveyTech.Core.Extensions;
using Xunit;

namespace HoveyTech.Core.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void IsPhoneNumber_does_return_true_if_10_digits()
        {
            Assert.True("1234567890".IsPhoneNumber());
        }

        [Fact]
        public void IsPhoneNumber_does_return_false_if_non_digit_characters()
        {
            Assert.False("(123) 456-789".IsPhoneNumber());
        }

        [Fact]
        public void IsPhoneNumber_does_return_true_if_still_10_digits_with_other_characters()
        {
            Assert.True("(123) 456-7890".IsPhoneNumber());
        }

        [Fact]
        public void IsPhoneNumber_must_be_10_digits()
        {
            Assert.False("1".IsPhoneNumber());
            Assert.False("12".IsPhoneNumber());
            Assert.False("123".IsPhoneNumber());
            Assert.False("1234".IsPhoneNumber());
            Assert.False("12345".IsPhoneNumber());
            Assert.False("123456".IsPhoneNumber());
            Assert.False("1234567".IsPhoneNumber());
            Assert.False("12345678".IsPhoneNumber());
            Assert.False("12345678911".IsPhoneNumber());
        }

        [Fact]
        public void ExtractDigits_does_remove_non_digit_characters()
        {
            Assert.Equal("123", "ABC1-!@#$2-qwoeiru3".ExtractDigitsOrNull());
        }

        [Fact]
        public void IsNullOrEmpty_does_return_true_for_empty()
        {
            Assert.True("".IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_does_return_true_for_null()
        {
            string value = null;
            Assert.True(value.IsNullOrEmpty());
        }

        [Fact]
        public void IsNotNullOrEmpty_does_return_true_for_empty()
        {
            Assert.True("hello".IsNotNullOrEmpty());
        }

        [Fact]
        public void IsNotNullOrEmpty_does_return_true_for_space()
        {
            Assert.True(" ".IsNotNullOrEmpty());
        }

        [Fact]
        public void EnsureNotLongerThan_does_trim_string_if_longer_than()
        {
            Assert.Equal("12345", "123456".EnsureNotLongerThan(5));
        }

        [Fact]
        public void EnsureNotLongerThan_does_not_trim_string_if_not_longer()
        {
            Assert.Equal("123", "123".EnsureNotLongerThan(5));
        }

        [Fact]
        public void EnsureNotLongerThan_returns_value_if_empty_orNull()
        {
            Assert.Equal("", "".EnsureNotLongerThan(5));
            Assert.Null(((string)null).EnsureNotLongerThan(5));
        }

        [Fact]
        public void IsValidEmailAddress_does_return_false_for_random_text()
        {
            Assert.False(((string)null).IsValidEmailAddress());
            Assert.False("".IsValidEmailAddress());
            Assert.False("12345".IsValidEmailAddress());
            Assert.False("test.user".IsValidEmailAddress());
            Assert.False("test.user@company".IsValidEmailAddress());
            Assert.False("test.user@company.".IsValidEmailAddress());
            Assert.False("test.user@.company".IsValidEmailAddress());
        }
    }
}
