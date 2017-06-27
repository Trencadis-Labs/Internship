using System;
using System.Globalization;

namespace Presentation.WebUI.Globalization
{
  public static class SupportedCultures
  {
    public static readonly CultureInfo Default = new CultureInfo("en-US");

    public static readonly CultureInfo[] All = new[]
    {
      SupportedCultures.Default,

      new CultureInfo("ro"),
      new CultureInfo("ro-RO")
    };

    public static bool IsSupportedCulture(string cultureName, out CultureInfo culture)
    {
      culture = null;

      if (!string.IsNullOrWhiteSpace(cultureName))
      {
        foreach (var c in SupportedCultures.All)
        {
          if (string.Equals(cultureName, c.Name, StringComparison.OrdinalIgnoreCase))
          {
            culture = c;

            return true;
          }
        }
      }

      return false;
    }
  }
}
