using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Santa_WishList.Controllers;
using SantasWishlist.Domain;
using SantasWishlist_Data;
using SantasWishlist_Data.Models;
using SantasWishlist_Data.Repositories;

namespace Santa_WishList
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SantaDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DatabaseContext")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
            }).AddEntityFrameworkStores<SantaDbContext>()
            .AddSignInManager<SignInManager<IdentityUser>>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsNice", policy => policy.RequireClaim("BeenNice"));
                options.AddPolicy("WishListDone", policy => policy.RequireClaim("WishListFilledIn"));
            });

            services.AddScoped<IGiftRepository, GiftRepository>();
            services.AddScoped<ISantaRepository, SantaRepositorySQL>();
            services.AddScoped<AccountController>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SantaDbContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
