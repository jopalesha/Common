using System;
using System.Globalization;
using System.Linq;

namespace Jopalesha.Common.Infrastructure.Helpers
{
    public static class StringHelper
    {
        public static bool TryParseToDouble(string value, out double result)
        {
            result = 0;
            return !string.IsNullOrWhiteSpace(value) && double.TryParse(value.Replace(',', '.'),
                       NumberStyles.Number, NumberFormatInfo.InvariantInfo, out result);
        }

        public static bool ContainsAny(this string input, StringComparison comparisonType,
            params string[] containsKeywords) =>
            containsKeywords.Any(keyword => input.IndexOf(keyword, comparisonType) >= 0);
    }
}
