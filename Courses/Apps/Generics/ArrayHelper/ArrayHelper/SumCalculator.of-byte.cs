namespace ArrayHelper
{
  public class ByteSumCalculator : SumCalculator<byte>
  {
    public override byte Sum(byte a, byte b)
    {
      return (byte)(a + b);
    }
  }
}
