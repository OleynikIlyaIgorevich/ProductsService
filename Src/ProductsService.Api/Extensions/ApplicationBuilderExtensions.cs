namespace ProductsService.Api.Extensions;

internal static class ApplicationBuilderExtensions
{
    internal static IApplicationBuilder AddSwagger(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder
            .UseSwagger()
            .UseSwaggerUI();
    }
}
