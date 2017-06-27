using System.ComponentModel.DataAnnotations;

namespace Presentation.WebUI.Models
{
  public class UpdatePerson : CreatePerson
  {
    [Required(ErrorMessage = "CreateOrUpdatePerson_IdIsRequired")]
    public int Id { get; set; }
  }
}
