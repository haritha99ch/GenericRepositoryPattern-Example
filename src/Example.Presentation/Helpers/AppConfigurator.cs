using Example.AppSettings;
using Example.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Example.Presentation.Helpers;
public static class AppConfigurator
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddAppSettingsOptions();
        services.AddInfrastructure();
    }

    private static void ConfigureConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddAppSettings();
    }

    public static IHost CreateApp()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureHostConfiguration(config => config.ConfigureConfiguration())
            .ConfigureServices((_, services) => services.ConfigureServices()).Build();
    }
}
