using Presentation.ConsoleUI.Views.Abstractions;
using System;

namespace Presentation.ConsoleUI.Views.Implementation
{
  public class UnknownCommandView : IView<string>
  {
    public void Render(string data)
    {
      Console.WriteLine($"Unknown command '{data}' ....");
      Console.WriteLine();
    }
  }
}
