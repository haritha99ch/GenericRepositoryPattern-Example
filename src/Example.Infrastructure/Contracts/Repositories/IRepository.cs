using Example.Domain.Common;

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

}
