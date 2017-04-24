using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ArrayHelperTests
{
  public partial class ArrayHelperTests
  {
    #region Array null

    // Array null, Length negative, vary index

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Null_And_Index_Is_Negative_And_Length_Is_Negative()
    {
      int[] array = null;
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, -1, -1);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Null_And_Index_Is_Zero_And_Length_Is_Negative()
    {
      int[] array = null;
      Assert.ThrowsException<ArgumentException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 0, -1);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Null_And_Index_Is_Positive_And_Length_Is_Negative()
    {
      int[] array = null;
      Assert.ThrowsException<ArgumentException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 1, -1);
        });
    }

    // Array null, Length zero, vary index

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Null_And_Index_Is_Negative_And_Length_Is_Zero()
    {
      int[] array = null;
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, -1, 0);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Null_And_Index_Is_Zero_And_Length_Is_Zero()
    {
      int[] array = null;
      Assert.ThrowsException<ArgumentNullException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 0, 0);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Null_And_Index_Is_Positive_And_Length_Is_Zero()
    {
      int[] array = null;
      Assert.ThrowsException<ArgumentNullException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 1, 0);
        });
    }

    // Array null, Length positive, vary index

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Null_And_Index_Is_Negative_And_Length_Is_Positive()
    {
      int[] array = null;
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, -1, 2);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Null_And_Index_Is_Zero_And_Length_Is_Positive()
    {
      int[] array = null;
      Assert.ThrowsException<ArgumentNullException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 0, 2);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Null_And_Index_Is_Positive_And_Length_Is_Positive()
    {
      int[] array = null;
      Assert.ThrowsException<ArgumentNullException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 1, 2);
        });
    }

    #endregion

    #region Array is empty

    // Array empty, Length negative, vary index

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Empty_And_Index_Is_Negative_And_Length_Is_Negative()
    {
      int[] array = new int[0];
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, -1, -1);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Empty_And_Index_Is_Zero_And_Length_Is_Negative()
    {
      int[] array = new int[0];
      Assert.ThrowsException<ArgumentException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 0, -1);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Empty_And_Index_Is_Positive_And_Length_Is_Negative()
    {
      int[] array = new int[0];
      Assert.ThrowsException<ArgumentException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 1, -1);
        });
    }

    // Array empty, Length zero, vary index

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Empty_And_Index_Is_Negative_And_Length_Is_Zero()
    {
      int[] array = new int[0];
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, -1, 0);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Empty_And_Index_Is_Zero_And_Length_Is_Zero()
    {
      int[] array = new int[0];
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 0, 0);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Empty_And_Index_Is_Positive_And_Length_Is_Zero()
    {
      int[] array = new int[0];
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 1, 0);
        });
    }

    // Array empty, Length positive, vary index

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Empty_And_Index_Is_Negative_And_Length_Is_Positive()
    {
      int[] array = new int[0];
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, -1, 2);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Empty_And_Index_Is_Zero_And_Length_Is_Positive()
    {
      int[] array = new int[0];
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 0, 2);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Is_Empty_And_Index_Is_Positive_And_Length_Is_Positive()
    {
      int[] array = new int[0];
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 1, 2);
        });
    }

    #endregion

    #region Array has elements

    // Array has elements, Length negative, vary index

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Negative_And_Length_Is_Negative()
    {
      int[] array = new int[] { 1, 2, 3, 4 };
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, -1, -1);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Zero_And_Length_Is_Negative()
    {
      int[] array = new int[] { 1, 2, 3, 4 };
      Assert.ThrowsException<ArgumentException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 0, -1);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Positive_And_Length_Is_Negative()
    {
      int[] array = new int[] { 1, 2, 3, 4 };
      Assert.ThrowsException<ArgumentException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 1, -1);
        });
    }

    // Array has elements, Length zero, vary index

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Negative_And_Length_Is_Zero()
    {
      int[] array = new int[] { 1, 2, 3, 4 };
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, -1, 0);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Zero_And_Length_Is_Zero()
    {
      int[] array = new int[] { 1, 2, 3, 4 };

      var result = ArrayHelper.ArrayHelper.SubArray(array, 0, 0);

      Assert.IsNotNull(result);

      Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Positive_And_Length_Is_Zero()
    {
      int[] array = new int[] { 1, 2, 3, 4 };

      var result = ArrayHelper.ArrayHelper.SubArray(array, 1, 0);

      Assert.IsNotNull(result);

      Assert.AreEqual(0, result.Length);
    }

    // Array has elements, Length positive, vary index

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Negative_And_Length_Is_Positive()
    {
      int[] array = new int[] { 1, 2, 3, 4 };
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, -1, 2);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Zero_And_Length_Is_Positive_And_Less_Than_Array_Length()
    {
      int[] array = new int[] { 1, 2, 3, 4 };

      var result = ArrayHelper.ArrayHelper.SubArray(array, 0, 2);

      Assert.IsNotNull(result);
      Assert.AreEqual(2, result.Length);
      CollectionAssert.AreEqual(new int[] { 1, 2 }, result);
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Zero_And_Length_Is_Positive_And_With_Index_Reaches_Till_Array_Length()
    {
      int[] array = new int[] { 1, 2, 3, 4 };

      var result = ArrayHelper.ArrayHelper.SubArray(array, 0, array.Length);

      Assert.IsNotNull(result);
      Assert.AreEqual(array.Length, result.Length);
      CollectionAssert.AreEqual(array, result);
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Zero_And_Length_Is_Positive_And_With_Index_Exceeds_Array_Length()
    {
      int[] array = new int[] { 1, 2, 3, 4 };

      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 0, array.Length + 1);
        });
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Positive_And_Length_Is_Positive_And_Less_Than_Array_Length()
    {
      int[] array = new int[] { 1, 2, 3, 4 };

      var result = ArrayHelper.ArrayHelper.SubArray(array, 1, 2);

      Assert.IsNotNull(result);
      Assert.AreEqual(2, result.Length);
      CollectionAssert.AreEqual(new int[] { 2, 3 }, result);
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Positive_And_Length_Is_Positive_And_With_Index_Reaches_Till_Array_Length()
    {
      int[] array = new int[] { 1, 2, 3, 4 };

      var result = ArrayHelper.ArrayHelper.SubArray(array, 1, array.Length - 1);

      Assert.IsNotNull(result);
      Assert.AreEqual(array.Length - 1, result.Length);
      CollectionAssert.AreEqual(new int[] { 2, 3, 4 }, result);
    }

    [TestMethod]
    public void SubArray_Test_When_Input_Array_Has_Elements_And_Index_Is_Positive_And_Length_Is_Positive_And_With_Index_Exceeds_Till_Array_Length()
    {
      int[] array = new int[] { 1, 2, 3, 4 };

      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.SubArray(array, 1, array.Length);
        });
    }

    #endregion
  }
}
