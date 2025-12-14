using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using tariqi.Application_Layer.Interfaces;
using tariqi.Application_Layer.Services;
using tariqi.Domain_Layer.Entities;

namespace tariqi.Presentation_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalAuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IExternalAuthServices _externalAuthService;

        public ExternalAuthController(SignInManager<ApplicationUser> signInManager, IExternalAuthServices externalAuthService)
        {
            _signInManager = signInManager;
            _externalAuthService = externalAuthService;
        }

        // GOOGLE LOGIN
        [HttpGet("signin-google")]
        public IActionResult SignInWithGoogle()
        {
            var redirectUrl = Url.Action("GoogleResponse", "ExternalAuth");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, "Google");
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            if (!result.Succeeded)
                return BadRequest("Google authentication failed");

            var email = result.Principal.FindFirstValue(ClaimTypes.Email);
            var token = await _externalAuthService.GoogleLoginAsync(email);

            return Ok(new
            {
                Message = "Google login successful",
                Token = token
            });
        }
    }
}
