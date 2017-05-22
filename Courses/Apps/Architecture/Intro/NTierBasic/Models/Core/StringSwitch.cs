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

  public class StringSwitch<TResult> : CustomSwitch<string, TResult>
  {
    private StringSwitch(string on, StringComparison stringComparison)
     : base(on, (switchOn, caseLabel) => string.Equals(switchOn, caseLabel, stringComparison))
    {
    }

    public StringSwitch<TResult> Case(string label, Func<TResult> returnFunc)
    {
      this.AddCase(label, returnFunc);

      return this;
    }

    public StringSwitch<TResult> MultiCase(string[] labels, Func<TResult> returnFunc)
    {
      if (labels != null)
      {
        foreach (var label in labels)
        {
          this.AddCase(label, returnFunc);
        }
      }

      return this;
    }

    public StringSwitch<TResult> Default(Func<TResult> returnFunc)
    {
      this.SetDefault(returnFunc);

      return this;
    }

    public static StringSwitch<TResult> On(string on)
    {
      return new StringSwitch<TResult>(on, StringComparison.CurrentCulture);
    }

    public static StringSwitch<TResult> On(string on, StringComparison stringComparison)
    {
      return new StringSwitch<TResult>(on, stringComparison);
    }
  }
}
