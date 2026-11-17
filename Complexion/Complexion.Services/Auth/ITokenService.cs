using Complexion.Models.Auth;

namespace Complexion.Services.Auth
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}