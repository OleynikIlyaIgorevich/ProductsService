namespace ProductsService.Infra.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public UnitOfWork(
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        => await _dbContext.SaveChangesAsync(false, cancellationToken) > 0;
}
