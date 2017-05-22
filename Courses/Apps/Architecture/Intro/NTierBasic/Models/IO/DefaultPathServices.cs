using System.IO;

namespace Models.IO
{
  public class DefaultPathServices : IPathServices
  {
    public string GetCurrentDirectory()
    {
      return Directory.GetCurrentDirectory();
    }
  }
}
