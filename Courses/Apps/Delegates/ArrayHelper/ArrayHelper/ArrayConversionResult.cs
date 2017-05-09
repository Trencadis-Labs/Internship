namespace ArrayHelper
{
  public class ArrayConversionResult<T, U>
  {
    public ArrayConversionResult(U[] convertedElements = null, T[] nonConvertibleElements = null)
    {
      this.ConvertedElements = convertedElements ?? new U[0];

      this.NonConvertibleElements = nonConvertibleElements ?? new T[0];
    }

    public U[] ConvertedElements { get; private set; }

    public T[] NonConvertibleElements { get; private set; }

    public static ArrayConversionResult<T, U> Empty()
    {
      return new ArrayConversionResult<T, U>();
    }
  }
}
