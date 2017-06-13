using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.WebUI.Models
{
  public class CreatePerson
  {
    //[Required(ErrorMessageResourceName = "CreatePerson_FirstNameIsRequired", 
    //          ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessage = "CreatePerson_FirstNameIsRequired")]
    [Display(Name = "FirstName")]
    public string FirstName { get; set; }

    //[Required(ErrorMessageResourceName = "CreatePerson_LastNameIsRequired",
    //          ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessage = "CreatePerson_LastNameIsRequired")]
    [Display(Name = "LastName")]
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

    //[Required (ErrorMessageResourceName = "CreatePerson_DateOfBirthRequired",
    //          ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessage = "CreatePerson_DateOfBirthRequired")]
    [Display(Name = "DateOfBirth")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
  }
}
