using System;
using System.Text;
using System.Linq;

namespace LINQExercises
{
  class Program
  {
    private static void AllPersonsBornInRange(DataSet ds, DateTime startDate, DateTime endDate)
    {
      ConsoleHelper.WriteSectionForCollection(
        $"Persons born between '{startDate:yyyy-MM-dd}' and '{endDate:yyyy-MM-dd}'",
        ds.AllPersonsBornInRange(startDate, endDate),
        person => Console.WriteLine($"#{person.Id} - {person.FirstName} {person.LastName} - {person.DateOfBirth}"),
        () => Console.WriteLine("(no person)"));
    }

    private static void AllPersonsWhichAreStudents(DataSet ds)
    {
      ConsoleHelper.WriteSectionForCollection(
        "Persons which are also students",
        ds.AllPersonsWhichAreStudents(),
        person => Console.WriteLine($"#{person.Id} - {person.FirstName} {person.LastName}"),
        () => Console.WriteLine("(no person)"));
    }

    private static void AllPersonsWhichAreNotStudents(DataSet ds)
    {
      ConsoleHelper.WriteSectionForCollection(
        "Persons which are NOT students",
        ds.AllPersonsWhichAreNotStudents(),
        person => Console.WriteLine($"#{person.Id} - {person.FirstName} {person.LastName}"),
        () => Console.WriteLine("(no person)"));
    }

    private static void AllCoursesFromUniversitiesAttendedByStudents(DataSet ds)
    {
      ConsoleHelper.WriteSectionForCollection(
        "All courses from all the universities attended by students",
        ds.AllCoursesFromUniversitiesAttendedByStudents(),
        attCourse =>
        {
          var courseTextBuilder = new StringBuilder();
          courseTextBuilder.AppendLine($"# {attCourse.Title}");
          courseTextBuilder.AppendLine($"    at following universities: ");
          foreach (var item in attCourse.AtUniversities)
          {
            courseTextBuilder.AppendLine($"       - course #{item.CourseId} at (#{item.University.Id}){item.University.Name} in year {item.YearOfStudy}, semester {item.SemesterOfStudy}");
          }

          Console.WriteLine(courseTextBuilder.ToString());
        },
        () =>
        {
          Console.WriteLine("(no course)");
        });
    }

    private static void AllUniversitiesWithTheirStudents(DataSet ds)
    {
      ConsoleHelper.WriteSectionForCollection(
        "All universities with their students",
        ds.AllUniversitiesWithTheirStudents(),
        univ =>
        {
          var courseTextBuilder = new StringBuilder();
          courseTextBuilder.AppendLine($"#{univ.Id} {univ.Name}");

          var studentsArray = univ.Students?.ToArray();
          if ((studentsArray != null) && (studentsArray.Length > 0))
          {
            courseTextBuilder.AppendLine($"    with following students: ");

            foreach (var item in studentsArray)
            {
              courseTextBuilder.AppendLine($"       - student #{item.Student.Id} {item.Student.FirstName} {item.Student.LastName} from {item.GraduationDate} " + (item.GraduationDate.HasValue ? $"till {item.GraduationDate}" : "till now (not graduated yet)"));
            }
          }
          else
          {
            courseTextBuilder.AppendLine($"    (no students)");
          }


          Console.WriteLine(courseTextBuilder.ToString());
        },
        () =>
        {
          Console.WriteLine("(no university)");
        });
    }

    private static void AllStudentsThatGraduatedOn(DataSet ds, string[] universityNames, int graduationYear)
    {
      if(universityNames == null)
      {
        universityNames = new string[0];
      }

      var universityNamesCsv = string.Join(",", universityNames.Select(u => "'" + u + "'"));

      ConsoleHelper.WriteSectionForCollection(
        $"All students that graduated {universityNamesCsv} in year {graduationYear}",
        ds.AllStudentsThatGraduatedOn(University.MultipleUniversitiesByName(universityNames), graduationYear),
        gradstud =>
        {
          var courseTextBuilder = new StringBuilder();
          courseTextBuilder.AppendLine($"#{gradstud.Student.Id} {gradstud.Student.FirstName} {gradstud.Student.LastName}");

          var graduatedUniversitiesArray = gradstud.GraduatedUniversities?.ToArray();
          if ((graduatedUniversitiesArray != null) && (graduatedUniversitiesArray.Length > 0))
          {
            courseTextBuilder.AppendLine($"    graduated following universities: ");

            foreach (var item in gradstud.GraduatedUniversities)
            {
              courseTextBuilder.AppendLine($"       - university (#{item.University.Id}){item.University.Name} on {item.GraduationDate:yyyy-MM-dd}");
            }
          }
          else
          {
            courseTextBuilder.AppendLine($"    (no universities)");
          }

          Console.WriteLine(courseTextBuilder.ToString());
        },
        () => Console.WriteLine("(no student)"));
    }

    static void Main(string[] args)
    {
      DataSet ds = new DataSet();
      ds.Initialize();

      AllPersonsBornInRange(ds, new DateTime(1980, 1, 1), new DateTime(1999, 12, 31));

      AllPersonsWhichAreStudents(ds);

      AllPersonsWhichAreNotStudents(ds);

      AllCoursesFromUniversitiesAttendedByStudents(ds);

      AllUniversitiesWithTheirStudents(ds);

      AllStudentsThatGraduatedOn(ds, new[] { "University of Oradea", "UBB", "Universitatea Tehnica Cluj" }, 1996);

      ConsoleHelper.PromptForClose();
    }
  }
}
