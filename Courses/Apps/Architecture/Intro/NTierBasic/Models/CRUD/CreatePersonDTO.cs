using System;

namespace Models.CRUD
{
  public class CreatePersonDTO
  {
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public byte[] ImageData { get; set; }

    public string ImageFileName { get; set; }
  }
}
