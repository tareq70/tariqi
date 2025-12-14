using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tariqi.Application_Layer.DTOs.Auth_DTOs;
using tariqi.Application_Layer.Interfaces;
using tariqi.Domain_Layer.Entities;
using tariqi.Domain_Layer.Enums;
using tariqi.Domain_Layer.Repositories_Interfaces;

namespace tariqi.Application_Layer.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToken _token;

        public AuthService(UserManager<ApplicationUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IConfiguration configuration,
                           IToken token)
        {
            _userManager = userManager;
            _token = token;
        }

        private readonly Dictionary<UserRole, string> _roleMap = new()
        {
            { UserRole.Passenger, "Passenger" },
            { UserRole.Driver, "Driver" },
            { UserRole.Admin, "Admin" },
            { UserRole.RegionManager, "RegionManager" },
            { UserRole.AreaManager, "AreaManager" }


        };

        public async Task RegisterUser(RegisterDto dto, UserRole role)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("Email already exists");

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                Gender = dto.Gender,
                BirthDate = dto.BirthDate,
                PhoneNumber = dto.PhoneNumber,
                Role = role,
                IsActive = true,
                RefreshToken = Guid.NewGuid().ToString(), 
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            if (!_roleMap.TryGetValue(role, out var roleName))
                throw new Exception("Role mapping not found");

            var roleResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!roleResult.Succeeded)
                throw new Exception(string.Join(", ", roleResult.Errors.Select(e => e.Description)));
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