using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SantasWishlist.Domain;
using SantasWishlist_Data;

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
            }).AddEntityFrameworkStores<SantaDbContext>();

            services.AddScoped<IGiftRepository>();

            services.AddControllersWithViews();
        }
 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
