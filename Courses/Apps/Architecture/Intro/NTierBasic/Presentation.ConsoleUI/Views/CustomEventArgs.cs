using Models.Sorting;
using System;

namespace Presentation.ConsoleUI.Views
{
  public class GoToFirstPageEventArgs : EventArgs
  {
  }

  public class GoToLastPageEventArgs : EventArgs
  {
  }

  public class GoToPrevPageEventArgs : EventArgs
  {
  }

  public class GoToNextPageEventArgs : EventArgs
  {
  }

  public abstract class SortEventArgs<TSort> : EventArgs
    where TSort : struct
  {
    public SortEventArgs(TSort criteria, SortDirection sortDirection)
    {
      this.SortCriteria = criteria;
      this.SortDirection = sortDirection;
    }

    public TSort SortCriteria
    {
      get;
      private set;
    }

    public SortDirection SortDirection
    {
      get;
      private set;
    }
  }

  public class SortByPersonPropertiesEventArgs : SortEventArgs<PersonSortCriteria>
  {
    public SortByPersonPropertiesEventArgs(PersonSortCriteria sortCriteria, SortDirection sortDirection)
      : base(sortCriteria, sortDirection)
    {
    }
  }

  public class ExitEventArgs : EventArgs
  {
  }

  public class UnknownCommandEventArgs : EventArgs
  {
    public UnknownCommandEventArgs(string command)
      : base()
    {
      this.Command = command;
    }

    public string Command { get; private set; }
  }
}
