using System.Net;
using Tick.API.Exceptions;
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

            if (builder.Environment.IsProduction())
            {
                builder.Services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = Path.Combine("dist");
                });
            }

            WebApplication app = builder.Build();

            app.UseHttpsRedirection();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapWhen(y => !y.Request.Path.StartsWithSegments("/api"),
                    client =>
                    {
                        client.UseSpa(spa =>
                        {
                            spa.UseProxyToSpaDevelopmentServer("https://localhost:6363");
                        });
                    });
            }
            else
            {
                app.UseStaticFiles();
                app.UseSpaStaticFiles();
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "dist";
                });
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
