using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQExercises
{
  public static class ExercisesImplementation
  {
    public static IEnumerable<Person> AllPersonsBornInRange(this DataSet data, DateTime startDate, DateTime endDate)
    {
      if (data == null)
      {
        return Enumerable.Empty<Person>();
      }

      var query = data.Persons.Where(p => (p.DateOfBirth >= startDate) && (p.DateOfBirth <= endDate));

      return query;
    }

    public static IEnumerable<Person> AllPersonsWhichAreStudents(this DataSet data)
    {
      if (data == null)
      {
        return Enumerable.Empty<Person>();
      }

      var query = data.Persons.Where(p => data.Students.Any(s => p.Id == s.Id));

      return query;
    }

    public static IEnumerable<Person> AllPersonsWhichAreNotStudents(this DataSet data)
    {
      if (data == null)
      {
        return Enumerable.Empty<Person>();
      }

      var query = data.Persons.Where(p => !data.Students.Any(s => p.Id == s.Id));

      return query;
    }

    public static IEnumerable<Results.AttendedCourse> AllCoursesFromUniversitiesAttendedByStudents(this DataSet data)
    {
      if (data == null)
      {
        return Enumerable.Empty<Results.AttendedCourse>();
      }

      var query = data.Students
                      // starting from students we need to know the universities they attended
                      .SelectMany(student => student.AttendedUniversities)
                      // we need university ids
                      .Select(attendedUniv => attendedUniv.UniversityId)
                      // no university ids duplicates
                      .Distinct()
                      // using university ids, pick all courses for that university
                      .Join(
                        data.Courses,
                        univId => univId,
                        course => course.UniversityId,
                        (univId, course) => course
                      )
                      // then group courses by titles (we want unique courses, by title)
                      .GroupBy(course => course.Title)
                      // compose the end-result
                      .Select(courseGroup => new Results.AttendedCourse(
                        title: courseGroup.Key,
                        atUniversities: courseGroup.Join(
                                          data.Universities,
                                          course => course.UniversityId,
                                          university => university.Id,
                                          (course, university) => new Results.AttendedCourseAtUniversity(
                                                                    courseId: course.Id,
                                                                    university: university,
                                                                    yearOfStudy: course.YearOfStudy,
                                                                    semesterOfStudy: course.SemesterOfStudy))

                        ));

      return query;
    }

    public static IEnumerable<Results.UniversityWithStudents> AllUniversitiesWithTheirStudents(this DataSet data)
    {
      if (data == null)
      {
        return Enumerable.Empty<Results.UniversityWithStudents>();
      }

      var query = data.Universities.Select(university => new Results.UniversityWithStudents(
                    id: university.Id,
                    name: university.Name,
                    addresss: university.Address,
                    students: data.Students
                                  .SelectMany(student => student.AttendedUniversities,
                                              (student, attendedUniversity) => new
                                              {
                                                UniversityId = attendedUniversity.UniversityId,
                                                Student = student,
                                                From = attendedUniversity.RegistrationDate,
                                                To = attendedUniversity.GraduationDate
                                              })
                                  .Where(univstud => univstud.UniversityId == university.Id)
                                  .Select(univstud => new Results.StudentAtUniversity(
                                    student: univstud.Student,
                                    from: univstud.From,
                                    to: univstud.To))
        ));

      return query;
    }

    public static IEnumerable<Results.StudentWithGraduatedUniversity> AllStudentsThatGraduatedOn(this DataSet data, Predicate<University> universityPredicate, int graduationYear)
    {
      if (data == null)
      {
        return Enumerable.Empty<Results.StudentWithGraduatedUniversity>();
      }

      var query = data.Students
                      .SelectMany(
                        stud => stud.AttendedUniversities,
                        (student, attended) => new
                        {
                          Student = student,
                          UniversityId = attended.UniversityId,
                          GraduationDate = attended.GraduationDate
                        })
                      .Where(studattend => studattend.GraduationDate.HasValue &&
                                           (studattend.GraduationDate.Value.Year == graduationYear))
                      .Join(
                        data.Universities,
                        studattend => studattend.UniversityId,
                        university => university.Id,
                        (studattend, university) => new
                        {
                          Student = studattend.Student,
                          University = university,
                          GraduationDate = studattend.GraduationDate.Value
                        })
                      .Where(studuniv => universityPredicate(studuniv.University))
                      .GroupBy(studuniv => studuniv.Student)
                      .Select(studunivgroup => new Results.StudentWithGraduatedUniversity(
                        student: studunivgroup.Key,
                        graduatedUniversities: studunivgroup.Select(gr => new Results.GraduatedUniversity(
                                                                            university: gr.University,
                                                                            graduationDate: gr.GraduationDate))

                      ));

      return query;
    }
  }
}
