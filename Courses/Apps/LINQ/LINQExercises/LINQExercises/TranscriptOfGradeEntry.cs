using System;

namespace LINQExercises
{
  public class TranscriptOfGradeEntry
  {
    public TranscriptOfGradeEntry(int courseId, float grade)
    {
      if ((grade <= 0F) || (grade > 10F))
      {
        throw new ArgumentOutOfRangeException(nameof(grade), $"The grade (passed value='{grade}') must be between 0 and 10");
      }

      this.CourseId = courseId;
      this.Grade = grade;
    }

    public int CourseId { get; private set; }

    public float Grade { get; private set; }
  }
}
