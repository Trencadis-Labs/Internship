namespace ArrayHelper
{
  public abstract class SumCalculator<T> : ISumCalculator<T>
  {
    public abstract T Sum(T a, T b);
  }
}
