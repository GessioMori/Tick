using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tick.Identity.DataContext;
using Tick.Identity.Models;
using Tick.Shared.Consts;
using Tick.Shared.Interfaces.Identity;

namespace Tick.Identity.Services
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Tick.API"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            })
                .AddEntityFrameworkStores<IdentityDataContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = Paths.BaseAccountPath + "/" + Paths.LoginPath;
                options.LogoutPath = Paths.BaseAccountPath + "/" + Paths.LogoutPath;
                options.AccessDeniedPath = Paths.BaseAccountPath + "/" + Paths.AccessDeniedPath;
                options.SlidingExpiration = true;
            });

            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }
    }
}
