using System;

namespace LINQExercises
{
  public class Course
  {
    public Course(int universityId, string title, int yearOfStudy, int semesterOfStudy)
    {
      if (string.IsNullOrWhiteSpace(title))
      {
        throw new ArgumentNullException(nameof(title));
      }

      if (yearOfStudy <= 0)
      {
        throw new ArgumentException($"The year of study (passed value={yearOfStudy}) must be a positive number");
      }

      if ((semesterOfStudy <= 0) || (semesterOfStudy > 2))
      {
        throw new ArgumentException($"The semester of study (passed value={semesterOfStudy}) must be a valid semester value (1 or 2)");
      }

      this.Id = UniqueIds.GetUniqueId<Course>();
      this.UniversityId = universityId;
      this.Title = title;
      this.YearOfStudy = yearOfStudy;
      this.SemesterOfStudy = semesterOfStudy;
    }

    public int Id { get; private set; }

    public int UniversityId { get; private set; }

    public string Title { get; private set; }

    public int YearOfStudy { get; private set; }

    public int SemesterOfStudy { get; private set; }
  }
}
