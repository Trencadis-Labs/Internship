using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.WebUI.FileUploads
{
  public static class FormFileExtensions
  {
    public static byte[] GetRawBytes(this IFormFile file)
    {
      if (file == null)
      {
        return null;
      }

      if (file.Length <= 0)
      {
        return new byte[0];
      }

      using (var stream = new MemoryStream())
      {
        file.CopyTo(stream);

        return stream.ToArray();
      }
    }

    public static async Task<byte[]> GetRawBytesAsync(this IFormFile file)
    {
      if (file == null)
      {
        return null;
      }

      if (file.Length <= 0)
      {
        return new byte[0];
      }

      using (var stream = new MemoryStream())
      {
        await file.CopyToAsync(stream);

        return stream.ToArray();
      }
    }
  }
}
