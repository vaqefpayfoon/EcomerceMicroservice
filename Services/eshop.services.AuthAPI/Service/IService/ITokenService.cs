using StackExchange.Redis;

namespace eshop.services.AuthAPI.Service.IService;


public interface ITokenService
{
    Task StoreTokenAsync(string userId, string token);
    Task RemoveTokenAsync(string userId);
    Task<IEnumerable<RedisKey>> GetAllKeysAsync();
    Task<Dictionary<string, string>> GetAllDataAsync();
}