using System;
using System.Collections.Generic;

namespace LINQExercises
{
  public class AttendedUniversity
  {
    private readonly List<TranscriptOfGradeEntry> transcriptOfGrades = new List<TranscriptOfGradeEntry>();

    public AttendedUniversity(
      int universityId,
      int personId,
      DateTime registrationDate,
      DateTime? graduationDate = null,
      IEnumerable<TranscriptOfGradeEntry> grades = null)
    {
      if (graduationDate.HasValue)
      {
        if (graduationDate < registrationDate)
        {
          throw new ArgumentException($"Graduation date ('{graduationDate}') must be after registration date ('{registrationDate}')");
        }
      }

      this.UniversityId = universityId;
      this.PersonId = personId;
      this.RegistrationDate = registrationDate;
      this.GraduationDate = graduationDate;

      if (grades == null)
      {
        this.transcriptOfGrades.AddRange(grades);
      }
    }

    public int UniversityId { get; private set; }

    public int PersonId { get; private set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime? GraduationDate { get; set; }

    public IEnumerable<TranscriptOfGradeEntry> TranscriptOfGrades
    {
      get
      {
        return this.transcriptOfGrades;
      }
    }

    public AttendedUniversity FluentAddTranscriptOfGradeEntry(TranscriptOfGradeEntry entry)
    {
      if (entry != null)
      {
        this.transcriptOfGrades.Add(entry);
      }

      return this;
    }

    public AttendedUniversity FluentSetGraduationDate(DateTime graduationDate)
    {
      if (graduationDate < this.RegistrationDate)
      {
        throw new ArgumentException($"Graduation date ('{graduationDate}') must be after registration date ('{this.RegistrationDate}')");
      }

      this.GraduationDate = graduationDate;

      return this;
    }
  }
}
