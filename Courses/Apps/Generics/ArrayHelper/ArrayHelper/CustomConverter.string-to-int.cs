namespace ArrayHelper
{
  public class StringToIntCustomConverter : CustomConverter<string, int>
  {
    public override bool CanConvertBetweenTypes()
    {
      return true;
    }

    public override bool TryConvertValue(string value, out int converted)
    {
      return int.TryParse(value, out converted);
    }
  }
}
