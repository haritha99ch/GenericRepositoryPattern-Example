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

    /// <summary>
    /// Get By primary key with non-predicated Specification that may include: Order, Joins.
    /// <para>
    /// Specification should contains a parameterless constructor or result in
    /// <see cref="System.MissingMethodException">System.MissingMethodException: Cannot dynamically create an instance of type</see>
    /// </para>
    /// </summary>
    /// <param name="id">Primary Key</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TSpecification">Type of non-predicated Specification</typeparam>
    /// <exception cref="System.MissingMethodException">Cannot dynamically create an instance of type</exception>
    Task<TEntity?> GetByIdAsync<TSpecification>(Guid id, CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity>;

    /// <summary>
    /// Get all with non-predicated Specification that may include: Order, Joins.
    /// <para>
    /// Specification should contains a parameterless constructor or result in
    /// <see cref="System.MissingMethodException">System.MissingMethodException: Cannot dynamically create an instance of type</see>
    /// </para>
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TSpecification">Type of non-predicated Specification</typeparam>
    /// <exception cref="System.MissingMethodException">Cannot dynamically create an instance of type</exception>
    Task<IEnumerable<TEntity>> GetAllAsync<TSpecification>(CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity>;

    /// <summary>
    /// Get one with predicated Specification that may include: Order, Joins.
    /// Optional predication will returns the first record of many.
    /// </summary>
    /// <param name="specification">Predicated Specification</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TSpecification">Predicated Specification</typeparam>
    Task<TEntity?> GetOneAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>;

    /// <summary>
    /// Get many with predicated Specification that may include: Order, Joins.
    /// Optional predication may returns the all of records.
    /// </summary>
    /// <param name="specification">Predicated Specification</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TSpecification">Predicated Specification</typeparam>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetManyAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>;

    /// <summary>
    /// Get boolean value of existence with predicated Specification that may include: Order, Joins.
    /// Optional predication may result in unpredictable results.
    /// </summary>
    /// <param name="specification">Predicated Specification</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TSpecification">Predicated Specification</typeparam>
    Task<bool> ExistsAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>;

    #endregion


    #region Selectors

    /// <summary>
    /// Get By primary key, by selecting specific properties from the entity.
    /// </summary>
    /// <param name="id">Primary Key</param>
    /// <param name="selector">Selector mapping. <see cref="EntitySelector{TEntity,TResult}.Selector"/></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TResult">Named object that abstracts <see cref="EntitySelector{TEntity,TResult}"/></typeparam>
    /// <returns>Named object that contains selected properties of the entity</returns>
    Task<TResult?> GetByIdAsync<TResult>(Guid id, Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null) where TResult : EntitySelector<TEntity, TResult>;

    /// <summary>
    /// Get all, by selecting specific properties from the entity.
    /// </summary>
    /// <param name="selector">Selector mapping. <see cref="EntitySelector{TEntity,TResult}.Selector"/></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TResult">Named object that abstracts <see cref="EntitySelector{TEntity,TResult}"/></typeparam>
    /// <returns>Named object that contains selected properties of the entity</returns>
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null) where TResult : EntitySelector<TEntity, TResult>;

    Task<TResult?> GetOneAsync<TResult, TSpecification>(TSpecification specification,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null)
        where TResult : EntitySelector<TEntity, TResult>
        where TSpecification : Specification<TEntity>;

    /// <summary>
    /// Get many, by selecting specific properties from the entity, with non-predicated Specification that may include: Order, Joins.
    /// Optional predication will returns the all of records.
    /// </summary>
    /// <param name="specification"></param>
    /// <param name="selector">Selector mapping. <see cref="EntitySelector{TEntity,TResult}.Selector"/></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TResult">Named object that abstracts <see cref="EntitySelector{TEntity,TResult}"/></typeparam>
    /// <typeparam name="TSpecification">Predicated Specification</typeparam>
    /// <returns>Named object that contains selected properties of the entity</returns>
    Task<IEnumerable<TResult>> GetManyAsync<TResult, TSpecification>(TSpecification specification,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null)
        where TResult : EntitySelector<TEntity, TResult>
        where TSpecification : Specification<TEntity>;

    #endregion


}
