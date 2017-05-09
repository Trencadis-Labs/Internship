using System;
using System.Collections.Generic;

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

    public static void WriteSectionWithAction(string headingText, Action contentAction)
    {
      WriteHeadingStart(headingText);

      contentAction?.Invoke();

      WriteHeadingEnd();
    }

    public static void WriteSectionForQuery<T>(string headingText, IEnumerable<T> queryData, Action<T> elementAction)
    {
      WriteHeadingStart(headingText);

      if(queryData != null)
      {
        foreach(var element in queryData)
        {
          elementAction?.Invoke(element);
        }
      }

      WriteHeadingEnd();
    }

    public static void WriteSectionForData<T>(string headingText, T data, Action<T> dataAction)
    {
      WriteHeadingStart(headingText);

      dataAction?.Invoke(data);

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
