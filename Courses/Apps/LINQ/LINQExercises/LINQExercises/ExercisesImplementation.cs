using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQExercises
{
  public static class ExercisesImplementation
  {
    public static IEnumerable<Person> AllPersonsBornInRange(this DataSet data, DateTime startDate, DateTime endDate)
    {
      if (data == null)
      {
        return Enumerable.Empty<Person>();
      }

      var query = data.Persons.Where(p => (p.DateOfBirth >= startDate) && (p.DateOfBirth <= endDate));

      return query;
    }

    public static IEnumerable<Person> AllPersonsWhichAreStudents(this DataSet data)
    {
      if (data == null)
      {
        return Enumerable.Empty<Person>();
      }

      var query = data.Persons.Where(p => data.Students.Any(s => p.Id == s.Id));

      return query;
    }

    public static IEnumerable<Person> AllPersonsWhichAreNotStudents(this DataSet data)
    {
      if (data == null)
      {
        return Enumerable.Empty<Person>();
      }

      var query = data.Persons.Where(p => !data.Students.Any(s => p.Id == s.Id));

      return query;
    }
  }
}
