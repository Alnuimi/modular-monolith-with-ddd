using Blocks.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis.OM;
using StackExchange.Redis;

namespace Journals.Persistence;

public static class DependenciesInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddSingleton(new RedisConnectionProvider(connectionString!));
        var redis = ConnectionMultiplexer.Connect(connectionString!.Replace("redis://", ""));
        services.AddSingleton<IConnectionMultiplexer>(redis);

        services.AddSingleton<JournalDbContext>();
        services.AddScoped(typeof(Repository<>));
        
        return services;
    }
}