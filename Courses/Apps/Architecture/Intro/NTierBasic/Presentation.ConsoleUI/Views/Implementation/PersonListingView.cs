using Models;
using Models.Extensions;
using Models.Paging;
using Presentation.ConsoleUI.Views.Abstractions;
using System;

namespace Presentation.ConsoleUI.Views.Implementation
{
  public class PersonListingView : IView<PagedCollection<Person>>
  {
    public void Render(PagedCollection<Person> data)
    {
      Console.Clear();

      ConsoleHelper.WriteSectionForCollection(
        $"Person listing (page {data.PageIndex + 1} of {data.TotalPagesCount})",
        "",
        data.Data,
        p => Console.WriteLine($"#{p.Id.ToFixedWidthString(5)} {p.FullName.ToFixedWidthString(25)} {p.DateOfBirth:yyyy-MM-dd}"),
        () => Console.WriteLine("(no persons)"));
    }
  }
}
