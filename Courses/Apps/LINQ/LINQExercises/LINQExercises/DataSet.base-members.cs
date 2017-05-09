using System.Collections.Generic;

namespace LINQExercises
{
  public partial class DataSet
  {
    private readonly List<Person> persons = new List<Person>();
    private readonly List<Course> courses = new List<Course>();
    private readonly List<University> universities = new List<University>();
    private readonly List<Student> students = new List<Student>();

    public IEnumerable<Person> Persons { get { return this.persons; } }

    public IEnumerable<Student> Students { get { return this.students; } }

    public IEnumerable<Course> Courses { get { return this.courses; } }

    public IEnumerable<University> Universities { get { return this.universities; } }
  }
}
