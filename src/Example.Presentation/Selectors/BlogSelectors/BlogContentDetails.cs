using System.Linq.Expressions;

namespace Example.Presentation.Selectors.BlogSelectors;
/// <summary>
/// This will include <see cref="Member"/> and <see cref="Account"/>, as select query accesses <see cref="Member"/> and <see cref="Member.Account"/> navigation properties
/// <code>
///     SELECT TOP(1) [b].[Caption], [m].[FirstName], [m].[LastName], [a].[Email], [b].[CreatedAt], [b].[Description], CASE
///     WHEN [b].[AgeRestricted] = CAST(1 AS bit) THEN N'Age Restricted'
///     ELSE N'Not Age Restricted'
///     END
///     FROM [Blogs] AS [b]
///     INNER JOIN [Members] AS [m] ON [b].[MemberId] = [m].[Id]
///     INNER JOIN [Accounts] AS [a] ON [m].[AccountId] = [a].[Id]
/// </code>
/// </summary>
/// <param name="Title"></param>
/// <param name="Author"></param>
/// <param name="DateTime"></param>
/// <param name="Description"></param>
/// <param name="AgeRestricted"></param>
public record BlogContentDetails(
        string Title,
        string Author,
        DateTime DateTime,
        string Description,
        string AgeRestricted
    ) : EntitySelector<Blog, BlogContentDetails>
{
    protected override Expression<Func<Blog, BlogContentDetails>> Select() =>
        e => new(
            e.Caption,
            $"{e.Member!.FirstName} {e.Member.LastName} - {e.Member.Account!.Email}",
            e.CreatedAt,
            e.Description,
            e.AgeRestricted ? "Age Restricted" : "Not Age Restricted"
        );
}
