using DataAccess.Abstractions;
using DataAccess.Database;
using DataAccess.InMemory;
using DataAccess.Xml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Models;
using Models.Core;
using Models.IO;
using Models.Paging;
using Models.Sorting;
using Presentation.ConsoleUI.Views.Abstractions;
using Presentation.ConsoleUI.Views.Implementation;
using System;
using System.IO;

namespace Presentation.ConsoleUI
{
  // some resources used to implement Configuration & DI:
  //     - https://blogs.msdn.microsoft.com/fkaduk/2017/02/22/using-strongly-typed-configuration-in-net-core-console-app/
  //     - http://pioneercode.com/post/dependency-injection-logging-and-configuration-in-a-dot-net-core-console-app
  //     - https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/
  class Program
  {
    private static IConfigurationRoot CreateConfigurationRoot()
    {
      // although it's a console app,
      // we're using the same configuration libraries as in ASP.NET Core
      // see more here: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration
      var configBuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(path: "appSettings.json", optional: false, reloadOnChange: true);

      var configRoot = configBuilder.Build();

      return configRoot;
    }

    private static IServiceProvider CreateServiceProvider(IConfigurationRoot configRoot)
    {
      // although it's a console app, 
      // we're using the same DI libraries as ASP.NET Core
      // check more info here: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection
      var serviceCollection = new ServiceCollection();

      // add configuration to DI
      serviceCollection
        .AddOptions()
        .Configure<GlobalSettings>(configRoot.GetSection("Configuration"));

      // register dependencies
      serviceCollection
        .AddSingleton(provider => provider.GetService<IOptions<GlobalSettings>>().Value)
        .AddTransient<IPathServices, DefaultPathServices>()
        .AddTransient<IPersonRepository>(provider =>
        {
          var globalSettings = provider.GetService<IOptions<GlobalSettings>>().Value;

          return StringSwitch<IPersonRepository>
                  .On(globalSettings.UsedRepo)
                  .Case("Xml", () => new XmlPersonRepository(
                                      settings: globalSettings,
                                      pathServices: provider.GetService<IPathServices>()))
                  .MultiCase(new[] { "Db", "Database" }, () => new DatabasePersonRepository(globalSettings))
                  .Default(() => new InMemoryPersonRepository())
                  .Evaluate();
        })
        .AddTransient<IView<SortedPagedCollection<Person, PersonSortCriteria>>, PersonListingView>()
        .AddTransient<IEventPublishView<SortedPagedCollection<Person, PersonSortCriteria>>, MenuView>()
        .AddTransient<IView<string>, UnknownCommandView>();

      var serviceProvider = serviceCollection.BuildServiceProvider();

      return serviceProvider;
    }

    public static void Main(string[] args)
    {
      var configRoot = CreateConfigurationRoot();

      var serviceProvider = CreateServiceProvider(configRoot);

      PersonUi ui = new PersonUi(
        pageSize: 10,
        personRepository: serviceProvider.GetService<IPersonRepository>(),
        personListingView: serviceProvider.GetService<IView<SortedPagedCollection<Person, PersonSortCriteria>>>(),
        menuView: serviceProvider.GetService<IEventPublishView<SortedPagedCollection<Person, PersonSortCriteria>>>(),
        unknownCommandView: serviceProvider.GetService<IView<string>>());

      ui.Start();
    }
  }
}