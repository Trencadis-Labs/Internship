namespace ArrayHelper
{
  public class DoubleSumCalculator : SumCalculator<double>
  {
    public override double Sum(double a, double b)
    {
      return a + b;
    }
  }
}
