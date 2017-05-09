using System;

namespace ArrayHelper
{
  public class LambdaCustomConverter<T, U> : CustomConverter<T, U>
  {
    private readonly TryConvertValueDelegate<T, U> convertDelegate;

    public LambdaCustomConverter(TryConvertValueDelegate<T, U> convertDelegate)
    {
      this.convertDelegate = convertDelegate ?? throw new ArgumentNullException(nameof(convertDelegate));
    }

    public override bool TryConvertValue(T value, out U converted)
    {
      return this.convertDelegate(value, out converted);
    }
  }
}
