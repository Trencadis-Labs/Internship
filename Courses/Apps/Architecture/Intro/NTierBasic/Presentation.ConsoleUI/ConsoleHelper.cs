using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.ConsoleUI
{
  public static class ConsoleHelper
  {
    public static void WriteHeadingStart(string headingText = "")
    {
      Console.WriteLine(new string('=', Console.WindowWidth - 1));

      if (!string.IsNullOrWhiteSpace(headingText))
      {
        Console.WriteLine(headingText);
        Console.WriteLine(new string('-', Console.WindowWidth - 1));
      }
    }

    public static void WriteHeadingEnd(string footerText = "")
    {
      if (!string.IsNullOrWhiteSpace(footerText))
      {
        Console.WriteLine(new string('-', Console.WindowWidth - 1));
        Console.WriteLine(footerText);
      }

      Console.WriteLine(new string('=', Console.WindowWidth - 1));
      Console.WriteLine();
    }

    public static void WriteSectionWithAction(string headingText, string footerText, Action contentAction)
    {
      WriteHeadingStart(headingText);

      contentAction?.Invoke();

      WriteHeadingEnd(footerText);
    }

    public static void WriteSectionForCollection<T>(string headingText, string footerText, IEnumerable<T> collection, Action<T> elementAction, Action nullOrEmptyCollectionAction = null)
    {
      WriteHeadingStart(headingText);

      var collectionArray = collection?.ToArray();

      if ((collectionArray != null) && (collectionArray.Length > 0))
      {
        foreach (var element in collectionArray)
        {
          elementAction?.Invoke(element);
        }
      }
      else
      {
        nullOrEmptyCollectionAction?.Invoke();
      }

      WriteHeadingEnd(footerText);
    }

    public static void WriteSectionForData<T>(string headingText, string footerText, T data, Action<T> action)
    {
      WriteHeadingStart(headingText);

      action?.Invoke(data);

      WriteHeadingEnd(footerText);
    }

    public static void PromptForClose()
    {
      Console.WriteLine(new string('*', Console.WindowWidth - 1));
      Console.WriteLine("Press any key to close ...");
      Console.ReadKey();
    }
  }
}
