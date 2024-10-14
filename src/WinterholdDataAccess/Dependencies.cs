using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WinterholdDataAccess.Models;

namespace WinterholdDataAccess;

public class Dependencies
{
    public static void ConfigureService(IConfiguration configuration, IServiceCollection services){
        services.AddDbContext<WinterholdContext>(
            option=>option.UseSqlServer(configuration.GetConnectionString("WinterholdConnection"))
        );
    }
}
