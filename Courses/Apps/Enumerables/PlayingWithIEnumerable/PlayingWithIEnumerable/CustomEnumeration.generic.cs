using System.Collections;
using System.Collections.Generic;

namespace PlayingWithIEnumerable
{
  public class CustomEnumeration<T> : IEnumerable<T>
  {
    private readonly T[] backingArray;

    public CustomEnumeration(params T[] elements)
    {
      if (elements == null)
      {
        elements = new T[0];
      }

      this.backingArray = new T[elements.Length];
      for (int i = 0; i < elements.Length; i++)
      {
        this.backingArray[i] = elements[i];
      }
    }

    public IEnumerator<T> GetEnumerator()
    {
      return this.GetGenericEnumerator_UsingCustomArrayEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator_UsingCustomArrayEnumerator();
    }

    // generic enumerators

    private IEnumerator<T> GetGenericEnumerator_UsingForeach()
    {
      // Magic happens here!!
      DebuggingTool.DebugMsg($"CustomEnumeration<T> :: Called GetEnumerator (generic)");

      foreach (var item in this.backingArray)
      {
        DebuggingTool.DebugMsg($"CustomEnumeration<T> :: New foreach iteration (generic):: current element is {item}");

        yield return item;
      }
    }

    private IEnumerator<T> GetGenericEnumerator_UsingCustomArrayEnumerator()
    {
      var iterator = new ArrayEnumerator<T>(this.backingArray);

      return iterator;
    }

    // non-generic enumerators

    private IEnumerator GetNongenericEnumerator_UsingForeach()
    {
      // Magic happens here!!
      DebuggingTool.DebugMsg($"CustomEnumeration<T> :: Called GetEnumerator (non-generic)");

      foreach (object item in this.backingArray)
      {
        DebuggingTool.DebugMsg($"CustomEnumeration<T> :: New foreach iteration (non-generic) :: current element is {item}");

        yield return item;
      }
    }

    private IEnumerator GetNongenericEnumerator_UsingArrayClass()
    {
      // hides some types used internally (SZArrayEnumerator and ArrayEnumerator)
      // https://referencesource.microsoft.com/#mscorlib/system/array.cs,ac1786d68e16892e
      // https://referencesource.microsoft.com/#mscorlib/system/array.cs,4f49b6bfd66eb1e5
      return this.backingArray.GetEnumerator();
    }

    private IEnumerator GetEnumerator_UsingCustomArrayEnumerator()
    {
      var iterator = new ArrayEnumerator(this.backingArray);

      return iterator;
    }
  }
}
