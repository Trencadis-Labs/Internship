﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Models;
using System;

namespace Presentation.WebUI.ModelBinding
{
  public class PersonModelBinderProvider : IModelBinderProvider
  {
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
      if (context == null)
      {
        throw new ArgumentNullException(nameof(context));
      }

      if (context.Metadata.ModelType == typeof(Person))
      {
        return new BinderTypeModelBinder(typeof(PersonModelBinder));
      }

      return null;
    }
  }
}
