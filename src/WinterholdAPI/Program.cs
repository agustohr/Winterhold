using WinterholdAPI.Configurations;
using static WinterholdDataAccess.Dependencies;

namespace WinterholdAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);

        IServiceCollection services = builder.Services;
        IConfiguration configuration = builder.Configuration;
        ConfigureService(configuration, services);
        services.AddControllers();
        services.AddBusinessServices();

        services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins, policy => {
                policy.WithOrigins(configuration.GetValue<string>("ClientUrl"))
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        var app = builder.Build();
        app.UseRouting();
        app.UseCors(MyAllowSpecificOrigins);
        app.MapControllers();

        app.Run();
    }
}
