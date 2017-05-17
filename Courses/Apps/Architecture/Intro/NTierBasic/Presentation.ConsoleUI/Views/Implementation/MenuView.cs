using Models;
using Models.Extensions;
using Models.Paging;
using Models.Sorting;
using Presentation.ConsoleUI.Views.Abstractions;
using System;
using System.Text;

namespace Presentation.ConsoleUI.Views.Implementation
{
  public class MenuView : IEventPublishView<SortedPagedCollection<Person, PersonSortCriteria>>
  {
    public event EventHandler OnViewEvent;

    public void Render(SortedPagedCollection<Person, PersonSortCriteria> data)
    {
      var optionsBuilder = new StringBuilder();

      optionsBuilder.AppendLine("Use one of the following options to continue:");

      optionsBuilder.AppendLine("- To go to first page: type 'first-pg'");

      if (data.CanGoPreviousPage())
      {
        optionsBuilder.AppendLine("- To go to previous page: type 'prev-pg'");
      }

      if (data.CanGoNextPage())
      {
        optionsBuilder.AppendLine("- To go to next page: type 'next-pg'");
      }

      optionsBuilder.AppendLine("- To go to last page: type 'last-pg'");

      if ((data.SortCriteria != PersonSortCriteria.ById) || ((data.SortCriteria == PersonSortCriteria.ById) && (data.SortDirection == SortDirection.Descending)))
      {
        optionsBuilder.AppendLine("- To change sort to be by person's Id ascending: type 'sort-id-asc'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ById) || ((data.SortCriteria == PersonSortCriteria.ById) && (data.SortDirection == SortDirection.Ascending)))
      {
        optionsBuilder.AppendLine("- To change sort to be by person's Id descending: type 'sort-id-desc'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ByFirstName) || ((data.SortCriteria == PersonSortCriteria.ByFirstName) && (data.SortDirection == SortDirection.Descending)))
      {
        optionsBuilder.AppendLine("- To change sort to be by person's FistName ascending: type 'sort-first-name-asc'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ByFirstName) || ((data.SortCriteria == PersonSortCriteria.ByFirstName) && (data.SortDirection == SortDirection.Ascending)))
      {
        optionsBuilder.AppendLine("- To change sort to be by person's FistName descending: type 'sort-first-name-desc'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ByLastName) || ((data.SortCriteria == PersonSortCriteria.ByLastName) && (data.SortDirection == SortDirection.Descending)))
      {
        optionsBuilder.AppendLine("- To change sort to be by person's LastName ascending: type 'sort-last-name-asc'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ByLastName) || ((data.SortCriteria == PersonSortCriteria.ByLastName) && (data.SortDirection == SortDirection.Ascending)))
      {
        optionsBuilder.AppendLine("- To change sort to be by person's LastName descending: type 'sort-last-name-desc'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ByBirthDate) || ((data.SortCriteria == PersonSortCriteria.ByBirthDate) && (data.SortDirection == SortDirection.Descending)))
      {
        optionsBuilder.AppendLine("- To change sort to be by person's BirthDate ascending: type 'sort-birth-date-asc'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ByBirthDate) || ((data.SortCriteria == PersonSortCriteria.ByBirthDate) && (data.SortDirection == SortDirection.Ascending)))
      {
        optionsBuilder.AppendLine("- To change sort to be by person's BirthDate descending: type 'sort-birth-date-desc'");
      }

      optionsBuilder.AppendLine("- To exit: type 'exit'");

      optionsBuilder.Append("Your option is = ");

      Console.Write(optionsBuilder.ToString());

      var option = Console.ReadLine();

      if (string.Equals(option, "first-pg", StringComparison.OrdinalIgnoreCase))
      {
        this.OnViewEvent?.Invoke(this, new GoToFirstPageEventArgs());
      }
      else if (string.Equals(option, "prev-pg", StringComparison.OrdinalIgnoreCase))
      {
        this.OnViewEvent?.Invoke(this, new GoToPrevPageEventArgs());
      }
      else if (string.Equals(option, "next-pg", StringComparison.OrdinalIgnoreCase))
      {
        this.OnViewEvent?.Invoke(this, new GoToNextPageEventArgs());
      }
      else if (string.Equals(option, "last-pg", StringComparison.OrdinalIgnoreCase))
      {
        this.OnViewEvent?.Invoke(this, new GoToLastPageEventArgs());
      }
      else if (string.Equals(option, "exit", StringComparison.OrdinalIgnoreCase))
      {
        this.OnViewEvent?.Invoke(this, new ExitEventArgs());
      }
      else
      {
        this.OnViewEvent?.Invoke(this, new UnknownCommandEventArgs(option));
      }
    }
  }
}
