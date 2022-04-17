
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Apps.MVCApp.DI;
using Apps.MVCApp.Models;

namespace Apps.MVCApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediator(config =>
            {
                config.MediatorHandlersRegister();

            });

            services.AddDbContext<MVCAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLLocalConnection")));

            //services.AddDbContext<MVCAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection")));

            //services.AddDbContext<MVCAppContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostGresSqlConnection")));

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;

                options.Password.RequireDigit = false;

                options.Password.RequiredUniqueChars = 0;

                options.Password.RequireLowercase = false;

                options.Password.RequireNonAlphanumeric = false;

                options.Password.RequireUppercase = false;

                options.Password.RequiredLength = 3;

            }).AddEntityFrameworkStores<MVCAppContext>();

            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
