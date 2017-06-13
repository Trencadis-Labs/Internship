using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Models;
using System;
using System.Threading.Tasks;

namespace Presentation.WebUI.ModelBinding
{
  public class PersonModelBinder : IModelBinder
  {
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
      if (bindingContext == null)
      {
        throw new ArgumentNullException(nameof(bindingContext));
      }

      var firstNameProviderResult =
           bindingContext.ValueProvider.GetValue("FirstName");

      var lastNameProviderResult =
           bindingContext.ValueProvider.GetValue("LastName");

      var dateOfBirthProviderResult =
           bindingContext.ValueProvider.GetValue("DateOfBirth");

      DateTime dateOfBirth = DateTime.MinValue;
      DateTime.TryParse(dateOfBirthProviderResult.FirstValue, out dateOfBirth);

      bindingContext.Result = ModelBindingResult.Success(
        new Person()
        {
          FirstName = firstNameProviderResult.FirstValue,
          LastName = lastNameProviderResult.FirstValue,
          DateOfBirth = dateOfBirth
        });

      return TaskCache.CompletedTask;
    }
  }
}
