using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ArrayHelperTests
{
  public partial class ArrayHelperTests
  {
    [TestMethod]
    public void ElementAt_Test_When_Input_Array_Is_Null_And_Index_Is_Negative()
    {
      int[] array = null;
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.ElementAt(array, -1);
        });
    }

    [TestMethod]
    public void ElementAt_Test_When_Input_Array_Is_Null_And_Index_Is_Zero()
    {
      int[] array = null;
      Assert.ThrowsException<ArgumentNullException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.ElementAt(array, 0);
        });
    }

    [TestMethod]
    public void ElementAt_Test_When_Input_Array_Is_Null_And_Index_Is_Positive()
    {
      int[] array = null;
      Assert.ThrowsException<ArgumentNullException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.ElementAt(array, 1);
        });
    }

    [TestMethod]
    public void ElementAt_Test_When_Input_Array_Is_Empty_And_Index_Is_Negative()
    {
      int[] array = new int[0];
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.ElementAt(array, -1);
        });
    }

    [TestMethod]
    public void ElementAt_Test_When_Input_Array_Is_Empty_And_Index_Is_Zero()
    {
      int[] array = new int[0];
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.ElementAt(array, 0);
        });
    }

    [TestMethod]
    public void ElementAt_Test_When_Input_Array_Is_Empty_And_Index_Is_Positive()
    {
      int[] array = new int[0];
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.ElementAt(array, 1);
        });
    }

    [TestMethod]
    public void ElementAt_Test_When_Input_Array_Has_Elements_And_Index_Is_Negative()
    {
      int[] array = new int[] { 1, 2, 3, 4 };
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.ElementAt(array, -1);
        });
    }

    [TestMethod]
    public void ElementAt_Test_When_Input_Array_Has_Elements_And_Index_Is_Zero()
    {
      int[] array = new int[] { 1, 2, 3, 4 };

      var result = ArrayHelper.ArrayHelper.ElementAt(array, 0);

      Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void ElementAt_Test_When_Input_Array_Has_Elements_And_Index_Is_Positive_And_Lower_Than_Array_Size()
    {
      int[] array = new int[] { 1, 2, 3, 4 };

      var result = ArrayHelper.ArrayHelper.ElementAt(array, 2);

      Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void ElementAt_Test_When_Input_Array_Has_Elements_And_Index_Is_Positive_But_Higher_Than_Array_Size()
    {
      int[] array = new int[] { 1, 2, 3, 4 };
      Assert.ThrowsException<IndexOutOfRangeException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.ElementAt(array, 100);
        });
    }
  }
}
