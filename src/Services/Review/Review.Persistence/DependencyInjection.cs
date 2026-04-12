using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Review.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Blocks.EntityFramework.Interceptors;
using Blocks.Core.Extensions;

namespace Review.Persistence;

public static  class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString  = configuration.GetConnectionString("Database");
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddDbContext<ReviewDbContext>((provider ,options) =>
        {
            options.UseSqlServer(connectionString);
            options.AddInterceptors(provider.GetRequiredService<ISaveChangesInterceptor>());
        });
        
        services.AddScoped(typeof(Repository<>));
        // services.AddScoped(typeof(ArticleRepository)); // we can use AddConcreteImplementationOfGeneric extension method to register all concrete implementations of Repository<T> in the assembly
        services.AddConcreteImplementationOfGeneric(typeof(Repository<>), new[] {typeof(DependencyInjection).Assembly});
        services.AddScoped(typeof(AssetTypeDefinitionRepository));
        
        return services;
    }
}