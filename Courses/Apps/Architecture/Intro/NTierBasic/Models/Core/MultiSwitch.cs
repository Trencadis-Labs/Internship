using System;

namespace Models.Core
{
  public abstract class AbstractMultiSwitch<T1, T2> : CustomSwitch<Tuple<T1, T2>>
  {
    protected AbstractMultiSwitch(Tuple<T1, T2> on, Func<T1, T1, bool> comparison1, Func<T2, T2, bool> comparison2)
      : base(on: on, comparison: (tuple1, tuple2) => comparison1(tuple1.Item1, tuple2.Item1) && comparison2(tuple1.Item2, tuple2.Item2))
    {
    }
  }

  public class MultiSwitch<T1, T2> : AbstractMultiSwitch<T1, T2>
    where T1 : IEquatable<T1>
    where T2 : IEquatable<T2>
  {
    protected MultiSwitch(T1 on1, T2 on2)
      : base(on: new Tuple<T1, T2>(on1, on2), comparison1: (x, y) => ((IEquatable<T1>)x).Equals(y), comparison2: (x, y) => ((IEquatable<T2>)x).Equals(y))
    {

    }

    protected MultiSwitch(Tuple<T1, T2> on)
      : base(on: on, comparison1: (x, y) => ((IEquatable<T1>)x).Equals(y), comparison2: (x, y) => ((IEquatable<T2>)x).Equals(y))
    {

    }

    public MultiSwitch<T1, T2> Case(T1 label1, T2 label2, Action action)
    {
      this.AddCase(new Tuple<T1, T2>(label1, label2), action);

      return this;
    }

    public MultiSwitch<T1, T2> Case(Tuple<T1, T2> label, Action action)
    {
      this.AddCase(label, action);

      return this;
    }

    public MultiSwitch<T1, T2> Default(Action action)
    {
      this.SetDefault(action);

      return this;
    }

    public static MultiSwitch<T1, T2> On(T1 on1, T2 on2)
    {
      return new MultiSwitch<T1, T2>(on1, on2);
    }

    public static MultiSwitch<T1, T2> On(Tuple<T1, T2> on)
    {
      return new MultiSwitch<T1, T2>(on);
    }
  }

  public class ValueTypeMultiSwitch<T1, T2> : AbstractMultiSwitch<T1, T2>
    where T1 : struct
    where T2 : struct
  {
    protected ValueTypeMultiSwitch(T1 on1, T2 on2)
      : base(on: new Tuple<T1, T2>(on1, on2), comparison1: (x, y) => x.Equals(y), comparison2: (x, y) => x.Equals(y))
    {

    }

    protected ValueTypeMultiSwitch(Tuple<T1, T2> on)
      : base(on: on, comparison1: (x, y) => x.Equals(y), comparison2: (x, y) => x.Equals(y))
    {

    }

    public ValueTypeMultiSwitch<T1, T2> Case(T1 label1, T2 label2, Action action)
    {
      this.AddCase(new Tuple<T1, T2>(label1, label2), action);

      return this;
    }

    public ValueTypeMultiSwitch<T1, T2> Case(Tuple<T1, T2> label, Action action)
    {
      this.AddCase(label, action);

      return this;
    }

    public ValueTypeMultiSwitch<T1, T2> Default(Action action)
    {
      this.SetDefault(action);

      return this;
    }

    public static ValueTypeMultiSwitch<T1, T2> On(T1 on1, T2 on2)
    {
      return new ValueTypeMultiSwitch<T1, T2>(on1, on2);
    }

    public static ValueTypeMultiSwitch<T1, T2> On(Tuple<T1, T2> on)
    {
      return new ValueTypeMultiSwitch<T1, T2>(on);
    }
  }
}
