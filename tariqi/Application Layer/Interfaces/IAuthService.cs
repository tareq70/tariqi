using tariqi.Application_Layer.DTOs.Auth_DTOs;
using tariqi.Domain_Layer.Entities;

namespace tariqi.Application_Layer.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto registerDto);
        Task<string?> LoginAsync(LoginDto loginDto);
    }
}
