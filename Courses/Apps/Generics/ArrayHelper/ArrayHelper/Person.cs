using System;
using System.Collections.Generic;

namespace ArrayHelper
{
  public class Person
  {
    public string FirstName { get; set; }

    public string LastName { get; set; }
  }

  public class Student : Person
  {
    public string UniversityName { get; set; }
  }

  public class PersonComparer : IComparer<Person>
  {
    public int Compare(Person x, Person y)
    {
      // Value Meaning 
      //     - Less than zero = x is less than y.
      //     - Zero = x equals y.
      //     - Greater than zero = x is greater than y.

      if (object.ReferenceEquals(x, null) && (object.ReferenceEquals(y, null)))
      {
        return 0;
      }

      if (object.ReferenceEquals(x, null) || (object.ReferenceEquals(y, null)))
      {
        // x == null OR y == null
        if (object.ReferenceEquals(x, null))
        {
          // (null) < y
          return -1;
        }

        if(object.ReferenceEquals(y, null))
        {
          // x > (null)
          return 1;
        }
      }

      string fullNameX = $"{x.FirstName} {x.LastName}";

      string fullNameY = $"{y.FirstName} {y.LastName}";

      return string.Compare(fullNameX, fullNameY);
    }
  }
}
