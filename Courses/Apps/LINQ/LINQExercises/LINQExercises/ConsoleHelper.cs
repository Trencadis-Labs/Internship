using System;

namespace LINQExercises
{
  public static class ConsoleHelper
  {
    public static void WriteHeadingStart(string headingText)
    {
      Console.WriteLine(new string('=', Console.WindowWidth - 1));
      Console.WriteLine(headingText);
      Console.WriteLine(new string('-', Console.WindowWidth - 1));
    }

    public static void WriteHeadingEnd()
    {
      Console.WriteLine(new string('=', Console.WindowWidth - 1));
      Console.WriteLine();
    }

    public static void WriteHeadingWithAction(string headingText, Action contentAction)
    {
      WriteHeadingStart(headingText);

      contentAction?.Invoke();

      WriteHeadingEnd();
    }

    public static void PromptForClose()
    {
      Console.WriteLine(new string('*', Console.WindowWidth - 1));
      Console.WriteLine("Press any key to close ...");
      Console.ReadKey();
    }
  }
}
