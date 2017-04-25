using System.Collections;

namespace PlayingWithIEnumerable
{
  public class CustomEnumeration : IEnumerable
  {
    private readonly object[] backingArray;

    public CustomEnumeration(params object[] elements)
    {
      if (elements == null)
      {
        elements = new object[0];
      }

      this.backingArray = new object[elements.Length];
      for (int i = 0; i < elements.Length; i++)
      {
        this.backingArray[i] = elements[i];
      }
    }

    public IEnumerator GetEnumerator()
    {
      return this.GetEnumerator_UsingCustomArrayEnumerator();
    }

    private IEnumerator GetEnumerator_UsingForeach()
    {
      // Magic happens here!!
      DebuggingTool.DebugMsg($"CustomEnumeration :: Called GetEnumerator");

      foreach (var item in this.backingArray)
      {
        DebuggingTool.DebugMsg($"CustomEnumeration :: New foreach iteration :: current element is {item}");

        yield return item;
      }
    }

    private IEnumerator GetEnumerator_UsingArrayClass()
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
