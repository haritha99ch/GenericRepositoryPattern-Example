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
        var record = await ExistsAsync(id, cancellationToken);
        if (!record) return false;
        await EntitySet.Where(e => e.Id == id).ExecuteDeleteAsync();
        await SaveChangesAsync(cancellationToken);
        return !await ExistsAsync(id, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken? cancellationToken = null)
        => await Queryable.AnyAsync(PredicateById(id), cancellationToken ?? CancellationToken.None);

    #endregion


    #region Specification

    public async Task<TEntity?> GetByIdAsync<TSpecification>(Guid id, CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity>
        => await Queryable.AddSpecification<TEntity, TSpecification>()
            .FirstOrDefaultAsync(PredicateById(id), cancellationToken ?? CancellationToken.None);

    public async Task<IEnumerable<TEntity>> GetAllAsync<TSpecification>(CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity>
        => await Queryable.AddSpecification<TEntity, TSpecification>()
            .ToListAsync(cancellationToken ?? CancellationToken.None);

    public async Task<IEnumerable<TEntity>> GetManyAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>
        => await Queryable.AddSpecification(specification).ToListAsync(cancellationToken ?? CancellationToken.None);

    public async Task<TEntity?> GetOneAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>
        => await Queryable.AddSpecification(specification)
            .FirstOrDefaultAsync(cancellationToken ?? CancellationToken.None);

    public async Task<bool> ExistsAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity>
        => await Queryable.AddSpecification(specification).AnyAsync(cancellationToken ?? CancellationToken.None);

    #endregion


    #region Selector

    public async Task<TResult?> GetByIdAsync<TResult>(Guid id,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null)
        where TResult : EntitySelector<TEntity, TResult>
        => await Queryable.Where(PredicateById(id)).Select(selector)
            .FirstOrDefaultAsync(cancellationToken ?? CancellationToken.None);

    public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null)
        where TResult : EntitySelector<TEntity, TResult>
        => await Queryable.Select(selector).ToListAsync(cancellationToken ?? CancellationToken.None);

    public async Task<TResult?> GetByIdAsync<TResult, TSpecification>(Guid id,
        Expression<Func<TEntity, TResult>> selector, CancellationToken? cancellationToken = null)
        where TResult : EntitySelector<TEntity, TResult>
        where TSpecification : Specification<TEntity>
        => await Queryable.AddSpecification<TEntity, TSpecification>().Where(PredicateById(id)).Select(selector)
            .FirstOrDefaultAsync(cancellationToken ?? CancellationToken.None);

    public async Task<IEnumerable<TResult>> GetManyAsync<TResult, TSpecification>(TSpecification specification,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null)
        where TResult : EntitySelector<TEntity, TResult>
        where TSpecification : Specification<TEntity>
        => await Queryable.AddSpecification(specification).Select(selector)
            .ToListAsync(cancellationToken ?? CancellationToken.None);

    public async Task<TResult?> GetOneAsync<TResult, TSpecification>(TSpecification specification,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null)
        where TResult : EntitySelector<TEntity, TResult>
        where TSpecification : Specification<TEntity>
        => await Queryable.AddSpecification(specification).Select(selector)
            .FirstOrDefaultAsync(cancellationToken ?? CancellationToken.None);

    #endregion


    private async Task SaveChangesAsync(CancellationToken? cancellationToken = null)
        => await _context.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
}
