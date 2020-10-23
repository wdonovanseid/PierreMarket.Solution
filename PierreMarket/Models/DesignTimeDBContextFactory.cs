using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PierreMarket.Models
{
  public class PierreMarketContextFactory : IDesignTimeDbContextFactory<PierreMarketContext>
  {

    PierreMarketContext IDesignTimeDbContextFactory<PierreMarketContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<PierreMarketContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new PierreMarketContext(builder.Options);
    }
  }
}