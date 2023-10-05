using Example.AppSettings;
using Example.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Example.Infrastructure.Factories.Contexts;
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    /// <summary>
    ///     Creates a new instance of the <see cref="ApplicationDbContext" /> using the <see cref="IHost" />.
    /// </summary>
    /// <returns>ApplicationDbContext</returns>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(e => e.AddAppSettings())
            .ConfigureServices(e =>
            {
                e.AddAppSettingsOptions();
                e.AddInfrastructure();
            }).Build();

        return host.Services.GetRequiredService<ApplicationDbContext>();
    }
}
