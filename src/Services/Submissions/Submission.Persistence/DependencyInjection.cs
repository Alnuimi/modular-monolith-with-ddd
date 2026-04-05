using Blocks.EntityFramework.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Submission.Persistence.Repositories;

namespace Submission.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString  = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddDbContext<SubmissionDbContext>((provider, options) =>
        {
            options.UseSqlServer(connectionString);
            options.AddInterceptors(provider.GetService<ISaveChangesInterceptor>());
        });
        
        services.AddScoped(typeof(Repository<>));
        services.AddScoped(typeof(ArticleRepository));
        services.AddScoped(typeof(AssetTypeDefinitionRepository));
        services.AddScoped(typeof(PersonRepository));
        
        return services;
    }
}