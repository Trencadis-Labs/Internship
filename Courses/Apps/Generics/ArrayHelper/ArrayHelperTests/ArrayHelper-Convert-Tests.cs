using ArrayHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ArrayHelperTests
{
  public partial class ArrayHelperTests
  {
    [TestMethod]
    public void Convert_Test_When_Array_Is_Null_And_Converter_Is_Null()
    {
      int[] array = null;
      ICustomConverter<int, string> converter = null;

      var result = ArrayHelper.ArrayHelper.Convert(array, converter);

      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void Convert_Test_When_Array_Is_Empty_And_Converter_Is_Null()
    {
      int[] array = new int[0];
      ICustomConverter<int, string> converter = null;

      var result = ArrayHelper.ArrayHelper.Convert(array, converter);

      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void Convert_Test_When_Array_Has_Elements_And_Converter_Is_Null_And_Elements_Are_Type_Compatible_And_Value_Convertible()
    {
      int[] array = new int[] { 1, 2, 3 };
      ICustomConverter<int, string> converter = null;

      var result = ArrayHelper.ArrayHelper.Convert(array, converter);

      CollectionAssert.AreEqual(new[] { "1", "2", "3" }, result);
    }

    [TestMethod]
    public void Convert_Test_When_Array_Has_Elements_And_Converter_Is_Null_And_Elements_Are_Not_Type_Compatible()
    {
      string[] array = new string[] { "1", "2", "3" };
      ICustomConverter<string, int> converter = null;

      var result = ArrayHelper.ArrayHelper.Convert(array, converter);

      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }


    [TestMethod]
    public void Convert_Test_When_Array_Has_Elements_And_Converter_Is_Not_Null_And_Elements_Are_Type_Compatible_And_Value_Convertible()
    {
      string[] array = new string[] { "1", "2", "3" };
      ICustomConverter<string, int> converter = new StringToIntCustomConverter();

      var result = ArrayHelper.ArrayHelper.Convert(array, converter);

      CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result);
    }

    [TestMethod]
    public void Convert_Test_When_Array_Has_Elements_And_Converter_Is_Not_Null_And_Elements_Are_Type_Compatible_But_Not_All_Values_Are_Convertible()
    {
      string[] array = new string[] { "1", "2", "bla bla" };
      string[] nonConvertibleStrings;

      ICustomConverter<string, int> converter = new StringToIntCustomConverter();

      var result = ArrayHelper.ArrayHelper.Convert(array, converter, out nonConvertibleStrings);

      CollectionAssert.AreEqual(new[] { 1, 2 }, result);

      CollectionAssert.AreEqual(new[] { "bla bla" }, nonConvertibleStrings);
    }
  }
}
