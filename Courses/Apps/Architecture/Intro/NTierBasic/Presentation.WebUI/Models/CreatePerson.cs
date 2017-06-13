using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.WebUI.Models
{
  public class CreatePerson
  {
    [Required(ErrorMessageResourceName = "CreatePerson_FirstNameIsRequired", 
              ErrorMessageResourceType = typeof(Resource))]
    public string FirstName { get; set; }

    [Required(ErrorMessageResourceName = "CreatePerson_LastNameIsRequired",
              ErrorMessageResourceType = typeof(Resource))]
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

    [Required (ErrorMessageResourceName = "CreatePerson_DateOfBirthRequired",
              ErrorMessageResourceType = typeof(Resource))]
    public DateTime DateOfBirth { get; set; }
  }
}
