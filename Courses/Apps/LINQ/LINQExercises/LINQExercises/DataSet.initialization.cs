using System;

namespace LINQExercises
{
  public partial class DataSet
  {
    public void Initialize()
    {
      #region Initialize Persons

      this.InitializePersons(
        new Person("John", "Doe", new DateTime(1975, 3, 24)),
        new Person("Molly", "Joe", new DateTime(1985, 5, 9)),
        new Person("Rachel", "Ray", new DateTime(1995, 7, 12)),
        new Person("Brad", "Pitt", new DateTime(1990, 10, 21))
      );

      #endregion

      #region Initialize Universities

      this.InitializeUniversities(
        new University("University of Oradea", "str. Armatei Romane, nr. 1, Oradea, Romania"),
        new University("UBB", "str. Universitatii 7-9, Cluj-Napoca"),
        new University("Universitatea Tehnica Cluj", "str. Memorandumului 28, Cluj-Napoca")
      );

      #endregion

      #region Initialize Courses

      this.InitializeCourses(
        // Courses for 'University of Oradea'
        new Course(
          universityId: this.GetUniversityIdByName("University of Oradea"),
          title: "Programare orientata pe Obiecte",
          yearOfStudy: 1,
          semesterOfStudy: 1),
        new Course(
          universityId: this.GetUniversityIdByName("University of Oradea"),
          title: "Grafica pe Calculator",
          yearOfStudy: 1,
          semesterOfStudy: 2),

        // Courses for 'UBB'
        new Course(
          universityId: this.GetUniversityIdByName("UBB"),
          title: "Programare orientata pe Obiecte",
          yearOfStudy: 1,
          semesterOfStudy: 1),
        new Course(
          universityId: this.GetUniversityIdByName("UBB"),
          title: "Algebra",
          yearOfStudy: 1,
          semesterOfStudy: 2),
        new Course(
          universityId: this.GetUniversityIdByName("UBB"),
          title: "Baze de date",
          yearOfStudy: 2,
          semesterOfStudy: 1),

        // Courses for 'Universitatea Tehnica Cluj'
        new Course(
          universityId: this.GetUniversityIdByName("Universitatea Tehnica Cluj"),
          title: "Programare orientata pe Obiecte",
          yearOfStudy: 1,
          semesterOfStudy: 1),

        new Course(
          universityId: this.GetUniversityIdByName("Universitatea Tehnica Cluj"),
          title: "Analiza",
          yearOfStudy: 1,
          semesterOfStudy: 2),

        new Course(
          universityId: this.GetUniversityIdByName("Universitatea Tehnica Cluj"),
          title: "Structuri de date si tehnici de programare",
          yearOfStudy: 2,
          semesterOfStudy: 1)
      );

      #endregion

      #region Students setup

      this.SetupStudent(
        personPredicate: Person.WithName("John", "Doe"),
        universityPredicate: University.WithName("University of Oradea"),
        registrationDate: new DateTime(1990, 10, 1),
        graduationDate: new DateTime(1995, 7, 1),
        coursesAndGrades: new[] 
        {
          new CourseTitleAndGrade(
            "Programare orientata pe Obiecte",
            9F),
          new CourseTitleAndGrade(
            "Grafica pe Calculator",
            grade: 10F)
        });

      this.SetupStudent(
        personPredicate: Person.WithName("Molly", "Joe"),
        universityPredicate: University.WithName("UBB"),
        registrationDate: new DateTime(1993, 10, 1),
        graduationDate: new DateTime(1996, 7, 1),
        coursesAndGrades: new[]
        {
          new CourseTitleAndGrade(
            "Programare orientata pe Obiecte",
            7F),
          new CourseTitleAndGrade(
            "Algebra",
            9.5F)
        });

      this.SetupStudent(
        personPredicate: Person.WithName("Molly", "Joe"),
        universityPredicate: University.WithName("Universitatea Tehnica Cluj"),
        registrationDate: new DateTime(1993, 10, 1),
        graduationDate: new DateTime(1996, 7, 1),
        coursesAndGrades: new[]
        {
          new CourseTitleAndGrade(
            "Programare orientata pe Obiecte",
            9F),
          new CourseTitleAndGrade(
            "Analiza",
            9F),
           new CourseTitleAndGrade(
            "Structuri de date si tehnici de programare",
            10F)
        });

      #endregion
    }
  }
}
