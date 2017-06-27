using BusinessLogic.Abstractions;
using BusinessLogic.PersonImages;
using DataAccess.Abstractions;
using DataAccess.Database;
using DataAccess.FileSystem;
using DataAccess.InMemory;
using DataAccess.Xml;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;
using Models.Core;
using Models.IO;
using Presentation.WebUI.Globalization;
using Presentation.WebUI.ModelBinding;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Presentation.WebUI
{
  public class Startup
  {
    private IHostingEnvironment environment;

    public Startup(IHostingEnvironment env)
    {
      this.environment = env;

      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();

      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddOptions()
              .Configure<GlobalSettings>(Configuration.GetSection("Configuration"));

      services.AddLocalization(options => options.ResourcesPath = "Resources");

      services.AddMvc()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization();

      // Add framework services.
      services.AddMvc(
        options =>
        {
          options.ModelBinderProviders.Insert(0, new PersonModelBinderProvider());
        }
      );

      services
       .AddSingleton(provider => provider.GetService<IOptions<GlobalSettings>>().Value)
       .AddTransient<IPathServices, DefaultPathServices>()
       .AddTransient<IFileManager, FileSystemFileManager>(serviceProvider => 
       {
         string webRootPath = this.environment.WebRootPath;
         var uploadsFolderPath = Path.Combine(
                                  new DirectoryInfo(Path.Combine(webRootPath)).Parent.FullName, 
                                  "_Uploads");

         return new FileSystemFileManager(uploadsFolderPath);
       })
       .AddTransient<IPersonImageFileNameGenerator, GenerateImageFileNameFromPersonId>()
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
       });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseBrowserLink();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      var localizationOptions = new RequestLocalizationOptions
      {
        DefaultRequestCulture = new RequestCulture(SupportedCultures.Default),
        SupportedCultures = SupportedCultures.All,
        SupportedUICultures = SupportedCultures.All
      };

      app.UseRequestLocalization(localizationOptions);

      app.UseStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
