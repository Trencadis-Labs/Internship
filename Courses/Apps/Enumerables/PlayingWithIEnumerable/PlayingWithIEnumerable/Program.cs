using System;
using System.Collections;
using System.Collections.Generic;

namespace PlayingWithIEnumerable
{
  class Program
  {
    private static void IterateWithForeach<T>(IEnumerable<T> collection)
    {
      Console.WriteLine("IEnumerable<T> iteration using 'foreach'");

      foreach (var element in collection)
      {
        Console.WriteLine(element);
      }
    }

    private static void IterateManually<T>(IEnumerable<T> collection)
    {
      Console.WriteLine("IEnumerable<T> iteration manually, using IEnumerator<T>");

      using (var iterator = collection.GetEnumerator())
      {
        while (iterator.MoveNext())
        {
          Console.WriteLine(iterator.Current);
        }
      }
    }

    private static void IterateWithForeach_NonGeneric(IEnumerable collection)
    {
      Console.WriteLine("IEnumerable (non-generic) iteration using 'foreach'");

      foreach (var element in collection)
      {
        Console.WriteLine(element);
      }
    }

    private static void IterateManually_NonGeneric(IEnumerable collection)
    {
      Console.WriteLine("IEnumerable (non-generic) iteration manually, using IEnumerator");

      var iterator = collection.GetEnumerator();

      while (iterator.MoveNext())
      {
        Console.WriteLine(iterator.Current);
      }
    }

    static void Main(string[] args)
    {
      var customIntEnumeration = new CustomEnumeration<int>(1, 2, 3, 4);

      IterateWithForeach(customIntEnumeration);
      IterateManually(customIntEnumeration);

      IterateWithForeach_NonGeneric(customIntEnumeration);
      IterateManually_NonGeneric(customIntEnumeration);

      Console.WriteLine("Press any key to close ...");
      Console.ReadKey();
    }
  }
}
