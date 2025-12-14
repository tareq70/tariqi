using tariqi.Application_Layer.DTOs.Auth_DTOs;
using tariqi.Domain_Layer.Entities;

namespace tariqi.Application_Layer.Interfaces
{
    public interface IToken
    {
        public string GenerateJwtToken(ApplicationUser user, string role);
        Task<TokensDto> RefreshTokenAsync(string refreshToken);

    }
}
