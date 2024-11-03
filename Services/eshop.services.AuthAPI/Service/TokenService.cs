using eshop.services.AuthAPI.Service.IService;
using StackExchange.Redis;


namespace eshop.services.AuthAPI.Service;

public class TokenService : ITokenService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IConfiguration _configuration;

    public TokenService(IConnectionMultiplexer redis, IConfiguration configuration)
    {
        _redis = redis;
        _configuration = configuration;
    }

    public async Task StoreTokenAsync(string userId, string token)
    {
        var db = _redis.GetDatabase();
        await db.StringSetAsync(userId, token, TimeSpan.FromMinutes(30)); // Set expiration as needed
    }

    public async Task RemoveTokenAsync(string userId)
    {
        var db = _redis.GetDatabase();
        await db.KeyDeleteAsync(userId);
    }
    public async Task<IEnumerable<RedisKey>> GetAllKeysAsync() 
    {
        var server = _redis.GetServer(_configuration.GetSection("Redis")["ConnectionString"]); 
        return server.Keys(); 
    }
    public async Task<Dictionary<string, string>> GetAllDataAsync() 
    { 
        var db = _redis.GetDatabase(); var keys = await GetAllKeysAsync(); 
        var allData = new Dictionary<string, string>(); 
        foreach (var key in keys) 
        { 
            var value = await db.StringGetAsync(key); 
            allData.Add(key, value); 
        } 
        return allData; 
    }
}