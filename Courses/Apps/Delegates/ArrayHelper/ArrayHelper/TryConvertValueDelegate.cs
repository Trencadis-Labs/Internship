namespace ArrayHelper
{
  public delegate bool TryConvertValueDelegate<T, U>(T value, out U converted);
}
