using Bogus;
using Example.Domain.Entities;
using Example.Domain.Enums;

namespace Example.Presentation.Helpers;
public static class DataProvider
{
    private static readonly Func<Member> GetOneMember = () =>
    {
        var faker = new Faker<Member>()
            .RuleFor(e => e.FirstName, f => f.Name.FirstName())
            .RuleFor(e => e.LastName, f => f.Name.LastName())
            .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(e => e.DateOfBirth, f => f.Date.Past(20));
        var member = faker.Generate();
        member.Account = GetOneAccount();
        return member;
    };

    public static readonly Func<bool, Blog> GetOneBlogPost = (bool ageRestricted) =>
    {
        var faker = new Faker<Blog>()
            .RuleFor(e => e.Caption, f => f.Lorem.Sentences())
            .RuleFor(e => e.Description, f => f.Lorem.Paragraphs(3));
        var blog = faker.Generate();
        blog.AgeRestricted = ageRestricted;
        return blog;
    };

    public static Member YoungMember
    {
        get
        {
            var member = GetOneMember();
            member.DateOfBirth = DateTime.Now.AddYears(-13);
            return member;
        }
    }

    public static Member AdultMember
    {
        get
        {
            var member = GetOneMember();
            member.DateOfBirth = DateTime.Now.AddYears(-20);
            return member;
        }
    }

    private static readonly Func<Account> GetOneAccount = () =>
    {
        var faker = new Faker<Account>()
            .RuleFor(e => e.Email, f => f.Person.Email)
            .RuleFor(e => e.Password, f => f.Internet.Password());
        var account = faker.Generate();
        account.Role = Role.Member;
        return account;
    };
}
