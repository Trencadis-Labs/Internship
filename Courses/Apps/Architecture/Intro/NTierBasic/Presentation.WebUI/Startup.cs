using DataAccess.Abstractions;
using DataAccess.Database;
using DataAccess.InMemory;
using DataAccess.Xml;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;
using Models.Core;
using Models.IO;

namespace Presentation.WebUI
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
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

      // Add framework services.
      services.AddMvc();

      services
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
