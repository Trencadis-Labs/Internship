using System;

namespace Models.Core
{
  public class StringSwitch : CustomSwitch<string>
  {
    private StringSwitch(string on, StringComparison stringComparison)
      : base(on, (switchOn, caseLabel) => string.Equals(switchOn, caseLabel, stringComparison))
    {
    }

    public StringSwitch Case(string label, Action action)
    {
      this.AddCase(label, action);

      return this;
    }

    public StringSwitch Default(Action action)
    {
      this.SetDefault(action);

      return this;
    }

    public static StringSwitch On(string on)
    {
      return new StringSwitch(on, StringComparison.CurrentCulture);
    }

    public static StringSwitch On(string on, StringComparison stringComparison)
    {
      return new StringSwitch(on, stringComparison);
    }
  }
}
