using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExercises
{
  public partial class Results
  {
    public class StudentWithGraduatedUniversity
    {
      public StudentWithGraduatedUniversity(Student student, IEnumerable<GraduatedUniversity> graduatedUniversities)
      {
        this.Student = student;

        if (graduatedUniversities == null)
        {
          graduatedUniversities = Enumerable.Empty<GraduatedUniversity>();
        }

        this.GraduatedUniversities = graduatedUniversities;
      }

      public Student Student { get; private set; }

      public IEnumerable<GraduatedUniversity> GraduatedUniversities { get; private set; }
    }


    public class GraduatedUniversity
    {
      public GraduatedUniversity(University university, DateTime graduationDate)
      {
        this.University = university;
        this.GraduationDate = graduationDate;
      }

      public University University { get; private set; }

      public DateTime GraduationDate { get; private set; }
    }
  }
}
