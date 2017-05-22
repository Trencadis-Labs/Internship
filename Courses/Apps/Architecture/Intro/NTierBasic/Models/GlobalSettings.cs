namespace Models
{
  public class GlobalSettings
  {
    public string UsedRepo { get; set; }

    public RepositoriesConfig RepositoriesConfig { get; set; }
  }

  public class RepositoriesConfig
  {
    public XmlRepositoryConfig Xml { get; set; }

    public DatabaseRepositoryConfig Database { get; set; }
  }

  public class XmlRepositoryConfig
  {
    public string Path { get; set; }
  }

  public class DatabaseRepositoryConfig
  {
    public string ConnectionString { get; set; }
  }
}
