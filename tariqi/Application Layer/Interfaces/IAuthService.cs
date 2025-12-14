using tariqi.Application_Layer.DTOs.Auth_DTOs;
using tariqi.Domain_Layer.Entities;
using tariqi.Domain_Layer.Enums;

namespace tariqi.Application_Layer.Interfaces
{
    public interface IAuthService
    {
        Task RegisterUser(RegisterDto dto, UserRole role);


        Task<string?> LoginAsync(LoginDto loginDto);
    }
}
