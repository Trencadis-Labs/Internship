namespace ArrayHelper
{
  public class LongIntSumCalculator : SumCalculator<long>
  {
    public override long Sum(long a, long b)
    {
      return a + b;
    }
  }
}
