using eshop.services.AuthAPI.Models.Dto;

namespace eshop.services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task Logout(string userId);
        Task<bool> AssignRole(string email, string roleName);
        Task<Dictionary<string, string>> GetAllDataAsync();
    }
}
