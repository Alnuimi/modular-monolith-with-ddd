namespace Submission.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMemoryCache()                    // Basic Caching
            .AddEndpointsApiExplorer()           // Minimal API docs (Swagger)
            .AddSwaggerGen();                    // Swagger Setup
        
        return services;
    }
}