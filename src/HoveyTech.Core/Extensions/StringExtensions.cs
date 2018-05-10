using System;
using System.Text.RegularExpressions;

namespace HoveyTech.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsPhoneNumber(this string value)
        {
            value = value.ExtractDigitsOrNull();

            if (value.IsNullOrEmpty())
                return false;

            return Regex.IsMatch(value, @"^[\d]{10}$");
        }

        public static string ExtractDigitsOrNull(this string value)
        {
            if (value.IsNullOrEmpty())
                return null;

            value = Regex.Replace(value, @"[^\d]", string.Empty);

            if (value == string.Empty)
                return null;

            return value;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static string EnsureNotLongerThan(this string str, int length)
        {
            if (str.IsNullOrEmpty())
                return str;
            if (str.Length > length)
                return str.Substring(0, length);
            return str;
        }

        public static bool IsValidEmailAddress(this string value)
        {
            if (IsNullOrEmpty(value))
                return false;

            try
            {
                return Regex.IsMatch(value,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
