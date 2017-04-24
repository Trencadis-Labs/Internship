namespace ArrayHelper
{
  public class UIntSumCalculator : SumCalculator<uint>
  {
    public override uint Sum(uint a, uint b)
    {
      return a + b;
    }
  }
}
