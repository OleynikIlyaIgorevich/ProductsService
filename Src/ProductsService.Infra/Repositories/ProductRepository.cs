
namespace ProductsService.Infra.Repositories;

public class ProductRepository
    : Repository<Product, int>, IProductRepository
{
    public ProductRepository(
        ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<bool> IsExistByTitleAsync(string value, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .AnyAsync(p => p.Title.Equals(value));
    }

    public async Task<bool> IsExistForUpdateByTitleAsync(int id, string value, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .AnyAsync(p => !p.Id.Equals(id) && p.Title.Equals(value));
    }
}
