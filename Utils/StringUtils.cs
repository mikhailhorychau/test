using System;
using System.Globalization;
using UIScripts.Constants;

namespace UIScripts.Utils
{
    public static class StringUtils
    {
        public static string ToSignedString(this float value) => value <= 0 ? value.ToFormatValue() : $"+{value.ToFormatValue()}";

        public static string ToSignedString(this double value) => ToSignedString((float)value);
        public static string ToSignedString(this int value) => value <= 0 ? value.ToString() : $"+{value}";

        public static string ToDegreesString(this float value, bool signed = true) => 
            $"{(signed ? value.ToSignedString() : value.ToString("0.#"))}{StringConstants.DEGREES}";

        public static string ToDevSpeedString(this float value) => $"x{value.ToFormatValue()}";

        public static string ToFormatValue(this float value) => value.ToString("0.##", CultureInfo.InvariantCulture);

        public static string ToFormatValue(this double value) => value.ToString("0.##", CultureInfo.InvariantCulture);

        public static string ToDiffFormat(this double value) => value.ToString("0.000", CultureInfo.InvariantCulture);

        public static string ToDiffString(this double value) =>
            value <= 0 ? value.ToDiffFormat() : $"+{value.ToDiffFormat()}";
        

    }
}