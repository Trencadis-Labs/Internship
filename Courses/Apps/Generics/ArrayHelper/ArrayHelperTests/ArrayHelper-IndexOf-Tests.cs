using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArrayHelperTests
{
  public partial class ArrayHelperTests
  {
    [TestMethod]
    public void IndexOf_Test_When_Input_Array_Is_Null()
    {
      int[] array = null;

      var result = ArrayHelper.ArrayHelper.IndexOf(array, 1);

      Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void IndexOf_Test_When_Input_Array_Is_Empty()
    {
      int[] array = new int[0];

      var result = ArrayHelper.ArrayHelper.IndexOf(array, 1);

      Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void IndexOf_Test_When_Input_Array_Has_Elements_But_Lookup_Value_Has_No_Match()
    {
      int[] array = new int[] { 1, 2, 3, 4 };

      var result = ArrayHelper.ArrayHelper.IndexOf(array, 10);

      Assert.AreEqual(-1, result);
    }

    [TestMethod]
    public void IndexOf_Test_When_Input_Array_Has_Elements_And_Lookup_Value_Has_A_Match()
    {
      int[] array = new int[] { 1, 2, 3, 4 };

      var result = ArrayHelper.ArrayHelper.IndexOf(array, 3);

      Assert.AreEqual(2, result);
    }
  }
}
