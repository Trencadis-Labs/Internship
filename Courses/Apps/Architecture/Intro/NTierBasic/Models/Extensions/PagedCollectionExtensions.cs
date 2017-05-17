using Models.Paging;

namespace Models.Extensions
{
  public static class PagedCollectionExtensions
  {
    public static bool CanGoPreviousPage<T>(this PagedCollection<T> data)
    {
      if (data == null)
      {
        return false;
      }

      return data.PageIndex > 0;
    }

    public static bool CanGoNextPage<T>(this PagedCollection<T> data)
    {
      if (data == null)
      {
        return false;
      }

      return data.PageIndex + 1 < data.TotalPagesCount;
    }

    public static int GetLastPageIndex<T>(this PagedCollection<T> data)
    {
      if ((data == null) || (data.TotalPagesCount <= 0))
      {
        return 0;
      }

      return data.TotalPagesCount - 1;
    }
  }
}
