namespace ArrayHelper
{
  public class ULongIntSumCalculator : SumCalculator<ulong>
  {
    public override ulong Sum(ulong a, ulong b)
    {
      return a + b;
    }
  }
}
