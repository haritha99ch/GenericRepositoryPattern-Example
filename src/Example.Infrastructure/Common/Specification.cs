using Example.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Example.Infrastructure.Common;
/// <summary>
/// The Specification class is an abstract class that defines a set of criteria for a type of entity.
/// </summary>
/// <typeparam name="TEntity">The type of the entity to which the specification applies.</typeparam>
public abstract class Specification<TEntity> where TEntity : Entity
{
    /// <summary>
    /// Predicate to filter entities.
    /// </summary>
    public readonly Expression<Func<TEntity, bool>>? PredicateBy;

    /// <summary>
    /// List of includes for the entity.
    /// </summary>
    public List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; } = new();

    /// <summary>
    /// OrderBy expression for the entity.
    /// </summary>
    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

    /// <summary>
    /// OrderByDescending expression for the entity.
    /// </summary>
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

    /// <summary>
    /// Constructor that accepts a predicate.
    /// </summary>
    /// <param name="predicateBy"></param>
    protected Specification(Expression<Func<TEntity, bool>>? predicateBy = null)
    {
        PredicateBy = predicateBy;
    }

    /// <summary>
    /// Method to add an include to the Includes list.
    /// </summary>
    /// <param name="include"></param>
    protected void AddInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include)
        => Includes.Add(include!);

    /// <summary>
    /// Method to set the OrderBy expression.
    /// </summary>
    /// <param name="orderByExpression"></param>
    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        => OrderBy = orderByExpression;

    /// <summary>
    /// Method to set the OrderByDescending expression.
    /// </summary>
    /// <param name="orderByDescendingExpression"></param>
    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        => OrderByDescending = orderByDescendingExpression;
}
