using Models;

namespace BusinessLogic.Abstractions
{
  public interface IPersonImageFileNameGenerator
  {
    string GetImageFileName(Person person);
  }
}
