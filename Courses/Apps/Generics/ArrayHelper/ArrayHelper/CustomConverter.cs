using System;
using System.ComponentModel;

namespace ArrayHelper
{
  public class CustomConverter<T, U> : ICustomConverter<T, U>
  {
    private readonly static U DefaultValue = default(U);

    protected virtual U ReturnForNonConvertibleValue(T nonConvertibleValue)
    {
      return CustomConverter<T, U>.DefaultValue;
    }

    public virtual bool CanConvertBetweenTypes()
    {
      TypeConverter converterT = TypeDescriptor.GetConverter(typeof(T));
      if (converterT.CanConvertTo(typeof(U)))
      {
        return true;
      }

      return false;
    }

    protected virtual bool TryConvertNull(T nullValue, out U convertedNullValue)
    {
      convertedNullValue = this.ReturnForNonConvertibleValue(nullValue);

      return true;
    }

    public virtual bool TryConvertValue(T value, out U converted)
    {
      if (value == null)
      {
        return this.TryConvertNull(value, out converted);
      }

      if (!this.CanConvertBetweenTypes())
      {
        converted = this.ReturnForNonConvertibleValue(value);

        return false;
      }

      try
      {
        object convertedObject = System.Convert.ChangeType(value, typeof(U));

        converted = (U)convertedObject;

        return true;
      }
      catch (Exception ex) when ((ex is InvalidCastException) ||
                                  (ex is FormatException) ||
                                  (ex is OverflowException))
      {
        converted = this.ReturnForNonConvertibleValue(value);

        return false;
      }
    }

    public virtual U Convert(T value)
    {
      U converted;
      this.TryConvertValue(value, out converted);

      return converted;
    }
  }
}
