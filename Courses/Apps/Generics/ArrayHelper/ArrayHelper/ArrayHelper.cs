using System;
using System.Collections.Generic;

namespace ArrayHelper
{
  public static class ArrayHelper
  {
    public static int GetLength<T>(T[] array)
    {
      if ((array == null) || (array.Length == 0))
      {
        return 0;
      }

      return array.Length;
    }

    public static int IndexOf<T>(T[] array, T element)
      where T : IEquatable<T>
    {
      if ((array == null) || (array.Length == 0))
      {
        return -1;
      }

      for (int i = 0; i < array.Length; i++)
      {
        var item = array[i];
        if ((item != null) && item.Equals(element))
        {
          return i;
        }
      }

      return -1;
    }

    public static T ElementAt<T>(T[] array, int index)
    {
      if (index < 0)
      {
        throw new IndexOutOfRangeException($"Negative index '{index}' is not allowed");
      }

      if (array == null)
      {
        throw new ArgumentNullException(nameof(array));
      }

      if (index >= array.Length)
      {
        int upperBound = array.Length > 0 ? array.Length - 1 : 0;

        throw new IndexOutOfRangeException($"Index '{index}' is outside the bounds of the array [0, {upperBound}]");
      }

      return array[index];
    }

    public static void PrintToConsole<T>(T[] array)
    {
      if (array == null)
      {
        array = new T[0];
      }

      var stringValues = new List<string>();
      foreach (var item in array)
      {
        if (item != null)
        {
          stringValues.Add(item.ToString());
        }
        else
        {
          stringValues.Add("[null]");
        }
      }

      Console.WriteLine("Array elements: " + string.Join(",", stringValues));
    }

    public static T[] SubArray<T>(T[] array, int index, int length)
    {
      if (index < 0)
      {
        throw new IndexOutOfRangeException($"Negative index '{index}' is not allowed");
      }

      if (length < 0)
      {
        throw new ArgumentException($"The sub-array length must be a positive numeric value");
      }

      if (array == null)
      {
        throw new ArgumentNullException(nameof(array));
      }

      int upperBound = array.Length > 0 ? array.Length - 1 : 0;

      if (index >= array.Length)
      {
        throw new IndexOutOfRangeException($"Index '{index}' is outside the bounds of the array [0, {upperBound}]");
      }

      if (index + length > array.Length)
      {
        throw new IndexOutOfRangeException($"The combination of start-index '{index}' and sub-array length {length} exceed the bounds of the array [0, {upperBound}]");
      }

      T[] result = new T[length];

      for (int i = index; i-index < length; i++)
      {
        result[i - index] = array[i];
      }

      return result;
    }

    public static T[] SortAscending<T>(T[] array)
      where T : IComparable<T>
    {
      if ((array == null) || (array.Length == 0))
      {
        return new T[0];
      }

      T[] sortedArray = ArrayHelper.SubArray(array, 0, array.Length);
      for (int i = 0; i < sortedArray.Length; i++)
      {
        for (int j = i + 1; j < sortedArray.Length; j++)
        {
          if (sortedArray[i].CompareTo(sortedArray[j]) > 0)
          {
            T aux = sortedArray[i];
            sortedArray[i] = sortedArray[j];
            sortedArray[j] = aux;
          }
        }
      }

      return sortedArray;
    }

    public static T[] SortDescending<T>(T[] array)
      where T : IComparable<T>
    {
      if ((array == null) || (array.Length == 0))
      {
        return new T[0];
      }

      T[] sortedArray = ArrayHelper.SubArray(array, 0, array.Length);
      for (int i = 0; i < sortedArray.Length; i++)
      {
        for (int j = i + 1; j < sortedArray.Length; j++)
        {
          if (sortedArray[i].CompareTo(sortedArray[j]) < 0)
          {
            T aux = sortedArray[i];
            sortedArray[i] = sortedArray[j];
            sortedArray[j] = aux;
          }
        }
      }

      return sortedArray;
    }

    public static T[] Sum<T>(T[] array1, T[] array2, ISumCalculator<T> sumCalculator = null)
    {
      if (sumCalculator == null)
      {
        sumCalculator = new DynamicSumCalculator<T>();
      }

      if (array1 == null)
      {
        array1 = new T[0];
      }

      if (array2 == null)
      {
        array2 = new T[0];
      }

      if (array1.Length != array2.Length)
      {
        throw new ArgumentException($"Array sizes must match, currently first array has {array1.Length} elements, while second array has {array2.Length} elements");
      }

      int commonLength = array1.Length;
      T[] result = new T[commonLength];
      for (int i = 0; i < commonLength; i++)
      {
        result[i] = sumCalculator.Sum(array1[i], array2[i]);
      }

      return result;
    }

    public static U[] Convert<T, U>(T[] array)
    {
      T[] nonConvertibleValues;
      return ArrayHelper.Convert<T, U>(array, null, out nonConvertibleValues);
    }

    public static U[] Convert<T, U>(T[] array, ICustomConverter<T, U> converter)
    {
      T[] nonConvertibleValues;
      return ArrayHelper.Convert<T, U>(array, converter, out nonConvertibleValues);
    }

    public static U[] Convert<T, U>(T[] array, out T[] nonConvertibleValues)
    {
      return ArrayHelper.Convert<T, U>(array, null, out nonConvertibleValues);
    }

    public static U[] Convert<T, U>(T[] array, ICustomConverter<T, U> converter, out T[] nonConvertibleValues)
    {
      nonConvertibleValues = new T[0];

      if ((array == null) || (array.Length == 0))
      {
        return new U[0];
      }

      if(converter == null)
      {
        converter = new CustomConverter<T, U>();
      }

      if(!converter.CanConvertBetweenTypes())
      {
        return new U[0];
      }

      var convertedValues = new List<U>();
      var failConvertValues = new List<T>();
      for (int i = 0; i< array.Length; i++)
      {
        T item = array[i];
        U convertedItem;
        if(converter.TryConvertValue(item, out convertedItem))
        {
          convertedValues.Add(convertedItem);
        }
        else
        {
          failConvertValues.Add(item);
        }
      }

      nonConvertibleValues = failConvertValues.ToArray();

      return convertedValues.ToArray();
    }
  }
}
