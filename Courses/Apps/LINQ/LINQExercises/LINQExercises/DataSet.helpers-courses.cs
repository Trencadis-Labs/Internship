using System;
using System.Linq;

namespace LINQExercises
{
  public partial class DataSet
  {
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
  }
}
