using Example.Infrastructure.Contexts;

namespace Example.Presentation.Helpers;
public static class DbCleaner
{
    public static async Task CleanUp(this ApplicationDbContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}
