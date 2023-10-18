using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Example.Infrastructure.Common;
/// <summary>
/// The EntitySelector is a base record used for dynamically generating selectors for entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public abstract record EntitySelector<TEntity, TResult> where TResult : EntitySelector<TEntity, TResult>
{
    /// <summary>
    /// Abstract method to be implemented in derived classes to define the selection expression.
    /// </summary>
    protected abstract Expression<Func<TEntity, TResult>> Select();

    /// <summary>
    /// Static property to get the selector from derived classes.
    /// </summary>
    public static Expression<Func<TEntity, TResult>> Selector
        => ((TResult)RuntimeHelpers.GetUninitializedObject(typeof(TResult))).Select();
}
