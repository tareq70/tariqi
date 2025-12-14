using tariqi.Application_Layer.DTOs.Auth_DTOs;

namespace tariqi.Application_Layer.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(string id);
        Task<UserDto> UpdateUserAsync(string id, UpdateUserDto dto);
    }
}
