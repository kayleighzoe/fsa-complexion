using Complexion.Models.Auth;

namespace Complexion.Repository.Auth
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(User user);
        Task<User?> GetUserDetailsByUsernameAsync(string username);
        Task<User?> GetUserDetailsByEmailAsync(string email);
        Task<UserProfileDto?> GetUserProfileAsync(Guid userId);
        Task UpdateProfileAsync(Guid userId, Guid skinProfileId, UpdateProfileDto updateProfileDto);
        Task<RegistrationConflictsDto> GetRegistrationConflictsAsync(string username, string email);
    }
}