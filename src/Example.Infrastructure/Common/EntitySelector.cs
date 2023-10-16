using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Example.Infrastructure.Common;
public abstract record EntitySelector<TEntity, TResult> where TResult : EntitySelector<TEntity, TResult>
{
    protected abstract Expression<Func<TEntity, TResult>> Select();

    public static Expression<Func<TEntity, TResult>> Selector
        => ((TResult)RuntimeHelpers.GetUninitializedObject(typeof(TResult))).Select();
}
