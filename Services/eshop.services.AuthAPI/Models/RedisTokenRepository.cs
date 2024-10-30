using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace eshop.services.AuthAPI.Models;

public class RedisTokenRepository : ITokenRepository
{
    private readonly IDistributedCache _cache;

    public RedisTokenRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task SetTokenAsync(string key, string token, TimeSpan expiration)
    {
        await _cache.SetStringAsync(key, token, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        });
    }

    public async Task<string> GetTokenAsync(string key)
    {
        return await _cache.GetStringAsync(key);
    }

    public async Task DeleteTokenAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}