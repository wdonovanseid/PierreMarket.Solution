using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace PierreMarket
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

      InitializeDatabase(host);

      host.Run();
    }

    private static void InitializeDatabase(IWebHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;

        try
        {
          SeedData.InitializeAsync(services).Wait();
        }
        catch (Exception ex)
        {
          var logger = services
              .GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "Error occurred seeding the DB.");
        }
      }
    }
  }
}