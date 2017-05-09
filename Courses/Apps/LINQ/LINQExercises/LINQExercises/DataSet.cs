using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQExercises
{
  public class DataSet
  {
    private class CourseTitleAndGrade
    {
      public CourseTitleAndGrade(string courseTitle, float grade)
      {
        this.CourseTitle = courseTitle;
        this.Grade = grade;
      }

      public string CourseTitle { get; private set; }

      public float Grade { get; private set; }
    }

    private readonly List<Person> persons = new List<Person>();
    private readonly List<Course> courses = new List<Course>();
    private readonly List<University> universities = new List<University>();
    private readonly List<Student> students = new List<Student>();

    private void InitializeUniversities(params University[] universities)
    {
      if (universities == null)
      {
        return;
      }

      this.universities.AddRange(universities);
    }

    private int GetUniversityId(Predicate<University> universityPredicate)
    {
      if (universityPredicate == null)
      {
        throw new ArgumentNullException(nameof(universityPredicate));
      }

      var id = this.universities.First(u => universityPredicate(u)).Id;

      return id;
    }

    private int GetUniversityIdByName(string universityName)
    {
      return this.GetUniversityId(univ => string.Equals(univ.Name, universityName, StringComparison.OrdinalIgnoreCase));
    }



    private void InitializeCourses(params Course[] courses)
    {
      if (courses == null)
      {
        return;
      }

      this.courses.AddRange(courses);
    }

    private int GetCourseId(Predicate<Course> coursePredicate)
    {
      if (coursePredicate == null)
      {
        throw new ArgumentNullException(nameof(coursePredicate));
      }

      var id = this.courses.First(u => coursePredicate(u)).Id;

      return id;
    }

    private int GetCourseIdByTitleAndUniversityName(string title, string universityName)
    {
      int universityId = this.GetUniversityIdByName(universityName);

      return this.GetCourseId(
        c => string.Equals(c.Title, title, StringComparison.OrdinalIgnoreCase) &&
             (c.UniversityId == universityId)
      );
    }

    private void InitializePersons(params Person[] persons)
    {
      if (persons == null)
      {
        return;
      }

      this.persons.AddRange(persons);
    }

    private void SetupStudent(
      Predicate<Person> personPredicate,
      Predicate<University> universityPredicate,
      DateTime registrationDate,
      DateTime? graduationDate,
      params CourseTitleAndGrade[] coursesAndGrades)
    {
      if (personPredicate == null)
      {
        throw new ArgumentNullException(nameof(personPredicate));
      }

      if (universityPredicate == null)
      {
        throw new ArgumentNullException(nameof(universityPredicate));
      }

      Person thePerson = null;
      Student theStudent = this.students.FirstOrDefault(s => personPredicate(s));
      if (theStudent != null)
      {
        thePerson = theStudent;
      }
      else
      {
        thePerson = this.persons.First(p => personPredicate(p));
      }

      var theUniversity = this.universities.First(u => universityPredicate(u));

      // TODO: validate TranscriptOfGradeEntry to point to valid courses, provided by the specified university

      var attendedUniversity = new AttendedUniversity(
        theUniversity.Id,
        thePerson.Id,
        registrationDate,
        graduationDate,
        coursesAndGrades.Select(cg => new TranscriptOfGradeEntry(
          courseId: this.GetCourseIdByTitleAndUniversityName(
            title: cg.CourseTitle,
            universityName: theUniversity.Name),
          grade: cg.Grade)));

      if (theStudent == null)
      {
        // new student

        var newStudent = Student.FromPerson(
          thePerson,
          new[] { attendedUniversity });

        this.students.Add(newStudent);
      }
      else
      {
        // existing student
        theStudent.FluentAddAttendedUniversity(attendedUniversity);
      }
    }

    public void Initialize()
    {
      this.InitializePersons(
        new Person("John", "Doe", new DateTime(1975, 3, 24)),
        new Person("Molly", "Joe", new DateTime(1985, 5, 9)),
        new Person("Rachel", "Ray", new DateTime(1995, 7, 12)),
        new Person("Brad", "Pitt", new DateTime(1990, 10, 21))
      );

      this.InitializeUniversities(
        new University("University of Oradea", "str. Armatei Romane, nr. 1, Oradea, Romania"),
        new University("UBB", "str. Universitatii 7-9, Cluj-Napoca")
      );

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
          semesterOfStudy: 2)
      );


      this.SetupStudent(
        Person.WithName("John", "Doe"),
        University.WithName("University of Oradea"),
        new DateTime(1990, 10, 1),
        new DateTime(1995, 7, 1),
        new CourseTitleAndGrade(
          "Programare orientata pe Obiecte",
          9F),
        new CourseTitleAndGrade(
          "Grafica pe Calculator",
          grade: 10F)
      );

      this.SetupStudent(
        Person.WithName("Molly", "Joe"),
        University.WithName("UBB"),
        new DateTime(1993, 10, 1),
        new DateTime(1996, 7, 1),
        new CourseTitleAndGrade(
          "Programare orientata pe Obiecte", 
          7F),
        new CourseTitleAndGrade(
          "Algebra",
          9.5F)
      );
    }

    public IEnumerable<Person> Persons { get { return this.persons; } }

    public IEnumerable<Student> Students { get { return this.students; } }

    public IEnumerable<Course> Courses { get { return this.courses; } }

    public IEnumerable<University> Universities { get { return this.universities; } }


  }
}
