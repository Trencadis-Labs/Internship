namespace LINQExercises
{
  public partial class DataSet
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
  }
}
