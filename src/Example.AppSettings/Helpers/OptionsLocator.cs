using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Example.AppSettings.Helpers;
public static class OptionsLocator
{
    public static TOptions GetOptions<TOptions>(this IServiceProvider serviceProvider) where TOptions : OptionsBase
    {
        var options = serviceProvider.GetService<IConfigureOptions<TOptions>>();

        if (options != null) return serviceProvider.GetRequiredService<IOptions<TOptions>>().Value;
        throw new InvalidOperationException(
            $"No configuration found for {typeof(TOptions).Name}. "
            + $"Please ensure that {typeof(TOptions).Name} "
            + $"is configured for {Assembly.GetCallingAssembly().GetName().Name}.");
    }
}
