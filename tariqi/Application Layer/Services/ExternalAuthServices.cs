using Microsoft.AspNetCore.Identity;
using tariqi.Application_Layer.Interfaces;
using tariqi.Domain_Layer.Entities;

namespace tariqi.Application_Layer.Services
{
    public class ExternalAuthServices : IExternalAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToken _token;

        public ExternalAuthServices(UserManager<ApplicationUser> userManager, IToken token)
        {
            _userManager = userManager;
            _token = token;
        }

        public async Task<string> GoogleLoginAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email
                };

                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "User");
            }

            var jwtToken = _token.GenerateJwtToken(user, "User");
            return jwtToken;
        }
    }
}
