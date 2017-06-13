using System;

namespace Models.CRUD
{
  public class CreatePersonDTO
  {
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }
  }
}
