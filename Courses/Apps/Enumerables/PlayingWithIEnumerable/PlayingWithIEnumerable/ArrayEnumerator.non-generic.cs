using System;
using System.Collections;

namespace PlayingWithIEnumerable
{
  public class ArrayEnumerator : IEnumerator
  {
    private readonly Array array;
    private int startIndex;
    private int endIndex;
    private int currentIndex;

    public ArrayEnumerator(Array array)
    {
      if (array == null)
      {
        throw new ArgumentNullException(nameof(array));
      }

      int rank = array.Rank;
      if (rank != 1)
      {
        throw new ArgumentException("Only unidimensional arrays are supported");
      }

      this.array = array;
      this.currentIndex = -1;
      this.startIndex = 0;
      this.endIndex = this.array.Length - 1;
    }

    public object Current
    {
      get
      {
        if ((this.currentIndex < this.startIndex) || (this.currentIndex > this.endIndex))
        {
          throw new IndexOutOfRangeException();
        }

        return this.array.GetValue(this.currentIndex);
      }
    }

    public bool MoveNext()
    {
      this.currentIndex++;

      DebuggingTool.DebugMsg($"ArrayEnumerator :: MoveNext :: NewIndex={this.currentIndex}");

      return (this.currentIndex <= this.endIndex);
    }

    public void Reset()
    {
      this.currentIndex = -1;

      DebuggingTool.DebugMsg($"ArrayEnumerator :: Reset :: NewIndex={this.currentIndex}");
    }
  }
}
