using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArrayHelperTests
{
  [TestClass]
  public partial class ArrayHelperTests
  {
    [TestMethod]
    public void GetLength_Test_When_Input_Array_Is_Null()
    {
      int[] array = null;

      var result = ArrayHelper.ArrayHelper.GetLength(array);

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetLength_Test_When_Input_Array_Is_Empty()
    {
      int[] array = new int[0];

      var result = ArrayHelper.ArrayHelper.GetLength(array);

      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetLength_Test_When_Input_Array_Has_Elements()
    {
      int[] array = new int[] { 1, 2, 3, 4, 5 };

      var result = ArrayHelper.ArrayHelper.GetLength(array);

      Assert.AreEqual(array.Length, result);
    }
  }
}
