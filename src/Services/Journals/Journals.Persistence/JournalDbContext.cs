using Journals.Domain.Journals;
using Redis.OM;
using Redis.OM.Searching;
using StackExchange.Redis;

namespace Journals.Persistence;

public class JournalDbContext
{
    private readonly RedisConnectionProvider _provider;
    private readonly IDatabase _redisDb;

    public JournalDbContext(IConnectionMultiplexer redis, RedisConnectionProvider provider)
    {
        _provider = provider;
        _redisDb = redis.GetDatabase();
    }

    public IRedisCollection<Journal> Journals => _provider.RedisCollection<Journal>(); 
    
    public IRedisCollection<Editor> Editors => _provider.RedisCollection<Editor>();

    public RedisConnectionProvider Provider => _provider;
}

