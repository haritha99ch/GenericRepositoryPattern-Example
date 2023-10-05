using Example.AppSettings.Options;
using Example.AppSettings.OptionsConfigurators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.AppSettings;
public static class Configure
{
    public static void AddAppSettings(this IConfigurationBuilder configuration)
    {
        configuration.AddUserSecrets<AssemblyReference>();
    }

    public static void AddAppSettingsOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<OptionsConfigurator<SqlServerOptions>>();
    }
}
