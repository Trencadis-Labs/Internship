using System;

namespace LINQExercises
{
  public class Person
  {
    public Person(string firstName, string lastName, DateTime dateOfBirth)
      : this(
        id: UniqueIds.GenerateUniqueId<Person>(),
        firstName: firstName,
        lastName: lastName,
        dateOfBirth: dateOfBirth)
    {

    }

    protected Person(int id, string firstName, string lastName, DateTime dateOfBirth)
    {
      if (string.IsNullOrWhiteSpace(firstName))
      {
        throw new ArgumentNullException(nameof(firstName));
      }

      if (string.IsNullOrWhiteSpace(lastName))
      {
        throw new ArgumentNullException(nameof(firstName));
      }

      DateTime today = DateTime.Today;

      if (today.Year - dateOfBirth.Year < 0)
      {
        throw new ArgumentException($"Person cannot be born in the future, year-of-birth is '{dateOfBirth.Year}', current-year is {today.Year}");
      }

      this.Id = id;
      this.FirstName = firstName;
      this.LastName = lastName;
      this.DateOfBirth = dateOfBirth;
    }

    public int Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public DateTime DateOfBirth { get; private set; }

    public int Age
    {
      get
      {
        return DateTime.Now.Year - this.DateOfBirth.Year;
      }
    }

    public static Predicate<Person> WithName(string firstName, string lastName)
    {
      return (p) => (p != null) &&
                    string.Equals(p.FirstName, firstName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(p.LastName, lastName, StringComparison.OrdinalIgnoreCase);
    }

    public static Predicate<Person> WithId(int id)
    {
      return (p) => (p != null) && (p.Id == id);
    }
  }
}
