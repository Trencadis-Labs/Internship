using System;
using System.Linq;

namespace LINQExercises
{
  public partial class DataSet
  {
    private void InitializePersons(params Person[] persons)
    {
      if (persons == null)
      {
        return;
      }

      this.persons.AddRange(persons);
    }

    private void SetupStudent(
      Predicate<Person> personPredicate,
      Predicate<University> universityPredicate,
      DateTime registrationDate,
      DateTime? graduationDate,
      params CourseTitleAndGrade[] coursesAndGrades)
    {
      if (personPredicate == null)
      {
        throw new ArgumentNullException(nameof(personPredicate));
      }

      if (universityPredicate == null)
      {
        throw new ArgumentNullException(nameof(universityPredicate));
      }

      Person thePerson = null;
      Student theStudent = this.students.FirstOrDefault(s => personPredicate(s));
      if (theStudent != null)
      {
        thePerson = theStudent;
      }
      else
      {
        thePerson = this.persons.First(p => personPredicate(p));
      }

      var theUniversity = this.universities.First(u => universityPredicate(u));

      var attendedUniversity = new AttendedUniversity(
        theUniversity.Id,
        thePerson.Id,
        registrationDate,
        graduationDate,
        coursesAndGrades.Select(cg => new TranscriptOfGradeEntry(
          courseId: this.GetCourseIdByTitleAndUniversityName(
            title: cg.CourseTitle,
            universityName: theUniversity.Name),
          grade: cg.Grade)));

      if (theStudent == null)
      {
        // new student

        var newStudent = Student.FromPerson(
          thePerson,
          new[] { attendedUniversity });

        this.students.Add(newStudent);
      }
      else
      {
        // existing student
        theStudent.FluentAddAttendedUniversity(attendedUniversity);
      }
    }
  }
}
