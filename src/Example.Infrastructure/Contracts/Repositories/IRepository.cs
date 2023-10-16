using Example.Domain.Common;
using Example.Infrastructure.Common;
using System.Linq.Expressions;

namespace Example.Infrastructure.Contracts.Repositories;
public interface IRepository<TEntity> where TEntity : Entity
{


    #region Basic CRUD

    Task<TEntity> AddAsync(TEntity record, CancellationToken? cancellationToken = null);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken? cancellationToken = null);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken? cancellationToken = null);
    Task<TEntity> UpdateAsync(TEntity record, CancellationToken? cancellationToken = null);
    Task<bool> DeleteAsync(Guid id, CancellationToken? cancellationToken = null);
    Task<bool> ExistsAsync(Guid id, CancellationToken? cancellationToken = null);

    #endregion


    #region Specification

    Task<TEntity?> GetByIdAsync<TSpecification>(Guid id, CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity>;

    Task<IEnumerable<TEntity>> GetAllAsync<TSpecification>(CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity>;

    Task<TEntity?> GetOneAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>;

    Task<IEnumerable<TEntity>> GetManyAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>;

    Task<bool> ExistsAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>;

    #endregion


    #region Selectors

    Task<TResult?> GetByIdAsync<TResult>(Guid id, Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null) where TResult : EntitySelector<TEntity, TResult>;

    Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null) where TResult : EntitySelector<TEntity, TResult>;

    Task<IEnumerable<TResult>> GetAllAsync<TResult, TSpecification>(Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null)
        where TResult : EntitySelector<TEntity, TResult>
        where TSpecification : Specification<TEntity>;

    Task<TResult?> GetOneAsync<TResult, TSpecification>(TSpecification specification,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null)
        where TResult : EntitySelector<TEntity, TResult>
        where TSpecification : Specification<TEntity>;

    Task<IEnumerable<TResult>> GetManyAsync<TResult, TSpecification>(TSpecification specification,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null)
        where TResult : EntitySelector<TEntity, TResult>
        where TSpecification : Specification<TEntity>;

    #endregion


}
