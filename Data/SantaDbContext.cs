using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SantasWishlist_Data
{
    public class SantaDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public SantaDbContext(DbContextOptions<SantaDbContext> options) : base(options)
        {
        }

        //private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        // .AddJsonFile("appsettings.json")
        // .Build();

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DatabaseContext"));
        //    }
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}