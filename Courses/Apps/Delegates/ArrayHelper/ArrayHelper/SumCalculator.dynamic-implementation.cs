namespace ArrayHelper
{
  public class DynamicSumCalculator<T> : ISumCalculator<T>
  {
    public T Sum(T a, T b)
    {
      dynamic dynA = a;
      dynamic dynB = b;

      dynamic sum = dynA + dynB;

      return (T)sum;
    }
  }
}
