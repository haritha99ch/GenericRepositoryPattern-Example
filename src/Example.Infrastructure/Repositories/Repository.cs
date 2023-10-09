using Example.Domain.Common;
using Example.Infrastructure.Common;
using Example.Infrastructure.Contexts;
using Example.Infrastructure.Contracts.Repositories;
using Example.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Example.Infrastructure.Repositories;
internal class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly ApplicationDbContext _context;
    private DbSet<TEntity> EntitySet => _context.Set<TEntity>();
    private IQueryable<TEntity> Queryable => EntitySet.AsNoTracking();
    private static Expression<Func<TEntity, bool>> PredicateById(Guid id) => e => e.Id == id;


    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }


    #region CRUD

    public async Task<TEntity> AddAsync(TEntity record, CancellationToken? cancellationToken = null)
    {
        var newRecord = await EntitySet.AddAsync(record, cancellationToken ?? CancellationToken.None);
        await SaveChangesAsync(cancellationToken);
        newRecord.State = EntityState.Detached;
        return newRecord.Entity;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken? cancellationToken = null)
        => await Queryable.FirstOrDefaultAsync(PredicateById(id), cancellationToken ?? CancellationToken.None);

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken? cancellationToken = null)
        => await Queryable.ToListAsync(cancellationToken ?? CancellationToken.None);

    public async Task<TEntity> UpdateAsync(TEntity record, CancellationToken? cancellationToken = null)
    {
        var entry = EntitySet.Update(record);
        await SaveChangesAsync(cancellationToken);
        entry.State = EntityState.Detached;
        return entry.Entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken? cancellationToken = null)
    {
        var record = await GetByIdAsync(id, cancellationToken);
        if (record == null) return false;
        EntitySet.Remove(record);
        await SaveChangesAsync(cancellationToken);
        return await GetByIdAsync(id, cancellationToken) == null;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken? cancellationToken = null)
        => await Queryable.AnyAsync(PredicateById(id), cancellationToken ?? CancellationToken.None);

    #endregion


    #region Specification

    public async Task<TEntity?> GetByIdAsync<TSpecification>(Guid id,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>
    {
        var specification = (TSpecification)Activator.CreateInstance(typeof(TSpecification), PredicateById(id))!;
        return await Queryable.AddSpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TSpecification>(
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>
    {
        var specification = (TSpecification)Activator.CreateInstance(typeof(TSpecification))!;
        return await Queryable.AddSpecification(specification).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetManyAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>
        => await Queryable.AddSpecification(specification).ToListAsync(cancellationToken ?? CancellationToken.None);

    public async Task<TEntity?> GetOneAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>
        => await Queryable.AddSpecification(specification).FirstOrDefaultAsync();

    public async Task<bool> ExistsAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>
        => await Queryable.AddSpecification(specification).AnyAsync();

    #endregion


    private async Task SaveChangesAsync(CancellationToken? cancellationToken = null)
        => await _context.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
}
