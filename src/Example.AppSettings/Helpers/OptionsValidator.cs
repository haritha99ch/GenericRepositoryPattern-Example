namespace Example.AppSettings.Helpers;
public static class OptionsValidator
{
    public static void ValidateOptions<TOptions>(this TOptions options) where TOptions : OptionsBase
    {
        try
        {
            Validator.ValidateObject(options, new(options), true);
        }
        catch (Exception e)
        {
            throw new(
                $"\nCheck the following properties of section {typeof(TOptions).Name}, section in user secrets:\n{e.Message}");
        }
    }
}
