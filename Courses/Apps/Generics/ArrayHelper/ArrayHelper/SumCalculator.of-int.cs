namespace ArrayHelper
{
  public class IntSumCalculator : SumCalculator<int>
  {
    public override int Sum(int a, int b)
    {
      return a + b;
    }
  }
}
