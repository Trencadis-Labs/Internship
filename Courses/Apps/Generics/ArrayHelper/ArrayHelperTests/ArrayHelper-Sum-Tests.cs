using ArrayHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ArrayHelperTests
{
  public partial class ArrayHelperTests
  {
    // both arrays null, calculator = null
    [TestMethod]
    public void Sum_Test_When_Array1_Is_Null_And_Array2_Is_Null_And_Calculator_Is_Null()
    {
      int[] array1 = null;
      int[] array2 = null;
      ISumCalculator<int> calculator = null;

      var result = ArrayHelper.ArrayHelper.Sum(array1, array2, calculator);

      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }

    // array1 = empty, array2 = null, calculator = null
    [TestMethod]
    public void Sum_Test_When_Array1_Is_Empty_And_Array2_Is_Null_And_Calculator_Is_Null()
    {
      int[] array1 = new int[0];
      int[] array2 = null;
      ISumCalculator<int> calculator = null;

      var result = ArrayHelper.ArrayHelper.Sum(array1, array2, calculator);

      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }

    // array1 = null, array2 = empty, calculator = null
    [TestMethod]
    public void Sum_Test_When_Array1_Is_Null_And_Array2_Is_Empty_And_Calculator_Is_Null()
    {
      int[] array1 = null;
      int[] array2 = new int[0];
      ISumCalculator<int> calculator = null;

      var result = ArrayHelper.ArrayHelper.Sum(array1, array2, calculator);

      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }

    // array1 = empty, array2 = empty, calculator = null
    [TestMethod]
    public void Sum_Test_When_Array1_Is_Empty_And_Array2_Is_Empty_And_Calculator_Is_Null()
    {
      int[] array1 = new int[0];
      int[] array2 = new int[0];
      ISumCalculator<int> calculator = null;

      var result = ArrayHelper.ArrayHelper.Sum(array1, array2, calculator);

      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }

    // array1 = [...], array2 = null, calculator = null
    [TestMethod]
    public void Sum_Test_When_Array1_Has_Elements_And_Array2_Is_Null_And_Calculator_Is_Null()
    {
      int[] array1 = new int[] { 1, 2, 3 };
      int[] array2 = null;
      ISumCalculator<int> calculator = null;

      Assert.ThrowsException<ArgumentException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.Sum(array1, array2, calculator);
        });
    }

    // array1 = [...], array2 = empty, calculator = null
    [TestMethod]
    public void Sum_Test_When_Array1_Has_Elements_And_Array2_Is_Empty_And_Calculator_Is_Null()
    {
      int[] array1 = new int[] { 1, 2, 3 };
      int[] array2 = new int[0];
      ISumCalculator<int> calculator = null;

      Assert.ThrowsException<ArgumentException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.Sum(array1, array2, calculator);
        });
    }

    // array1 = null, array2 = [...], calculator = null
    [TestMethod]
    public void Sum_Test_When_Array1_Is_Null_And_Array2_Has_Elements_And_Calculator_Is_Null()
    {
      int[] array1 = null;
      int[] array2 = new int[] { 1, 2, 3 };
      ISumCalculator<int> calculator = null;

      Assert.ThrowsException<ArgumentException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.Sum(array1, array2, calculator);
        });
    }

    // array1 = empty, array2 = [...], calculator = null
    [TestMethod]
    public void Sum_Test_When_Array1_Is_Empty_And_Array2_Has_Elements_And_Calculator_Is_Null()
    {
      int[] array1 = new int[0];
      int[] array2 = new int[] { 1, 2, 3 };
      ISumCalculator<int> calculator = null;

      Assert.ThrowsException<ArgumentException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.Sum(array1, array2, calculator);
        });
    }

    [TestMethod]
    public void Sum_Test_When_Array1_Has_Elements_And_Array2_Has_Elements_And_Calculator_Is_Null_But_Arrays_Size_Are_Mismatched()
    {
      int[] array1 = new int[] { 1, 2 };
      int[] array2 = new int[] { 1, 2, 3 };
      ISumCalculator<int> calculator = null;

      Assert.ThrowsException<ArgumentException>(
        () =>
        {
          var result = ArrayHelper.ArrayHelper.Sum(array1, array2, calculator);
        });
    }

    [TestMethod]
    public void Sum_Test_When_Array1_Has_Elements_And_Array2_Has_Elements_And_Calculator_Is_Null_And_Arrays_Size_Are_Matching()
    {
      int[] array1 = new int[] { 3, 2, 1 };
      int[] array2 = new int[] { 1, 2, 3 };
      ISumCalculator<int> calculator = null;

      var result = ArrayHelper.ArrayHelper.Sum(array1, array2, calculator);

      CollectionAssert.AreEqual(new[] { 4, 4, 4 }, result);
    }

    [TestMethod]
    public void Sum_Test_When_Array1_Has_Elements_And_Array2_Has_Elements_And_Calculator_Is_Not_Null_And_Arrays_Size_Are_Matching()
    {
      int[] array1 = new int[] { 3, 2, 1 };
      int[] array2 = new int[] { 1, 2, 3 };
      ISumCalculator<int> calculator = new IntSumCalculator();

      var result = ArrayHelper.ArrayHelper.Sum(array1, array2, calculator);

      CollectionAssert.AreEqual(new[] { 4, 4, 4 }, result);
    }
  }
}
