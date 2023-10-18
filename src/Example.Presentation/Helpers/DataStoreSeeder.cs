using Example.Infrastructure.Contexts;

namespace Example.Presentation.Helpers;
public static class DataStoreSeeder
{
    public static async Task SeedData(this ApplicationDbContext context)
    {
        await context.CleanUp();
        for (var i = 0; i < 4; i++)
        {
            await context.Members.AddAsync(DataProvider.AdultMember);
            await context.Members.AddAsync(DataProvider.YoungMember);
        }
        var member = await context.Members.AddAsync(DataProvider.AdultMember);
        var memberId = member.Entity.Id;

        for (var i = 0; i < 4; i++)
        {
            var blog = DataProvider.GetOneBlogPost(false);
            blog.MemberId = memberId;
            await context.Blogs.AddAsync(blog);
            blog = DataProvider.GetOneBlogPost(true);
            blog.MemberId = memberId;
            await context.Blogs.AddAsync(blog);
        }
        // Set entity state to detached
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
    }
}
