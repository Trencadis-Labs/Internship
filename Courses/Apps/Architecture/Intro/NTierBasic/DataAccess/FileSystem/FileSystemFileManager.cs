using DataAccess.Abstractions;
using System;
using System.IO;

namespace DataAccess.FileSystem
{
  public class FileSystemFileManager : IFileManager
  {
    private readonly string storageFolderPath;

    public FileSystemFileManager(string storageFolderPath)
    {
      if(string.IsNullOrWhiteSpace(storageFolderPath))
      {
        throw new ArgumentNullException(nameof(storageFolderPath));
      }

      if(!Directory.Exists(storageFolderPath))
      {
        throw new DirectoryNotFoundException($"Directory '{storageFolderPath}' not found");
      }

      this.storageFolderPath = storageFolderPath;
    }

    public byte[] ReadFile(string name)
    {
      if(string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

      var fullPath = Path.Combine(this.storageFolderPath, name);

      if (!File.Exists(fullPath))
      {
        return null;
      }

      return File.ReadAllBytes(fullPath);
    }

    public void SaveFile(string name, byte[] content)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

      if(content == null)
      {
        throw new ArgumentNullException(nameof(content));
      }

      var fullPath = Path.Combine(this.storageFolderPath, name);

      File.WriteAllBytes(fullPath, content);
    }
  }
}
