namespace ArrayHelper
{
  public class SByteSumCalculator : SumCalculator<sbyte>
  {
    public override sbyte Sum(sbyte a, sbyte b)
    {
      return (sbyte)(a + b);
    }
  }
}
