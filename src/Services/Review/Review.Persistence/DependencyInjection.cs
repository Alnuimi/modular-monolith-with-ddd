using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Review.Persistence.Repositories;

namespace Review.Persistence;

public static  class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString  = configuration.GetConnectionString("Database");
        services.AddDbContext<ReviewDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        
        services.AddScoped(typeof(Repository<>));
        services.AddScoped(typeof(ArticleRepository));
        services.AddScoped(typeof(AssetTypeDefinitionRepository));
        
        return services;
    }
}