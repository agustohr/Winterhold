using WinterholdWeb.Configurations;
using static WinterholdDataAccess.Dependencies;

namespace WinterholdWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IServiceCollection services = builder.Services;
        IConfiguration configuration = builder.Configuration;
        ConfigureService(configuration, services);
        services.AddControllersWithViews();
        services.AddBusinessServices();
        var app = builder.Build();

        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=LandingPage}/{action=Index}"
        );
        app.UseStaticFiles();

        app.Run();
    }
}
