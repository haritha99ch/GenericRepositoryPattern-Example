using Microsoft.Extensions.Hosting;

namespace Example.Presentation.Helpers;
public static class ServiceLocator
{
    public static T GetService<T>(this IHost host) where T : class
    {
        if (host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException(
                $"{typeof(T)} needs to be registered within {typeof(T).Assembly.GetName().Name}/Configure.cs.");
        }
        return service;
    }
}
