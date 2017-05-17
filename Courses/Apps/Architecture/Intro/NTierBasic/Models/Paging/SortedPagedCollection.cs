using Models.Sorting;
using System.Collections.Generic;

namespace Models.Paging
{
  public class SortedPagedCollection<T, TSort> : PagedCollection<T>
    where TSort : struct
  {
    public SortedPagedCollection(IEnumerable<T> data, int pageIndex, int pageSize, int totalRecordsCount, TSort sortCriteria, SortDirection sortDirection)
      : base(data, pageIndex, pageSize, totalRecordsCount)
    {
      this.SortCriteria = sortCriteria;

      this.SortDirection = sortDirection;
    }

    public TSort SortCriteria { get; private set; }

    public SortDirection SortDirection { get; private set; }
  }
}
