namespace ArrayHelper
{
  public class BooleanSumCalculator : SumCalculator<bool>
  {
    public override bool Sum(bool a, bool b)
    {
      return a || b;
    }
  }
}
