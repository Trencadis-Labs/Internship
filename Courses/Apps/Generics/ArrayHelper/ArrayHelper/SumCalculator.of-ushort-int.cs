namespace ArrayHelper
{
  public class UShortIntSumCalculator : SumCalculator<ushort>
  {
    public override ushort Sum(ushort a, ushort b)
    {
      return (ushort)(a + b);
    }
  }
}
