namespace ProductsService.Infra.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddTransient<IProductRepository, ProductRepository>();
    }

    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services
            .AddTransient<IUnitOfWork, UnitOfWork>();
    }
}
