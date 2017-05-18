using System;
using System.Collections.Generic;

namespace Models.Core
{
  public abstract class CustomSwitch<T>
  {
    private readonly List<CaseLabel> caseLabels = new List<CaseLabel>();

    private Action defaultAction = null;

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

    protected void AddCase(T label, Action action)
    {
      this.caseLabels.Add(new CaseLabel(label, action));
    }

    protected void SetDefault(Action action)
    {
      this.defaultAction = action;
    }

    public void Evaluate()
    {
      foreach (var caseItem in this.caseLabels)
      {
        if (this.comparison(this.switchOn, caseItem.Label))
        {
          caseItem.Action?.Invoke();
          return;
        }
      }

      this.defaultAction?.Invoke();
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
}
