using System;
using System.Collections;
using System.Collections.Generic;

namespace PlayingWithIEnumerable
{
  public class ArrayEnumerator<T> : IEnumerator<T>
  {
    private T[] array;
    private int startIndex;
    private int endIndex;
    private int currentIndex;

    public ArrayEnumerator(T[] array)
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

    public T Current
    {
      get
      {
        if ((this.currentIndex < this.startIndex) || (this.currentIndex > this.endIndex))
        {
          throw new IndexOutOfRangeException();
        }

        return this.array[this.currentIndex];
      }
    }

    object IEnumerator.Current => this.Current;

    public void Dispose()
    {
      // good discussion here: http://stackoverflow.com/questions/232558/why-does-ienumeratort-inherit-from-idisposable-while-the-non-generic-ienumerat
      this.array = null;
    }

    public bool MoveNext()
    {
      this.currentIndex++;

      DebuggingTool.DebugMsg($"ArrayEnumerator<T> :: MoveNext :: NewIndex={this.currentIndex}");

      return (this.currentIndex <= this.endIndex);
    }

    public void Reset()
    {
      this.currentIndex = -1;

      DebuggingTool.DebugMsg($"ArrayEnumerator<T> :: Reset :: NewIndex={this.currentIndex}");
    }
  }
}
