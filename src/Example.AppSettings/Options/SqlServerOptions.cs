namespace Example.AppSettings.Options;
public class SqlServerOptions : OptionsBase
{
    [Required]
    public string ConnectionString { get; set; } = default!;
}
