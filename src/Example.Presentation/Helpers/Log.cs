using Newtonsoft.Json;

namespace Example.Presentation.Helpers;
public static class Log
{
    public static void WriteLine(object? item)
    {
        Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));
        Console.WriteLine("\n");
    }
}
