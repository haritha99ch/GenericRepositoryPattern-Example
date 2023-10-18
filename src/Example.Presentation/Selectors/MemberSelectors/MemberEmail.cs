using System.Linq.Expressions;

namespace Example.Presentation.Selectors.MemberSelectors;
/// <summary>
/// This will include <see cref="Account"/> as select query accesses <see cref="Account"/>
/// <code>
///      SELECT TOP(1) [a].[Email]
///      FROM [Members] AS [m]
///      INNER JOIN [Accounts] AS [a] ON [m].[AccountId] = [a].[Id]
/// </code>
/// </summary>
/// <param name="Email"></param>
public record MemberEmail(string Email) : EntitySelector<Member, MemberEmail>
{
    protected override Expression<Func<Member, MemberEmail>> Select() => e => new(e.Account!.Email);
}
