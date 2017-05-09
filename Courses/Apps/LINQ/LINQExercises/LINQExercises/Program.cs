using System;
using System.Linq;

namespace LINQExercises
{
  class Program
  {
    private static void AllPersonsBornInRange(DataSet ds, DateTime startDate, DateTime endDate)
    {
      var query = ds.Persons.Where(p => (p.DateOfBirth >= startDate) && (p.DateOfBirth <= endDate));

      ConsoleHelper.WriteHeadingWithAction(
        $"Persons born between '{startDate}' and '{endDate}'",
        () =>
        {
          foreach (var person in query)
          {
            Console.WriteLine($"#{person.Id} - {person.FirstName} {person.LastName} - {person.DateOfBirth}");
          }
        });
    }

    private static void AllPersonsWhichAreStudents(DataSet ds)
    {
      var query = ds.Persons.Where(p => ds.Students.Any(s => p.Id == s.Id));

      ConsoleHelper.WriteHeadingWithAction(
        $"Persons which are also students",
        () =>
        {
          foreach (var person in query)
          {
            Console.WriteLine($"#{person.Id} - {person.FirstName} {person.LastName}");
          }
        });
    }

    private static void AllPersonsWhichAreNotStudents(DataSet ds)
    {
      var query = ds.Persons.Where(p => !ds.Students.Any(s => p.Id == s.Id));

      ConsoleHelper.WriteHeadingWithAction(
        $"Persons which are NOT students",
        () =>
        {
          foreach (var person in query)
          {
            Console.WriteLine($"#{person.Id} - {person.FirstName} {person.LastName}");
          }
        });
    }

    static void Main(string[] args)
    {
      DataSet ds = new DataSet();
      ds.Initialize();

      AllPersonsBornInRange(ds, new DateTime(1980, 1, 1), new DateTime(1999, 12, 31));

      AllPersonsWhichAreStudents(ds);

      AllPersonsWhichAreNotStudents(ds);

      ConsoleHelper.PromptForClose();
    }
  }
}
