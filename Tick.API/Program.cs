using System.Net;
using Tick.API.Configurations;
using Tick.API.Exceptions;
using Tick.API.SPA;
using Tick.Identity.Services;

namespace Tick.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddUserSecrets<Program>();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new GlobalExceptionFilter());
            });

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Listen(IPAddress.Loopback, 5000);
                options.Listen(IPAddress.Loopback, 5001, listenOptions =>
                {
                    listenOptions.UseHttps();
                });
            });

            builder.Services.AddOpenApi();

            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddHabitServices(builder.Configuration);

            builder.Services.AddSpaStaticFilesForProduction(builder.Environment, builder.Configuration);

            WebApplication app = builder.Build();

            app.UseHttpsRedirection();

            app.UseSpaConfiguration();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
