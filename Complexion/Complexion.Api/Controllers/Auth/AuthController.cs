using System.Security.Claims;
using Complexion.Models.Auth;
using Complexion.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Complexion.Api.Extensions;

namespace Complexion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtSettings _jwtSettings;

        public AuthController(IAuthService authService, IOptions<JwtSettings> jwtSettings)
        {
            _authService = authService;
            _jwtSettings = jwtSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var token = await _authService.RegisterAsync(registerDto);
            SetAuthCookie(token);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            SetAuthCookie(token);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(JwtSettings.CookieName);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Me()
        {
            var userId = User.GetUserId();

            var username = User.FindFirst(ClaimTypes.Name)?.Value
                        ?? User.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;

            return Ok(new { userId, username });
        }

        private void SetAuthCookie(string token)
        {
            Response.Cookies.Append(JwtSettings.CookieName, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes)
            });
        }
    }
}