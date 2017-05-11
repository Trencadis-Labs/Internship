using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQExercises
{
  public partial class Results
  {
    public class UniversityWithStudents
    {
      public UniversityWithStudents(
        int id,
        string name,
        string addresss,
        IEnumerable<StudentAtUniversity> students)
      {
        this.Id = id;
        this.Name = name;
        this.Address = addresss;

        if (students == null)
        {
          students = Enumerable.Empty<StudentAtUniversity>();
        }

        this.Students = students;
      }

      public int Id { get; private set; }

      public string Name { get; private set; }

      public string Address { get; private set; }

      public IEnumerable<StudentAtUniversity> Students { get; private set; }
    }

    public class StudentAtUniversity
    {
      public StudentAtUniversity(Student student, DateTime from, DateTime? to)
      {
        this.Student = student;
        this.RegistrationDate = from;
        this.GraduationDate = to;
      }

      public Student Student { get; private set; }

      public DateTime RegistrationDate { get; private set; }

      public DateTime? GraduationDate { get; private set; }
    }
  }
}
