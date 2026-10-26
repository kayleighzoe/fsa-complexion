using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Complexion.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (!Guid.TryParse(value, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid user token.");
            }

            return userId;
        }
    }
}