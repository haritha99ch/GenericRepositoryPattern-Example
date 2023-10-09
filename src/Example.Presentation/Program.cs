using Example.Infrastructure.Contexts;
using Example.Infrastructure.Contracts.Repositories;
using Example.Presentation.Helpers;
using Example.Presentation.Specifications.AccountSpecifications;
using Example.Presentation.Specifications.MemberSpecifications;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(AppConfigurator.ConfigureConfiguration)
    .ConfigureServices(AppConfigurator.ConfigureServices)
    .Build();

var context = host.GetService<ApplicationDbContext>();
await context.CleanUp();

var memberRepository = host.GetService<IRepository<Member>>();
var blogRepository = host.GetService<IRepository<Blog>>();
var accountRepository = host.GetService<IRepository<Account>>();


#region Initial CRUD Operations

for (var i = 0; i < 5; i++)
{
    await memberRepository.AddAsync(DataProvider.AdultMember);
    await memberRepository.AddAsync(DataProvider.YoungMember);
}
Console.WriteLine("\nAdd member");
Console.ReadKey();
var newMember = await memberRepository.AddAsync(DataProvider.AdultMember);
ConsoleLog.WriteLine(newMember);

Console.WriteLine("\nGet Member by Id");
Console.ReadKey();
var member = await memberRepository.GetByIdAsync(newMember.Id);
ConsoleLog.WriteLine(member);

Console.WriteLine("\nUpdate Member");
Console.ReadKey();
member!.FirstName = "New Name";
member = await memberRepository.UpdateAsync(member);
member = await memberRepository.GetByIdAsync(member.Id);
ConsoleLog.WriteLine(member);

Console.WriteLine("\nDelete Member");
Console.ReadKey();
var deleted = await memberRepository.DeleteAsync(member!.Id);
Console.WriteLine(deleted);

Console.WriteLine("\nGet All members");
Console.ReadKey();
var allMember = await memberRepository.GetAllAsync();
ConsoleLog.WriteLine(allMember);

Console.WriteLine("\nAdd Blog Posts");
Console.ReadKey();
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
ConsoleLog.WriteLine(allBlog);

#endregion


#region Specification Operations

Console.WriteLine("\nGet Member by Id with Account");
Console.ReadKey();
var memberWithAccount = await memberRepository.GetByIdAsync<MemberByIdWithAccount>(member!.Id);
ConsoleLog.WriteLine(member);

Console.WriteLine("\nGet Account by Email and Password");
Console.ReadKey();
var accountSpecification =
    new AccountByEmailPassword(memberWithAccount!.Account!.Email, memberWithAccount.Account.Password);
var account = await accountRepository.GetOneAsync(accountSpecification);
Console.WriteLine(account);

Console.WriteLine("\nGet All Members with Account");
Console.ReadKey();
var membersWithAccount = await memberRepository.GetAllAsync<MemberWithAccount>();
ConsoleLog.WriteLine(membersWithAccount);

#endregion


Console.ReadKey();
