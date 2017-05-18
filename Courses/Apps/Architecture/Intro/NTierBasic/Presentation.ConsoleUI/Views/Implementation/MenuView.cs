using Models;
using Models.Core;
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
    private static class Options
    {
      public const string FirstPage = "first-pg";
      public const string PreviousPage = "prev-pg";
      public const string NextPage = "next-pg";
      public const string LastPage = "last-pg";
      public const string SortByIdAsc = "sort-id-asc";
      public const string SortByIdDesc = "sort-id-desc";
      public const string SortByFirstNameAsc = "sort-first-name-asc";
      public const string SortByFirstNameDesc = "sort-first-name-desc";
      public const string SortByLastNameAsc = "sort-last-name-asc";
      public const string SortByLastNameDesc = "sort-last-name-desc";
      public const string SortByBirthDateAsc = "sort-birth-date-asc";
      public const string SortByBirthDateDesc = "sort-birth-date-desc";
      public const string Exit = "exit";
    }

    public event EventHandler OnViewEvent;

    public void Render(SortedPagedCollection<Person, PersonSortCriteria> data)
    {
      this.RenderMenuOptions(data);

      var option = this.WaitForUserOptionInput();

      this.HandleUserOption(option);
    }

    private void RenderMenuOptions(SortedPagedCollection<Person, PersonSortCriteria> data)
    {
      var optionsBuilder = new StringBuilder();

      optionsBuilder.AppendLine("Use one of the following options to continue:");

      optionsBuilder.AppendLine($"- To go to first page: type '{Options.FirstPage}'");

      if (data.CanGoPreviousPage())
      {
        optionsBuilder.AppendLine($"- To go to previous page: type '{Options.PreviousPage}'");
      }

      if (data.CanGoNextPage())
      {
        optionsBuilder.AppendLine($"- To go to next page: type '{Options.NextPage}'");
      }

      optionsBuilder.AppendLine($"- To go to last page: type '{Options.LastPage}'");

      if ((data.SortCriteria != PersonSortCriteria.ById) || ((data.SortCriteria == PersonSortCriteria.ById) && (data.SortDirection == SortDirection.Descending)))
      {
        optionsBuilder.AppendLine($"- To change sort to be by person's Id ascending: type '{Options.SortByIdAsc}'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ById) || ((data.SortCriteria == PersonSortCriteria.ById) && (data.SortDirection == SortDirection.Ascending)))
      {
        optionsBuilder.AppendLine($"- To change sort to be by person's Id descending: type '{Options.SortByIdDesc}'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ByFirstName) || ((data.SortCriteria == PersonSortCriteria.ByFirstName) && (data.SortDirection == SortDirection.Descending)))
      {
        optionsBuilder.AppendLine($"- To change sort to be by person's FistName ascending: type '{Options.SortByFirstNameAsc}'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ByFirstName) || ((data.SortCriteria == PersonSortCriteria.ByFirstName) && (data.SortDirection == SortDirection.Ascending)))
      {
        optionsBuilder.AppendLine($"- To change sort to be by person's FistName descending: type '{Options.SortByFirstNameDesc}'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ByLastName) || ((data.SortCriteria == PersonSortCriteria.ByLastName) && (data.SortDirection == SortDirection.Descending)))
      {
        optionsBuilder.AppendLine($"- To change sort to be by person's LastName ascending: type '{Options.SortByLastNameAsc}'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ByLastName) || ((data.SortCriteria == PersonSortCriteria.ByLastName) && (data.SortDirection == SortDirection.Ascending)))
      {
        optionsBuilder.AppendLine($"- To change sort to be by person's LastName descending: type '{Options.SortByLastNameDesc}'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ByBirthDate) || ((data.SortCriteria == PersonSortCriteria.ByBirthDate) && (data.SortDirection == SortDirection.Descending)))
      {
        optionsBuilder.AppendLine($"- To change sort to be by person's BirthDate ascending: type '{Options.SortByBirthDateAsc}'");
      }

      if ((data.SortCriteria != PersonSortCriteria.ByBirthDate) || ((data.SortCriteria == PersonSortCriteria.ByBirthDate) && (data.SortDirection == SortDirection.Ascending)))
      {
        optionsBuilder.AppendLine($"- To change sort to be by person's BirthDate descending: type '{Options.SortByBirthDateDesc}'");
      }

      optionsBuilder.AppendLine($"- To exit: type '{Options.Exit}'");

      optionsBuilder.Append("Your option is = ");

      Console.Write(optionsBuilder.ToString());
    }

    private string WaitForUserOptionInput()
    {
      var option = Console.ReadLine();

      return option;
    }

    private void HandleUserOption(string option)
    {
      StringSwitch
        .On(option, StringComparison.OrdinalIgnoreCase)
        // paging
        .Case(
              Options.FirstPage,
              () => this.OnViewEvent?.Invoke(this, new GoToFirstPageEventArgs()))
        .Case(
              Options.PreviousPage,
              () => this.OnViewEvent?.Invoke(this, new GoToPrevPageEventArgs()))
        .Case(
              Options.NextPage,
              () => this.OnViewEvent?.Invoke(this, new GoToNextPageEventArgs()))
        .Case(
              Options.LastPage,
              () => this.OnViewEvent?.Invoke(this, new GoToLastPageEventArgs()))
        // sorting
        .Case(
              Options.SortByIdAsc,
              () => this.OnViewEvent?.Invoke(this, new SortByPersonPropertiesEventArgs(PersonSortCriteria.ById, SortDirection.Ascending)))
        .Case(
              Options.SortByIdDesc,
              () => this.OnViewEvent?.Invoke(this, new SortByPersonPropertiesEventArgs(PersonSortCriteria.ById, SortDirection.Descending)))
        .Case(
              Options.SortByFirstNameAsc,
              () => this.OnViewEvent?.Invoke(this, new SortByPersonPropertiesEventArgs(PersonSortCriteria.ByFirstName, SortDirection.Ascending)))
        .Case(
              Options.SortByFirstNameDesc,
              () => this.OnViewEvent?.Invoke(this, new SortByPersonPropertiesEventArgs(PersonSortCriteria.ByFirstName, SortDirection.Descending)))
        .Case(
              Options.SortByLastNameAsc,
              () => this.OnViewEvent?.Invoke(this, new SortByPersonPropertiesEventArgs(PersonSortCriteria.ByLastName, SortDirection.Ascending)))
        .Case(
              Options.SortByLastNameDesc,
              () => this.OnViewEvent?.Invoke(this, new SortByPersonPropertiesEventArgs(PersonSortCriteria.ByLastName, SortDirection.Descending)))
        .Case(
              Options.SortByBirthDateAsc,
              () => this.OnViewEvent?.Invoke(this, new SortByPersonPropertiesEventArgs(PersonSortCriteria.ByBirthDate, SortDirection.Ascending)))
        .Case(
              Options.SortByBirthDateDesc,
              () => this.OnViewEvent?.Invoke(this, new SortByPersonPropertiesEventArgs(PersonSortCriteria.ByBirthDate, SortDirection.Descending)))
        // exit
        .Case(
              Options.Exit,
              () => this.OnViewEvent?.Invoke(this, new ExitEventArgs()))
        // default
        .Default(() => this.OnViewEvent?.Invoke(this, new UnknownCommandEventArgs(option)))
        .Evaluate();
    }
  }
}
