namespace ArrayHelper
{
  public interface ICustomConverter<in T, U>
  {
    bool CanConvertBetweenTypes();

    bool TryConvertValue(T value, out U converted);

    U Convert(T value);
  }
}
