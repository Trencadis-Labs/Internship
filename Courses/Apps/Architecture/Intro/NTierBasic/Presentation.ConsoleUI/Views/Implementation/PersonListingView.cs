using Models;
using Models.Extensions;
using Models.Paging;
using Models.Sorting;
using Presentation.ConsoleUI.Views.Abstractions;
using System;

namespace Presentation.ConsoleUI.Views.Implementation
{
  public class PersonListingView : IView<SortedPagedCollection<Person, PersonSortCriteria>>
  {
    public void Render(SortedPagedCollection<Person, PersonSortCriteria> data)
    {
      Console.Clear();

      ConsoleHelper.WriteSectionForCollection(
        $"Person listing (page {data.PageIndex + 1} of {data.TotalPagesCount})",
        $"(sort criteria: {GetSortCriteriaString(data.SortCriteria)}, {GetSortDirectionString(data.SortDirection)})",
        data.Data,
        p => Console.WriteLine($"#{p.Id.ToFixedWidthString(5)} {p.FullName.ToFixedWidthString(25)} {p.DateOfBirth:yyyy-MM-dd}"),
        () => Console.WriteLine("(no persons)"));
    }

    private string GetSortCriteriaString(PersonSortCriteria sortCriteria)
    {
      switch (sortCriteria)
      {
        default:
        case PersonSortCriteria.ById:
          return "by id";

        case PersonSortCriteria.ByFirstName:
          return "by first name";

        case PersonSortCriteria.ByLastName:
          return "by last name";

        case PersonSortCriteria.ByBirthDate:
          return "by birth date";
      }
    }

    private string GetSortDirectionString(SortDirection sortDirection)
    {
      switch (sortDirection)
      {
        default:
        case SortDirection.Ascending:
          return "ascending";

        case SortDirection.Descending:
          return "descending";
      }
    }
  }
}
