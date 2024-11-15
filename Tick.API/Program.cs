
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

            builder.Services.AddOpenApi();
            builder.Services.AddIdentityServices(builder.Configuration);

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
