namespace ProductsService.Domain.Extensions;

public static class ServicesCollectionExtensions
{
    public static void AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddTransient<DbContext, ApplicationDbContext>()
            .AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseMySql(configuration.GetConnectionString(
                        nameof(ApplicationDbContext)),
                        new MySqlServerVersion(new Version(8, 0, 40))));


    }
}
