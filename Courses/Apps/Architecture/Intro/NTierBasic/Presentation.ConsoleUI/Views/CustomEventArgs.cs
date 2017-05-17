using System;

namespace Presentation.ConsoleUI.Views
{
  public class GoToFirstPageEventArgs : EventArgs
  {
    public GoToFirstPageEventArgs()
      : base()
    {

    }
  }

  public class GoToLastPageEventArgs : EventArgs
  {
    public GoToLastPageEventArgs()
      : base()
    {

    }
  }

  public class GoToPrevPageEventArgs : EventArgs
  {
    public GoToPrevPageEventArgs()
      : base()
    {

    }
  }

  public class GoToNextPageEventArgs : EventArgs
  {
    public GoToNextPageEventArgs()
      : base()
    {

    }
  }

  public class ExitEventArgs : EventArgs
  {
    public ExitEventArgs()
      : base()
    {

    }
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
