using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArrayHelperTests
{
  public partial class ArrayHelperTests
  {
    [TestMethod]
    public void SortDescending_Test_When_Input_Array_Is_Null()
    {
      int[] array = null;

      var result = ArrayHelper.ArrayHelper.SortDescending(array);

      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void SortDescending_Test_When_Input_Array_Is_Empty()
    {
      int[] array = new int[0];

      var result = ArrayHelper.ArrayHelper.SortDescending(array);

      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }

    [TestMethod]
    public void SortDescending_Test_When_Input_Array_Has_Elements()
    {
      int[] array = new int[] { 1, 2, 3, 4, 5};

      var result = ArrayHelper.ArrayHelper.SortDescending(array);

      Assert.IsNotNull(result);
      Assert.AreEqual(array.Length, result.Length);
      CollectionAssert.AreEqual(new int[] { 5, 4, 3, 2, 1 }, result);
    }
  }
}
