using BusinessLogic;
using Models;
using Models.Core;
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

    private readonly IView<SortedPagedCollection<Person, PersonSortCriteria>> personListingView;

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
      TypeSwitch.On(e.GetType())
          // paging
          .Case<GoToFirstPageEventArgs>(() => this.OnGoToFirstPageEvent())
          .Case<GoToPrevPageEventArgs>(() => this.OnGoToPrevPageEvent())
          .Case<GoToNextPageEventArgs>(() => this.OnGoToNextPageEvent())
          .Case<GoToLastPageEventArgs>(() => this.OnGoToLastPageEvent())
          // sorting
          .Case<SortByPersonPropertiesEventArgs>(() => this.OnSortEvent(
                                                        e.As<SortByPersonPropertiesEventArgs>().SortCriteria,
                                                        e.As<SortByPersonPropertiesEventArgs>().SortDirection))
          // exit
          .Case<ExitEventArgs>(() => this.OnExitEvent())
          // unknwon
          .Case<UnknownCommandEventArgs>(() => this.OnUnknownEvent(e.As<UnknownCommandEventArgs>()?.Command))
          .Evaluate();
    }

    private void OnGoToFirstPageEvent()
    {
      this.pageIndex = 0;

      this.LoadDataForCurrentPage();
    }

    private void OnGoToPrevPageEvent()
    {
      if (this.pageIndex > 0)
      {
        this.pageIndex--;
      }

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

    private void OnSortEvent(PersonSortCriteria criteria, SortDirection sortDirection)
    {
      this.sortCriteria = criteria;
      this.sortDirection = sortDirection;

      LoadDataForCurrentPage();
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

      if(this.pageIndex > data.GetLastPageIndex())
      {
        this.pageIndex = data.GetLastPageIndex();

        this.data = personBO.GetPersonsPaged(this.pageIndex, this.pageSize, this.sortCriteria, this.sortDirection);
      }

      this.personListingView.Render(this.data);

      this.menuView.Render(this.data);
    }
  }
}
