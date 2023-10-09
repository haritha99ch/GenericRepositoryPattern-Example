using Example.Domain.Entities;
using Example.Infrastructure.Contexts;
using Example.Infrastructure.Contracts.Repositories;
using Example.Presentation.Helpers;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(AppConfigurator.ConfigureConfiguration)
    .ConfigureServices(AppConfigurator.ConfigureServices)
    .Build();

var context = host.GetService<ApplicationDbContext>();
await context.CleanUp();


#region Initial CRUD Operations

var memberRepository = host.GetService<IRepository<Member>>();
var blogRepository = host.GetService<IRepository<Blog>>();

for (var i = 0; i < 5; i++)
{
    await memberRepository.AddAsync(DataProvider.AdultMember);
    await memberRepository.AddAsync(DataProvider.YoungMember);
}
var newMember = await memberRepository.AddAsync(DataProvider.AdultMember);

var member = await memberRepository.GetByIdAsync(newMember.Id);
member!.FirstName = "New Name";
member = await memberRepository.UpdateAsync(member);
member = await memberRepository.GetByIdAsync(member.Id);

var deleted = await memberRepository.DeleteAsync(member!.Id);
var allMember = await memberRepository.GetAllAsync();
member = allMember.FirstOrDefault();

for (var i = 0; i < 2; i++)
{
    var blog = DataProvider.GetOneBlogPost(false);
    blog.MemberId = member!.Id;
    await blogRepository.AddAsync(blog);

    blog = DataProvider.GetOneBlogPost(true);
    blog.MemberId = member!.Id;
    await blogRepository.AddAsync(blog);
}

var allBlog = await blogRepository.GetAllAsync();

#endregion


Console.ReadKey();
