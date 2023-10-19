using Example.Infrastructure.Contexts;
using Example.Infrastructure.Contracts.Repositories;
using Example.Presentation.Helpers;
using Example.Presentation.Selectors.BlogSelectors;
using Example.Presentation.Selectors.MemberSelectors;
using Example.Presentation.Specifications.BlogSpecifications;
using Example.Presentation.Specifications.MemberSpecifications;


#region App Configuration

// Configure app
var host = AppConfigurator.CreateApp();

// Init dataset
var context = host.GetService<ApplicationDbContext>();
await context.SeedData();

Console.Clear();

// Get repositories
var memberRepository = host.GetService<IRepository<Member>>();
var accountRepository = host.GetService<IRepository<Account>>();
var blogRepository = host.GetService<IRepository<Blog>>();

#endregion


// Evaluate repository


#region Basic CRUD

Console.Write("\nAdd Member...");
Console.ReadKey();

var member = await memberRepository.AddAsync(DataProvider.YoungMember);

Log.WriteLine(member);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Console.Write("\nGet Account...");
Console.ReadKey();

var account = await accountRepository.GetByIdAsync(member.AccountId);

Log.WriteLine(account);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Console.Write("\nGet blogs...");
Console.ReadKey();

var blogs = await blogRepository.GetAllAsync();

Log.WriteLine(blogs);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Console.Write("\nUpdate Member...");
Console.ReadKey();

var updateForMember = new Member
{
    FirstName = "Updated",
    LastName = member.LastName,
    DateOfBirth = member.DateOfBirth,
    PhoneNumber = member.PhoneNumber,
    Id = member.Id,
    AccountId = member.AccountId
};
var updatedMember = await memberRepository.UpdateAsync(updateForMember);

Log.WriteLine(member);
Console.WriteLine("Updated to...");
Log.WriteLine(updatedMember);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Console.Write("\nDelete Blog...");
Console.ReadKey();
var blogList = blogs.ToList();

var blogDeleted = await blogRepository.DeleteAsync(blogList.FirstOrDefault()!.Id);

Log.WriteLine(blogDeleted);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Console.Write("\nBlog exists?...");
Console.ReadKey();

var isBlogExists = await blogRepository.ExistsAsync(blogList.FirstOrDefault()!.Id);

Log.WriteLine(isBlogExists);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#endregion


#region Specification

Console.Write("\nGet Member With Account...");
Console.ReadKey();

var memberWithAccount = await memberRepository.GetByIdAsync<MemberWithAccount>(member.Id);

Log.WriteLine(memberWithAccount);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Console.Write("\nGet All blogs With Member...");
Console.ReadKey();

var allBlogsWithMember = await blogRepository.GetAllAsync<BlogWithMember>();

Log.WriteLine(allBlogsWithMember);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Console.Write("\nGet one blog With Member by caption...");
Console.ReadKey();
var blogsWithMember = allBlogsWithMember.ToList();

var blogWithMemberByCaption =
    await blogRepository.GetOneAsync<BlogWithMember>(new(blogsWithMember.FirstOrDefault()!.Caption));

Log.WriteLine(blogWithMemberByCaption);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Console.Write("\nGet many blogs With Member by Age restriction...");
Console.ReadKey();

var allAgeRestrictedBlogs = await blogRepository.GetManyAsync<BlogWithMember>(new(true));

var ageRestrictedBlogs = allAgeRestrictedBlogs.ToList();
Log.WriteLine(ageRestrictedBlogs);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Console.Write($"\nCheck blogs by Member {member.Id} exists...");
Console.ReadKey();

var blogsExists = await blogRepository.ExistsAsync<BlogWithMember>(new());

Log.WriteLine(blogsExists);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#endregion


# region Selector

Console.Write("\nGet Member email...");
Console.ReadKey();

MemberEmail? memberEmail = await memberRepository.GetByIdAsync<MemberEmail>(member.Id, MemberEmail.Selector);

Log.WriteLine(memberEmail);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


Console.Write("\nGet All blogs list details...");
Console.ReadKey();

IEnumerable<BlogListItemDetails> allBlogListItemDetails =
    await blogRepository.GetAllAsync<BlogListItemDetails>(BlogListItemDetails.Selector);

Log.WriteLine(allBlogListItemDetails);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


Console.Write("\nGet All age restricted blogs list details...");
Console.ReadKey();

IEnumerable<BlogListItemDetails> allAgeRestrictedBlogListItemDetails =
    await blogRepository.GetManyAsync<BlogListItemDetails, BlogWithMember>(new(true), BlogListItemDetails.Selector);

Log.WriteLine(allAgeRestrictedBlogListItemDetails);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Console.Write("\nGet one blog content details...");
Console.ReadKey();

BlogContentDetails? blogContentDetails =
    await blogRepository.GetOneAsync(new BlogWithMember(blogsWithMember.FirstOrDefault()!.Caption),
        BlogContentDetails.Selector);

Log.WriteLine(blogContentDetails);
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

# endregion


Console.ReadKey();
