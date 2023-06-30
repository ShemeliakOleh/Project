using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Scrapper_API.Services;
public class RedisCache
{
    private readonly ConnectionMultiplexer _redis;

    public RedisCache(IOptions<RedisConfiguration> config)
    {
        _redis = ConnectionMultiplexer.Connect(config.Value.Redis);
    }

    public void Set(string key, string value)
    {
        var db = _redis.GetDatabase();
        db.StringSet(key, value);
    }

    public string Get(string key)
    {
        var db = _redis.GetDatabase();
        return db.StringGet(key);
    }
}
