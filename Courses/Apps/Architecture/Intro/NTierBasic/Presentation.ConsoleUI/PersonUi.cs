using BusinessLogic;
using Models;
using Models.Extensions;
using Models.Paging;
using Models.Sorting;
using Presentation.ConsoleUI.Views;
using Presentation.ConsoleUI.Views.Abstractions;
using Presentation.ConsoleUI.Views.Implementation;
using System;

namespace Presentation.ConsoleUI
{
  public class PersonUi
  {
    private readonly int pageSize;

    private readonly IView<PagedCollection<Person>> personListingView;

    private readonly IEventPublishView<SortedPagedCollection<Person, PersonSortCriteria>> menuView;

    private readonly IView<string> unknownCommandView;

    private int pageIndex;

    private PersonSortCriteria sortCriteria = PersonSortCriteria.ById;

    private SortDirection sortDirection = SortDirection.Ascending;

    private SortedPagedCollection<Person, PersonSortCriteria> data = null;

    public PersonUi(int pageSize)
    {
      this.personListingView = new PersonListingView();

      this.menuView = new MenuView();

      this.unknownCommandView = new UnknownCommandView();

      this.pageSize = pageSize;
    }

    public void Start()
    {
      this.menuView.OnViewEvent += MenuView_OnViewEvent;

      this.sortCriteria = PersonSortCriteria.ById;

      this.sortDirection = SortDirection.Ascending;

      this.OnGoToFirstPageEvent();
    }

    public void Stop()
    {
      this.menuView.OnViewEvent -= MenuView_OnViewEvent;
    }

    private void MenuView_OnViewEvent(object sender, EventArgs e)
    {
      if (e.GetType() == typeof(GoToFirstPageEventArgs))
      {
        this.OnGoToFirstPageEvent();
      }
      else if (e.GetType() == typeof(GoToPrevPageEventArgs))
      {
        this.OnGoToPrevPageEvent();
      }
      else if (e.GetType() == typeof(GoToNextPageEventArgs))
      {
        this.OnGoToNextPageEvent();
      }
      else if (e.GetType() == typeof(GoToLastPageEventArgs))
      {
        this.OnGoToLastPageEvent();
      }
      else if (e.GetType() == typeof(ExitEventArgs))
      {
        this.OnExitEvent();
      }
      else if (e.GetType() == typeof(UnknownCommandEventArgs))
      {
        var evArgs = e as UnknownCommandEventArgs;

        this.OnUnknownEvent(evArgs?.Command);
      }
    }

    private void OnGoToFirstPageEvent()
    {
      this.pageIndex = 0;

      this.LoadDataForCurrentPage();
    }

    private void OnGoToPrevPageEvent()
    {
      this.pageIndex--;

      this.LoadDataForCurrentPage();
    }

    private void OnGoToNextPageEvent()
    {
      this.pageIndex++;

      this.LoadDataForCurrentPage();
    }

    private void OnGoToLastPageEvent()
    {
      this.pageIndex = this.data.GetLastPageIndex();

      this.LoadDataForCurrentPage();
    }

    private void OnExitEvent()
    {
      this.Stop();
    }

    private void OnUnknownEvent(string command)
    {
      this.unknownCommandView.Render(command);

      this.menuView.Render(this.data);
    }

    private void LoadDataForCurrentPage()
    {
      var personBO = new PersonBusinessObject();

      this.data = personBO.GetPersonsPaged(this.pageIndex, this.pageSize, this.sortCriteria, this.sortDirection);

      if (this.pageIndex == int.MaxValue)
      {
        this.pageIndex = this.data.TotalPagesCount - 1;
      }

      this.personListingView.Render(this.data);

      this.menuView.Render(this.data);
    }
  }
}
