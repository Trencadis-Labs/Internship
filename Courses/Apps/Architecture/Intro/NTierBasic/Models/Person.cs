using System;

namespace Models
{
  public class Person
  {
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName
    {
      get
      {
        return string.Format(
          "{0}{1}{2}",
          string.IsNullOrWhiteSpace(this.FirstName) ? "" : this.FirstName,
          string.IsNullOrWhiteSpace(this.FirstName) ? "" : " ",
          string.IsNullOrWhiteSpace(this.LastName) ? "" : this.LastName);
      }
    }

    public DateTime DateOfBirth { get; set; }

    public string ImageFileName { get; set; }
  }
}
