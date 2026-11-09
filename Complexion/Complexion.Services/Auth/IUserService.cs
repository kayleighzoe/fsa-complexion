using Complexion.Models.Auth;

namespace Complexion.Services.Auth
{
    public interface IUserService
    {
        Task<UserProfileDto> GetProfileAsync(Guid userId);
        Task UpdateProfileAsync(Guid userId, UpdateProfileDto updateProfileDto);
    }
}
