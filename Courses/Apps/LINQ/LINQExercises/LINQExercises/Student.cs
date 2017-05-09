using System;
using System.Collections.Generic;

namespace LINQExercises
{
  public class Student : Person
  {
    private readonly List<AttendedUniversity> attendedUniversities = new List<AttendedUniversity>();

    public Student(
      string firstName,
      string lastName,
      DateTime dateOfBirth,
      IEnumerable<AttendedUniversity> attendedUniversities = null)
      : this(
          id: UniqueIds.GenerateUniqueId<Student>(),
          firstName: firstName,
          lastName: lastName,
          dateOfBirth: dateOfBirth)
    {
      
    }

    private Student(
      int id,
      string firstName,
      string lastName,
      DateTime dateOfBirth,
      IEnumerable<AttendedUniversity> attendedUniversities = null)
      : base(id: id, firstName: firstName, lastName: lastName, dateOfBirth: dateOfBirth)
    {
      if (attendedUniversities != null)
      {
        this.attendedUniversities.AddRange(attendedUniversities);
      }
    }

    public IEnumerable<AttendedUniversity> AttendedUniversities
    {
      get
      {
        return this.attendedUniversities;
      }
    }

    public Student FluentAddAttendedUniversity(AttendedUniversity university)
    {
      if (university != null)
      {
        this.attendedUniversities.Add(university);
      }

      return this;
    }

    public static Student FromPerson(Person p, IEnumerable<AttendedUniversity> attendedUniversities = null)
    {
      return new Student(
        id: p.Id,
        firstName: p.FirstName,
        lastName: p.LastName,
        dateOfBirth: p.DateOfBirth,
        attendedUniversities: attendedUniversities);
    }
  }
}
