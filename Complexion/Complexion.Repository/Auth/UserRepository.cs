using Complexion.Models.Auth;
using Complexion.Repository.Data;

namespace Complexion.Repository.Auth
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _dbContext;

        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateUserAsync(User user)
        {
            const string sql = @"INSERT INTO Auth.[User] 
                                (
                                    SkinProfileId,
                                    FirstName,
                                    Surname,
                                    Username,
                                    Email,
                                    PasswordHash
                                )
                                OUTPUT INSERTED.UserId
                                Values 
                                (
                                    @SkinProfileId,
                                    @FirstName,
                                    @Surname,
                                    @Username,
                                    @Email,
                                    @PasswordHash
                                )";

            return await _dbContext.QuerySingleAsync<Guid>(sql, user);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            const string sql = @"SELECT
                                    UserId,
                                    SkinProfileId,
                                    FirstName,
                                    Surname,
                                    Username,
                                    Email,
                                    PasswordHash
                                FROM auth.[User]
                                WHERE Username = @Username";

            return await _dbContext.QuerySingleOrDefaultAsync<User>(sql, new { Username = username });
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            const string sql = @"SELECT
                                    UserId,
                                    SkinProfileId,
                                    FirstName,
                                    Surname,
                                    Username,
                                    Email,
                                    PasswordHash
                                FROM auth.[User]
                                WHERE Email = @Email";

            return await _dbContext.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
        }
    }
}
