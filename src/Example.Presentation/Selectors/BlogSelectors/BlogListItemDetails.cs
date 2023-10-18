using System.Linq.Expressions;

namespace Example.Presentation.Selectors.BlogSelectors;
/// <summary>
/// This will include <see cref="Member"/> as the query access <see cref="Member"/> navigation property.
/// <code>
///     SELECT [b].[Caption], [m].[FirstName], [m].[LastName], [b].[CreatedAt]
///     FROM [Blogs] AS [b]
///     INNER JOIN [Members] AS [m] ON [b].[MemberId] = [m].[Id]
/// </code>
/// </summary>
/// <param name="Caption"></param>
/// <param name="Author"></param>
/// <param name="DateTime"></param>
public record BlogListItemDetails
    (string Caption, string Author, DateTime DateTime) : EntitySelector<Blog, BlogListItemDetails>
{
    protected override Expression<Func<Blog, BlogListItemDetails>> Select() =>
        blog => new(blog.Caption, $"{blog.Member!.FirstName} {blog.Member!.LastName}", blog.CreatedAt);
}
