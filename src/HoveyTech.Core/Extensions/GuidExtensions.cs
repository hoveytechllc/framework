using System;
using System.Globalization;
using System.Text;

namespace HoveyTech.Core.Extensions
{
    public static class GuidExtensions
    {
        public static string ToByteString(this Guid value)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("X'");

            foreach (var @byte in value.ToByteArray())
                stringBuilder.Append(@byte.ToString("X2", CultureInfo.InvariantCulture));

            stringBuilder.Append("'");
            return stringBuilder.ToString();
        }
    }
}
