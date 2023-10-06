using Example.AppSettings;
using Example.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Presentation.Helpers;
public static class AppConfigurator
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddAppSettingsOptions();
        services.AddInfrastructure();
    }

    public static void ConfigureConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddAppSettings();
    }
}
