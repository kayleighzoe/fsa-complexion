using Complexion.Models.Auth;

namespace Complexion.Repository.Auth
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(User user);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<UserProfileDto?> GetUserProfileAsync(Guid userId);
        Task<bool> UpdateProfileAsync(Guid userId, Guid skinProfileId, UpdateProfileDto updateProfileDto);
    }
}
