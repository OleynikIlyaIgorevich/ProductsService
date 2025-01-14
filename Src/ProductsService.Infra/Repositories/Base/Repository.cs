namespace ProductsService.Infra.Repositories.Base;

public abstract class Repository<TEntity, TId>
    : IRepository<TEntity, TId> where TEntity : BaseEntity<TId>   
{
    protected readonly DbSet<TEntity> _dbSet;

    protected Repository(ApplicationDbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(
            TId id,
            CancellationToken cancellationToken,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = default,
            bool asTracking = default)
    {
        return asTracking
            ? include is null
                ? await _dbSet.FindAsync(new object[] { id }, cancellationToken)
                : await include(_dbSet).AsTracking().SingleOrDefaultAsync(x => Equals(x.Id, id), cancellationToken)
            : include is null
                ? await _dbSet.AsNoTracking().SingleOrDefaultAsync(x => Equals(x.Id, id), cancellationToken)
                : await include(_dbSet).AsNoTracking().SingleOrDefaultAsync(x => Equals(x.Id, id), cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken,
        Expression<Func<TEntity, bool>>? predicate = default,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = default,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = default,
        bool asTracking = default)
    {
        var query = asTracking ? _dbSet.AsTracking() : _dbSet.AsNoTrackingWithIdentityResolution();

        query = include is null ? query : include(query);
        query = predicate is null ? query : query.Where(predicate);
        query = orderBy is null ? query : orderBy(query);

        return await query
            .OrderBy(e => e.CreatedAt)
            .ToListAsync(cancellationToken);
    }


    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        => await _dbSet.AddAsync(entity, cancellationToken);


    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }
}