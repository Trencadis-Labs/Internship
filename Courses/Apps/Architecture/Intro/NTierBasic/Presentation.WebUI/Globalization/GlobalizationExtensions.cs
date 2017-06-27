using Microsoft.Extensions.Primitives;
using System;
using System.Globalization;

namespace Presentation.WebUI.Globalization
{
  public static class GlobalizationExtensions
  {
    public static CultureInfo GetRequestCulture(this Microsoft.AspNetCore.Http.HttpRequest request)
    {
      if (request != null)
      {
        // try to get culture from Accept-Language header
        var acceptLangs = request.Headers["Accept-Language"];
        if (!StringValues.IsNullOrEmpty(acceptLangs))
        {
          foreach (var preferredLang in acceptLangs)
          {
            if (SupportedCultures.IsSupportedCulture(preferredLang, out CultureInfo culture))
            {
              return culture;
            }
          }

          if (acceptLangs.Count == 1)
          {
            var items = acceptLangs.ToString().Split(',');

            foreach (var item in items)
            {
              var subItems = item.Split(';');

              foreach (var preferredLang in subItems)
              {
                if (SupportedCultures.IsSupportedCulture(preferredLang, out CultureInfo culture))
                {
                  return culture;
                }
              }
            }
          }
        }
      }

      return SupportedCultures.Default;
    }

    public static string GetDateTimeFormat_JQueryUI(this Microsoft.AspNetCore.Http.HttpRequest request)
    {
      var culture = request.GetRequestCulture();

      var datePattern = culture.DateTimeFormat.ShortDatePattern;

      if (!string.IsNullOrWhiteSpace(datePattern))
      {
        return datePattern.Replace("M", "m")
                          .Replace("yyyy", "yy");
      }

      // default format for jQuery UI
      return "mm/dd/yy";
    }

    public static string FormatDateTimeFor_JQueryUI(this Microsoft.AspNetCore.Http.HttpRequest request, DateTime dateTime)
    {
      if (dateTime > DateTime.MinValue)
      {
        var culture = request.GetRequestCulture();

        return dateTime.ToString(culture.DateTimeFormat.ShortDatePattern);
      }

      return string.Empty;
    }
  }
}
