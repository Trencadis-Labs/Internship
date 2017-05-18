using System;

namespace Models.Core
{
  public class TypeSwitch : CustomSwitch<Type>
  {
    private TypeSwitch(Type on)
      : base(on, (switchOn, caseLabel) => switchOn == caseLabel)
    {

    }

    public TypeSwitch Case(Type label, Action action)
    {
      this.AddCase(label, action);

      return this;
    }

    public TypeSwitch Case<TType>(Action action)
    {
      return this.Case(typeof(TType), action);
    }

    public TypeSwitch Default(Action action)
    {
      this.SetDefault(action);

      return this;
    }

    public static TypeSwitch On<TType>()
    {
      return TypeSwitch.On(typeof(TType));
    }

    public static TypeSwitch On(Type on)
    {
      return new TypeSwitch(on);
    }
  }
}
