using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQExercises
{
  public class DataSet
  {
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

    private void InitializeCourses(params Course[] courses)
    {
      if (courses == null)
      {
        return;
      }

      this.courses.AddRange(courses);
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
      params TranscriptOfGradeEntry[] grades)
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
        grades);

      if (theStudent == null)
      {
        // new student
        var newStudent = new Student(
          thePerson.Id,
          thePerson.FirstName,
          thePerson.LastName,
          thePerson.DateOfBirth,
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
        new Person(1, "John", "Doe", new DateTime(1975, 3, 24)),
        new Person(2, "Molly", "Joe", new DateTime(1985, 5, 9)),
        new Person(3, "Rachel", "Ray", new DateTime(1995, 7, 12)),
        new Person(4, "Brad", "Pitt", new DateTime(1990, 10, 21))
      );

      this.InitializeUniversities(
        new University(1, "University of Oradea", "str. Armatei Romane, nr. 1, Oradea, Romania"),
        new University(2, "UBB", "str. Universitatii 7-9, Cluj-Napoca")
      );

      this.InitializeCourses(
        // Courses for 'University of Oradea'
        new Course(id: 1, universityId: 1, title: "Programare orientata pe Obiecte", yearOfStudy: 1, semesterOfStudy: 1),
        new Course(id: 2, universityId: 1, title: "Grafica pe Calculator", yearOfStudy: 1, semesterOfStudy: 2),

        // Courses for 'UBB'
        new Course(id: 3, universityId: 2, title: "Programare orientata pe Obiecte", yearOfStudy: 1, semesterOfStudy: 1),
        new Course(id: 4, universityId: 2, title: "Algebra", yearOfStudy: 1, semesterOfStudy: 2)
      );

      this.SetupStudent(
        Person.WithName("John", "Doe"),
        University.WithName("University of Oradea"),
        new DateTime(1990, 10, 1),
        new DateTime(1995, 7, 1),
        new TranscriptOfGradeEntry(courseId: 1, grade: 9F),
        new TranscriptOfGradeEntry(courseId: 2, grade: 10F)
      );

      this.SetupStudent(
        Person.WithName("Molly", "Joe"),
        University.WithName("UBB"),
        new DateTime(1993, 10, 1),
        new DateTime(1996, 7, 1),
        new TranscriptOfGradeEntry(courseId: 3, grade: 7F),
        new TranscriptOfGradeEntry(courseId: 4, grade: 9.5F)
      );
    }

    public IEnumerable<Person> Persons {  get { return this.persons;  } }

    public IEnumerable<Student> Students { get { return this.students; } }

    public IEnumerable<Course> Courses { get { return this.courses; } }

    public IEnumerable<University> Universities { get { return this.universities; } }
  }
}
