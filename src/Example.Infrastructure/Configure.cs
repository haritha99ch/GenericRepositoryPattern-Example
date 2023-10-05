using Example.AppSettings.Helpers;
using Example.AppSettings.Options;
using Example.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Infrastructure;
public static class Configure
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(UseSqlServerFromOptions);
    }

    private static void UseSqlServerFromOptions(IServiceProvider serviceProvider, DbContextOptionsBuilder builder)
    {
        var sqlServerOptions = serviceProvider.GetOptions<SqlServerOptions>();
        builder.UseSqlServer(sqlServerOptions.ConnectionString);
        builder.EnableSensitiveDataLogging();
        builder.EnableDetailedErrors();
    }
}
