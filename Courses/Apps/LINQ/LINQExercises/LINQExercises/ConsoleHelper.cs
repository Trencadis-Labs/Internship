using System;
using System.Collections.Generic;
using System.Linq;

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

    public static void WriteSectionForCollection<T>(string headingText, IEnumerable<T> collection, Action<T> elementAction, Action nullOrEmptyCollectionAction = null)
    {
      WriteHeadingStart(headingText);

      var collectionArray = collection?.ToArray();

      if ((collectionArray != null) && (collectionArray.Length > 0))
      {
        foreach(var element in collectionArray)
        {
          elementAction?.Invoke(element);
        }
      }
      else
      {
        nullOrEmptyCollectionAction?.Invoke();
      }

      WriteHeadingEnd();
    }

    public static void WriteSectionForData<T>(string headingText, T data, Action<T> action)
    {
      WriteHeadingStart(headingText);

      action?.Invoke(data);

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
