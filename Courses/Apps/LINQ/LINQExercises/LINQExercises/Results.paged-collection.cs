using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQExercises
{
  public partial class Results
  {
    public class PagedCollection<T>
    {
      public PagedCollection(
        IEnumerable<T> itemsPerPage,
        int pageSize,
        int pageIndex,
        int totalElementsCount)
      {
        if (pageSize < 0)
        {
          throw new ArgumentException($"Page size ('{pageSize}') must be a positive number!");
        }

        if (pageIndex < 0)
        {
          throw new ArgumentException($"Page index ('{pageIndex}') must be a positive number!");
        }

        if (totalElementsCount < 0)
        {
          throw new ArgumentException($"Total elements count ('{totalElementsCount}') must be a positive number!");
        }

        if (itemsPerPage == null)
        {
          itemsPerPage = Enumerable.Empty<T>();
        }

        this.Items = itemsPerPage;
        this.PageSize = pageSize;
        this.PageIndex = pageIndex;
        this.TotalElementsCount = totalElementsCount;
      }

      public IEnumerable<T> Items { get; private set; }

      public int PageSize { get; private set; }

      public int PageIndex { get; private set; }

      public int TotalElementsCount { get; private set; }
    }
  }
}
