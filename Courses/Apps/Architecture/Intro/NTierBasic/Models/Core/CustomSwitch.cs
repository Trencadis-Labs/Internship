using System;
using System.Collections.Generic;

namespace Models.Core
{
  public abstract class CustomSwitch<T> : CustomSwitch<T, object>
  {
    protected CustomSwitch(T on, Func<T, T, bool> comparison)
      : base(on, comparison)
    {
    }

    protected virtual void AddCase(T label, Action action)
    {
      this.AddCase(
        label: label,
        returnFunc: () => { action?.Invoke(); return null; });
    }

    protected void SetDefault(Action action)
    {
      this.SetDefault(
        returnFunc: () => { action?.Invoke(); return null; });
    }

    private class CaseLabel
    {
      public CaseLabel(T label, Action action)
      {
        this.Label = label;
        this.Action = action;
      }

      public T Label { get; private set; }

      public Action Action { get; private set; }
    }
  }

  public abstract class CustomSwitch<T, TResult>
  {
    private readonly List<CaseLabel> caseLabels = new List<CaseLabel>();

    private Func<TResult> defaultFunc = null;

    private readonly T switchOn;

    private readonly Func<T, T, bool> comparison;

    protected CustomSwitch(T on, Func<T, T, bool> comparison)
    {
      if (comparison == null)
      {
        comparison = (switchOn, caseLabel) => false;
      }

      this.switchOn = on;
      this.comparison = comparison;
    }

    protected virtual void AddCase(T label, Func<TResult> returnFunc)
    {
      this.caseLabels.Add(new CaseLabel(label, returnFunc));
    }

    protected virtual void SetDefault(Func<TResult> returnFunc)
    {
      this.defaultFunc = returnFunc;
    }

    public TResult Evaluate()
    {
      foreach (var caseItem in this.caseLabels)
      {
        if (this.comparison(this.switchOn, caseItem.Label))
        {
          var func = caseItem.ReturnFunc;
          if (func != null)
          {
            return func.Invoke();
          }

          throw new InvalidOperationException();
        }
      }

      var defFunc = this.defaultFunc;
      if (defFunc != null)
      {
        return defFunc.Invoke();
      }

      throw new InvalidOperationException();
    }

    private class CaseLabel
    {
      public CaseLabel(T label, Func<TResult> returnFunc)
      {
        this.Label = label;
        this.ReturnFunc = returnFunc;
      }

      public T Label { get; private set; }

      public Func<TResult> ReturnFunc { get; private set; }
    }
  }
}
