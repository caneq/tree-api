using TreesApi.Web.Middlewares;

namespace TreesApi.Web.DependencyInjection;

public static class TreesApiServiceCollectionExtensions
{
    public static IServiceCollection AddTreesApiServices(this IServiceCollection services)
    { 
        return services.AddScoped<ExceptionHandlingMiddleware>();
    }
}
