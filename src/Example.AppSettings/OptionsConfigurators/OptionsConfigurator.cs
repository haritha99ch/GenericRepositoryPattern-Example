using Example.AppSettings.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Example.AppSettings.OptionsConfigurators;
public sealed class OptionsConfigurator<TOptions> : IConfigureOptions<TOptions>, IPostConfigureOptions<TOptions>
    where TOptions : OptionsBase
{
    private readonly IConfiguration _configuration;

    public OptionsConfigurator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(TOptions options) => _configuration.GetSection(typeof(TOptions).Name).Bind(options);

    public void PostConfigure(string? name, TOptions options) => options.ValidateOptions();
}
