using BusinessLogic.Abstractions;
using Models;
using System;
using System.IO;
using System.Linq;

namespace BusinessLogic.PersonImages
{
  public class GenerateImageFileNameFromPersonId : IPersonImageFileNameGenerator
  {
    private readonly int positionsCount;
    private readonly char padChar;

    public GenerateImageFileNameFromPersonId(int positionsCount = 6, char padChar = '0')
    {
      if(positionsCount < 1)
      {
        throw new ArgumentException($"Invalid value '{positionsCount}' for positions count, must be a strictly positive integer");
      }

      var isInvalidChar = Path.GetInvalidPathChars()
                              .Contains(padChar);

      if(isInvalidChar)
      {
        throw new ArgumentException($"Character '{padChar}' is not allowed in paths");
      }

      this.positionsCount = positionsCount;
      this.padChar = padChar;
    }

    public string GetImageFileName(Person person)
    {
      if(person == null)
      {
        throw new ArgumentNullException(nameof(person));
      }

      return $"Person_{person.Id.ToString().PadLeft(this.positionsCount, this.padChar)}";
    }
  }
}
