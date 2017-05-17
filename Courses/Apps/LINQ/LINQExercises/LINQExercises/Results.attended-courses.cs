using System.Collections.Generic;
using System.Linq;

namespace LINQExercises
{
  public partial class Results
  {
    public class AttendedCourse
    {
      public AttendedCourse(string title, IEnumerable<AttendedCourseAtUniversity> atUniversities)
      {
        this.Title = title;

        if (atUniversities == null)
        {
          atUniversities = Enumerable.Empty<AttendedCourseAtUniversity>();
        }

        this.AtUniversities = atUniversities;
      }

      public string Title { get; private set; }

      public IEnumerable<AttendedCourseAtUniversity> AtUniversities { get; private set; }
    }

    public class AttendedCourseAtUniversity
    {
      public AttendedCourseAtUniversity(
        int courseId,
        University university,
        int yearOfStudy,
        int semesterOfStudy)
      {
        this.CourseId = courseId;
        this.University = university;
        this.YearOfStudy = yearOfStudy;
        this.SemesterOfStudy = semesterOfStudy;
      }

      public int CourseId { get; private set; }

      public University University { get; private set; }

      public int YearOfStudy { get; private set; }

      public int SemesterOfStudy { get; private set; }
    }
  }
}
