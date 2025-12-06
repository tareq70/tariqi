using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tariqi.Application_Layer.DTOs.Auth_DTOs;
using tariqi.Application_Layer.Interfaces;
using tariqi.Domain_Layer.Entities;
using tariqi.Domain_Layer.Repositories_Interfaces;

namespace tariqi.Application_Layer.Services
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IToken _token;

        public AuthService(UserManager<ApplicationUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IConfiguration configuration,
                           IToken token)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _token = token;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                Gender = dto.Gender,
                BirthDate = dto.BirthDate,
                PhoneNumber = dto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            var role = dto.Role ?? "Passenger";
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.Users
           .FirstOrDefaultAsync(u => u.Email == dto.Identifier || u.PhoneNumber == dto.Identifier);

            if (user == null)
                throw new Exception("Invalid credentials");

            var isValid = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!isValid)
                throw new Exception("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            return _token.GenerateJwtToken(user, roles.FirstOrDefault() ?? "Passenger");
        }
    }
}