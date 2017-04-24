namespace ArrayHelper
{
  public class DecimalSumCalculator : SumCalculator<decimal>
  {
    public override decimal Sum(decimal a, decimal b)
    {
      return a + b;
    }
  }
}
