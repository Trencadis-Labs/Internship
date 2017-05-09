using System;

namespace LINQExercises
{
  public class University
  {
    public University(string name, string address)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

      this.Id = UniqueIds.GenerateUniqueId<University>();
      this.Name = name;
      this.Address = address;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Address { get; private set; }

    public static Predicate<University> WithName(string universityName)
    {
      return (u) => (u != null) && string.Equals(u.Name, universityName, StringComparison.OrdinalIgnoreCase);
    }

    public static Predicate<University> WithId(int id)
    {
      return (u) => (u != null) && (u.Id == id);
    }
  }
}
