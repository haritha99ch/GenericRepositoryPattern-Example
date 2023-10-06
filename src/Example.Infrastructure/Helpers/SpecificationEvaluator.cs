using Example.Domain.Common;
using Example.Infrastructure.Common;
using Example.Infrastructure.Repositories;

namespace Example.Infrastructure.Helpers;
internal static class SpecificationEvaluator
{
    /// <summary>
    ///     Adds filtering, eager loading of related entities, and sorting based on the provided specification.
    /// </summary>
    /// <param name="inputQuery"><see cref="Repository{TEntity}.Queryable"/></param>
    /// <param name="specification"><see cref="Specification{TEntity}"/></param>
    /// <typeparam name="TEntity">The type of entity being queried.</typeparam>
    /// <returns><see cref="Repository{TEntity}.Queryable"/></returns>
    internal static IQueryable<TEntity> AddSpecification<TEntity>
        (this IQueryable<TEntity> inputQuery, Specification<TEntity> specification) where TEntity : Entity
    {
        var query = inputQuery;

        if (specification.PredicateBy != null)
            query = query.Where(specification.PredicateBy);

        if (specification.Includes.Any())
            query = specification.Includes
                .Aggregate(query, (current, include) => include(current));

        if (specification.OrderBy != null) query = query.OrderBy(specification.OrderBy);

        else if (specification.OrderByDescending != null)
            query = query.OrderByDescending(specification.OrderByDescending);

        return query;
    }
}
