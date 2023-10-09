using System.Text.Json;

namespace Example.Presentation.Helpers;
public static class ConsoleLog
{
    public static void WriteLine(object? item)
    {
        Console.WriteLine("\n");
        Console.WriteLine(JsonSerializer.Serialize(item));
    }
}
