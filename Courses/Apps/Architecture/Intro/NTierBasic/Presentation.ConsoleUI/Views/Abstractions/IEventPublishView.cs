using System;

namespace Presentation.ConsoleUI.Views.Abstractions
{
  public interface IEventPublishView<T> : IView<T>
  {
    event EventHandler OnViewEvent;
  }
}
