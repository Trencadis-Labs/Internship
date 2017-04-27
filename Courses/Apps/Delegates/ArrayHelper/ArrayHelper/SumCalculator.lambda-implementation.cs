using System;

namespace ArrayHelper
{
  public class LambdaSumCalculator<T> : ISumCalculator<T>
  {
    private readonly Func<T, T, T> sumCalculation;

    public LambdaSumCalculator(Func<T, T, T> sumCalculation)
    {
      this.sumCalculation = sumCalculation ?? throw new ArgumentNullException(nameof(sumCalculation));
    }

    public T Sum(T a, T b)
    {
      return this.sumCalculation(a, b);
    }
  }
}
