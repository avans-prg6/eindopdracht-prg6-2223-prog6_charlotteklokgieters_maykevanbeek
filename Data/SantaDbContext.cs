using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SantasWishlist_Data.Models;

namespace SantasWishlist_Data
{
    public class SantaDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {

        public SantaDbContext(DbContextOptions<SantaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(new Role("1", "Santa", "SANTA"));
            modelBuilder.Entity<IdentityRole>().HasData(new Role("2", "Child", "CHILD"));

            PasswordHasher<IdentityRole> passwordHasher = new PasswordHasher<IdentityRole>();
            IdentityUser santa = new IdentityUser("Santa");
            santa.NormalizedUserName = "SANTA";
            santa.PasswordHash = passwordHasher.HashPassword(null, "santa");
            modelBuilder.Entity<IdentityUser>().HasData(santa);

            var userRole = new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = santa.Id
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRole);
           

            base.OnModelCreating(modelBuilder);
        }
    }
   
}