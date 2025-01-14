
namespace ProductsService.Application.IRepositories;

public interface IProductRepository
    : IRepository<Product, int>
{
    Task<bool> IsExistByTitleAsync(string value, CancellationToken cancellationToken);
    Task<bool> IsExistForUpdateByTitleAsync(int id, string value, CancellationToken cancellationToken);
}
