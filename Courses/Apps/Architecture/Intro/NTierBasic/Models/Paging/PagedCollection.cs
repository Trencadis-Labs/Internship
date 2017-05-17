using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.Paging
{
  public class PagedCollection<T>
  {
    public PagedCollection(IEnumerable<T> data, int pageIndex, int pageSize, int totalRecordsCount)
    {
      if (pageIndex < 0)
      {
        throw new ArgumentException($"{nameof(pageIndex)} must be a positive numeric value");
      }

      if (pageSize < 0)
      {
        throw new ArgumentException($"{nameof(pageSize)} must be a positive numeric value");
      }

      if (totalRecordsCount < 0)
      {
        throw new ArgumentException($"{nameof(totalRecordsCount)} must be a positive numeric value");
      }

      if (data == null)
      {
        data = Enumerable.Empty<T>();
      }

      this.Data = data;
      this.PageIndex = pageIndex;
      this.PageSize = pageSize;
      this.TotalRecordsCount = totalRecordsCount;
    }

    public IEnumerable<T> Data { get; private set; }

    public int PageIndex { get; private set; }

    public int PageSize { get; private set; }

    public int TotalRecordsCount { get; private set; }

    public int TotalPagesCount
    {
      get
      {
        if (this.PageSize == 0)
        {
          return 0;
        }

        return PagingHelpers.CalculateLastPageIndex(this.TotalRecordsCount, this.PageSize);
      }
    }
  }
}
