using System;

namespace LINQExercises
{
  class Program
  {
    private static void AllPersonsBornInRange(DataSet ds, DateTime startDate, DateTime endDate)
    {
      ConsoleHelper.WriteSectionForQuery(
        $"Persons born between '{startDate:yyyy-MM-dd}' and '{endDate:yyyy-MM-dd}'",
        ds.AllPersonsBornInRange(startDate, endDate),
        person => Console.WriteLine($"#{person.Id} - {person.FirstName} {person.LastName} - {person.DateOfBirth}"));
    }

    private static void AllPersonsWhichAreStudents(DataSet ds)
    {
      ConsoleHelper.WriteSectionForQuery(
        "Persons which are also students",
        ds.AllPersonsWhichAreStudents(),
        person => Console.WriteLine($"#{person.Id} - {person.FirstName} {person.LastName}"));
    }

    private static void AllPersonsWhichAreNotStudents(DataSet ds)
    {
      ConsoleHelper.WriteSectionForQuery(
        "Persons which are NOT students",
        ds.AllPersonsWhichAreNotStudents(),
        person => Console.WriteLine($"#{person.Id} - {person.FirstName} {person.LastName}"));
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
