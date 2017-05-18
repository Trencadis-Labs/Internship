using System;
using System.Globalization;

namespace Models.Extensions
{
  public static class StringExtensions
  {
    public static DateTime ParseWithFormat(this string value, string format)
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        return DateTime.MinValue;
      }

      DateTime result;

      DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

      return result;
    }
  }
}
