using Example.AppSettings.Helpers;
using Example.AppSettings.Options;
using Example.Infrastructure.Contexts;
using Example.Infrastructure.Contracts.Repositories;
using Example.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Infrastructure;
public static class Configure
{
    /// <summary>
    /// <see cref="Example.AppSettings.Configure.AddAppSettingsOptions" /> must be called before this
    /// </summary>
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(UseSqlServerFromOptions);
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }

    private static void UseSqlServerFromOptions(IServiceProvider serviceProvider, DbContextOptionsBuilder builder)
    {
        var sqlServerOptions = serviceProvider.GetOptions<SqlServerOptions>();
        builder.UseSqlServer(sqlServerOptions.ConnectionString, options =>
        {
            options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
        builder.EnableSensitiveDataLogging();
        builder.EnableDetailedErrors();
    }
}
