using Microsoft.EntityFrameworkCore;
using Tick.API.Contexts;
using Tick.Infra.Database;
using Tick.Infra.DatabaseRepository;
using Tick.Models.Mapping;
using Tick.Services;
using Tick.Shared.Interfaces;

namespace Tick.API.Configurations
{
    public static class TickServiceExtensions
    {
        public static IServiceCollection AddHabitServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TickDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Tick.API"));
            });

            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<ITickDataRepository, TickDataRepository>();
            services.AddScoped<IHabitsServices, HabitsServices>();

            return services;
        }
    }
}
