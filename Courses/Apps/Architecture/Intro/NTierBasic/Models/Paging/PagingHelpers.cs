using System;

namespace Models.Paging
{
  public static class PagingHelpers
  {
    public static int CalculateLastPageIndex(int totalRecordsCount, int pageSize)
    {
      if (pageSize <= 0)
      {
        throw new ArgumentException($"{nameof(pageSize)} must be a strict-positive integer value");
      }

      return (totalRecordsCount % pageSize == 0) ?
                totalRecordsCount / pageSize
                :
                (totalRecordsCount / pageSize) + 1;
    }
  }
}
