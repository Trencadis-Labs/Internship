namespace ArrayHelper
{
  public class FloatSumCalculator : SumCalculator<float>
  {
    public override float Sum(float a, float b)
    {
      return a + b;
    }
  }
}
