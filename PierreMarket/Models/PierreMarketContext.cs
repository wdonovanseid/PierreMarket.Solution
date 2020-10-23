using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PierreMarket.Models
{
  public class PierreMarketContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Object> Objects { get; set; }

    public PierreMarketContext(DbContextOptions options) : base(options) { }
  }
}