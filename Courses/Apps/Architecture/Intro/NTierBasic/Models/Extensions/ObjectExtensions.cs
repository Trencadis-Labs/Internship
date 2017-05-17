namespace Models.Extensions
{
  public static class ObjectExtensions
  {
    public static string ToFixedWidthString<T>(this T obj, int totalWidth)
    {
      return obj.ToString().PadRight(totalWidth);
    }
  }
}
