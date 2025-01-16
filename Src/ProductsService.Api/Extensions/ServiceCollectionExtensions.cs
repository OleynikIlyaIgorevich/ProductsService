namespace ProductsService.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductsService API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}
