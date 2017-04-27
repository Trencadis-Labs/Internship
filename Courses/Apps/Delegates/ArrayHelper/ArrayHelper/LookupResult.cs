using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayHelper
{
  public class LookupResult<T>
  {
    public LookupResult(bool found, int index, T element)
    {
      this.Found = found;
      this.Index = index;
      this.Element = element;
    }

    public bool Found { get; private set; }

    public int Index { get; private set; }

    public T Element { get; private set; }
  }
}
