using System;
using System.Collections.Generic;

namespace ArrayHelper
{
  class Program
  {
    private static void ArrayHelper_Sum()
    {
      var array1 = new[] { 1, 2, 3 };
      var array2 = new[] { 3, 4, 5 };

      var sum = ArrayHelper.Sum(array1, array2, new IntSumCalculator());

      ArrayHelper.PrintToConsole(sum);
    }

    static void Main(string[] args)
    {
      List<Student> list = new List<Student>
      {
        new Student { FirstName = "John", LastName = "Doe", UniversityName = "U.O." },
        new Student { FirstName = "Mary", LastName = "Joe", UniversityName = "UBB"}
      };

      // ...

      // covarianta: List<Student> => IEnumerable<Person>
      IEnumerable<Person> personsList = list as IEnumerable<Person>;
      foreach(var p in personsList)
      {
        Console.WriteLine($"{p.FirstName} {p.LastName}");
      }

      // contravarianta
      // IComparer<Person> => IComparer<Student>
      IComparer<Person> personComparer = new PersonComparer();
      var comparison1 = personComparer.Compare(
        new Person { FirstName = "John", LastName = "Doe" },
        new Person { FirstName = "Mary", LastName = "Joe" }
        );
      Console.WriteLine($"Person comparison result: {comparison1}");

      var studentComparer = personComparer as IComparer<Student>;
      var comparison2 = studentComparer.Compare(
        new Student { FirstName = "John", LastName = "Doe", UniversityName = "U.O." },
        new Student { FirstName = "Mary", LastName = "Joe", UniversityName = "UBB" }
      );
      Console.WriteLine($"Student comparison result: {comparison2}");

      // covarianta = cast normal (Derivat => Baza)
      // Generic<Concret> => Generic<Baza>
      // out: pentru rezultate intoarse de functii


      // contravarianta = cast anormal (contra) Baza => Derivat
      // Generic<Baza> => Generic<Concret>
      // in: pentru parametrii ceruti de functii


      Console.ReadKey();
    }
  }
}
