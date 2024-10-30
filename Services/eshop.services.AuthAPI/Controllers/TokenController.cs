using eshop.services.AuthAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace eshop.services.AuthAPI.Controller;

[Route("api/token")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IConfiguration _configuration;

    public TokenController(ITokenRepository tokenRepository, IConfiguration configuration)
    {
        _tokenRepository = tokenRepository;
        _configuration = configuration;
    }

    [HttpPost("store-token")]
    public async Task<IActionResult> StoreToken(string token)
    {
        var key = "user_token"; // Define a unique key for the user/session
        await _tokenRepository.SetTokenAsync(key, token, TimeSpan.FromDays(3)); // Set expiration as needed
        return Ok("Token stored successfully");
    }

    [HttpGet("get-token")]
    public async Task<IActionResult> GetToken()
    {
        var key = "user_token";
        var token = await _tokenRepository.GetTokenAsync(key);
        return Ok(token);
    }

    [HttpDelete("delete-token")]
    public async Task<IActionResult> DeleteToken()
    {
        var key = "user_token";
        await _tokenRepository.DeleteTokenAsync(key);
        return Ok("Token deleted successfully");
    }

    [HttpGet("config")]
    public async Task<IActionResult> Config()
    {
        // var item = _configuration.GetSection("ApiSettings:JwtOptions");
        return Ok("mine");    
    }
}