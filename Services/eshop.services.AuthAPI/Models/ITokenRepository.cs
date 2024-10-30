namespace eshop.services.AuthAPI.Models;

public interface ITokenRepository
{
    Task SetTokenAsync(string key, string token, TimeSpan expiration);
    Task<string> GetTokenAsync(string key);
    Task DeleteTokenAsync(string key);
}