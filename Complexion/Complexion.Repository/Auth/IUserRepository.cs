using Complexion.Models.Auth;

namespace Complexion.Repository.Auth
{
    public interface IUserRepository
    {
        Task<Guid> CreateUserAsync(User user);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
    }
}
