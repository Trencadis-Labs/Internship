using BusinessLogic;
using BusinessLogic.Abstractions;
using DataAccess.Abstractions;
using Models;
using Models.Core;
using Models.Extensions;
using Models.Paging;
using Models.Sorting;
using Presentation.ConsoleUI.Views;
using Presentation.ConsoleUI.Views.Abstractions;
using System;

namespace Presentation.ConsoleUI
{
  public class PersonUi
  {
    private readonly int pageSize;

    private readonly IPersonRepository personRepository;
    private readonly IPersonImageFileNameGenerator fileNameGenerator;
    private readonly IFileManager fileManager;

    private readonly IView<SortedPagedCollection<Person, PersonSortCriteria>> personListingView;

    private readonly IEventPublishView<SortedPagedCollection<Person, PersonSortCriteria>> menuView;

    private readonly IView<string> unknownCommandView;

    private int pageIndex;

    private PersonSortCriteria sortCriteria = PersonSortCriteria.ById;

    private SortDirection sortDirection = SortDirection.Ascending;

    private SortedPagedCollection<Person, PersonSortCriteria> data = null;

    public PersonUi(
      int pageSize,
      IPersonRepository personRepository,
      IPersonImageFileNameGenerator fileNameGenerator,
      IFileManager fileManager,
      IView<SortedPagedCollection<Person, PersonSortCriteria>> personListingView,
      IEventPublishView<SortedPagedCollection<Person, PersonSortCriteria>> menuView,
      IView<string> unknownCommandView)
    {
      if (pageSize <= 0)
      {
        throw new ArgumentException($"The value '{pageSize}' used for page size is invalid, it must be a strictly positive numeric value");
      }

      this.pageSize = pageSize;

      this.personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
      this.fileNameGenerator = fileNameGenerator ?? throw new ArgumentNullException(nameof(fileNameGenerator));
      this.fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));

      this.personListingView = personListingView ?? throw new ArgumentNullException($"{nameof(personListingView)}");

      this.menuView = menuView ?? throw new ArgumentNullException($"{nameof(menuView)}");

      this.unknownCommandView = unknownCommandView ?? throw new ArgumentNullException($"{nameof(unknownCommandView)}");
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
      var personBO = new PersonBusinessObject(this.personRepository, this.fileNameGenerator, this.fileManager);

      this.data = personBO.GetPersonsPaged(this.pageIndex, this.pageSize, this.sortCriteria, this.sortDirection);

      if (this.pageIndex > data.GetLastPageIndex())
      {
        this.pageIndex = data.GetLastPageIndex();

        this.data = personBO.GetPersonsPaged(this.pageIndex, this.pageSize, this.sortCriteria, this.sortDirection);
      }

      this.personListingView.Render(this.data);

      this.menuView.Render(this.data);
    }
  }
}
