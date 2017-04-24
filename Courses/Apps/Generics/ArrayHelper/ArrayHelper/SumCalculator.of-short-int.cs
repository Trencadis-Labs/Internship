namespace ArrayHelper
{
  public class ShortIntSumCalculator : SumCalculator<short>
  {
    public override short Sum(short a, short b)
    {
      return (short)(a + b);
    }
  }
}
