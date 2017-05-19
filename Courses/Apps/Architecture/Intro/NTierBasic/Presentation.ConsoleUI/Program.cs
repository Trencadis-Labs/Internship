using System;

namespace Presentation.ConsoleUI
{
  class Program
  {
    static void Main(string[] args)
    {
      PersonUi ui = new PersonUi(10);

      ui.Start();
    }
  }
}