using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Presentation.WebUI.Models
{
  public class CreatePerson
  {
    //[Required(ErrorMessageResourceName = "CreateOrUpdatePerson_FirstNameIsRequired", 
    //          ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessage = "CreateOrUpdatePerson_FirstNameIsRequired")]
    [Display(Name = "FirstName")]
    public string FirstName { get; set; }

    //[Required(ErrorMessageResourceName = "CreateOrUpdatePerson_LastNameIsRequired",
    //          ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessage = "CreateOrUpdatePerson_LastNameIsRequired")]
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

    //[Required (ErrorMessageResourceName = "CreateOrUpdatePerson_DateOfBirthRequired",
    //          ErrorMessageResourceType = typeof(Resource))]
    [Required(ErrorMessage = "CreateOrUpdatePerson_DateOfBirthRequired")]
    [Display(Name = "DateOfBirth")]
    public DateTime DateOfBirth { get; set; }

    public IFormFile Image { get; set; }
  }
}
