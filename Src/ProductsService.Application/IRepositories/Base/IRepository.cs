namespace ProductsService.Application.IRepositories.Base;

public interface IRepository<TEntity, TId>
    where TEntity : BaseEntity<TId>
{
    Task<TEntity?> GetByIdAsync(
        TId id,
        CancellationToken cancellationToken,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = default,
        bool asTracking = default);

    Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken,
        Expression<Func<TEntity, bool>>? predicate = default,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = default,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = default,
        bool asTracking = default);


    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}