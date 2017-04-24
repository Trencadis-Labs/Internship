namespace ArrayHelper
{
  public class DynamicSumCalculator<T> : SumCalculator<T>
  {
    public override T Sum(T a, T b)
    {
      dynamic dynA = a;
      dynamic dynB = b;

      dynamic sum = dynA + dynB;

      return (T)sum;
    }
  }
}
