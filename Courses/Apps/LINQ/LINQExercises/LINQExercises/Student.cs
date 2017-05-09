using System;
using System.Collections.Generic;

namespace LINQExercises
{
  public class Student : Person
  {
    private readonly List<AttendedUniversity> attendedUniversities = new List<AttendedUniversity>();

    public Student(
      int id,
      string firstName,
      string lastName,
      DateTime dateOfBirth,
      IEnumerable<AttendedUniversity> attendedUniversities = null)
      : base(id, firstName, lastName, dateOfBirth)
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
  }
}
