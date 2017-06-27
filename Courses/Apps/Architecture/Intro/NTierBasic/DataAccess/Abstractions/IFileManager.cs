namespace DataAccess.Abstractions
{
  public interface IFileManager
  {
    void SaveFile(string name, byte[] content);

    byte[] ReadFile(string name);
  }
}
