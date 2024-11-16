namespace Tick.API.SPA
{
    public static class SpaServiceExtensions
    {
        public static void UseSpaConfiguration(this WebApplication app)
        {
            string? spaDevServerUrl = app.Configuration["Spa:DevelopmentServerUrl"];
            string? spaStaticFilesPath = app.Configuration["Spa:StaticFilesPath"] ?? "dist";

            app.MapWhen(context => !context.Request.Path.StartsWithSegments("/api"),
                appBuilder =>
                {
                    if (app.Environment.IsDevelopment() && !string.IsNullOrEmpty(spaDevServerUrl))
                    {
                        appBuilder.UseSpa(spa =>
                        {
                            spa.UseProxyToSpaDevelopmentServer(spaDevServerUrl);
                        });
                    }
                    else if (app.Environment.IsProduction() && !string.IsNullOrEmpty(spaDevServerUrl))
                    {
                        app.UseSpaStaticFiles();

                        appBuilder.UseSpa(spa =>
                        {
                            spa.Options.SourcePath = spaStaticFilesPath;
                        });
                    }
                    else
                    {
                        throw new Exception("Spa configurations not found.");
                    }
                });
        }

        public static IServiceCollection AddSpaStaticFilesForProduction(this IServiceCollection services,
            IHostEnvironment environment, IConfiguration configuration)
        {
            string? spaStaticFilesPath = configuration["Spa:StaticFilesPath"] ?? "dist";

            if (environment.IsProduction())
            {
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = Path.Combine(spaStaticFilesPath);
                });
            }
            return services;
        }
    }
}
