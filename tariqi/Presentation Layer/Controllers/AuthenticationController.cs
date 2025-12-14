using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tariqi.Application_Layer.DTOs.Auth_DTOs;
using tariqi.Application_Layer.Interfaces;
using tariqi.Application_Layer.Services;
using tariqi.Domain_Layer.Entities;
using tariqi.Domain_Layer.Enums;

namespace tariqi.Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IToken _token;

        public AuthenticationController(IAuthService authService, IToken token)
        {
            _authService = authService;
            _token = token;
        }
        [HttpPost("register/{role}")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto, [FromRoute] UserRole role)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _authService.RegisterUser(dto, role);
                return Ok(new { message = $"User registered successfully as {role}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token == null) return Unauthorized("Invalid credentials");

            return Ok(new { token });
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto dto)
        {
            try
            {
                var tokens = await _token.RefreshTokenAsync(dto.RefreshToken);
                return Ok(tokens);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok("Authentication controller is working!");
        }

    }
}
