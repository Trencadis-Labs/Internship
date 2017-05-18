using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Extensions
{
  public static class EventArgsExtensions
  {
    public static TDerived As<TDerived>(this EventArgs e)
      where TDerived : EventArgs
    {
      return e as TDerived;
    }
  }
}
