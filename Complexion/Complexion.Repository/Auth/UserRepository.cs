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

        public async Task<UserProfileDto?> GetUserProfileAsync(Guid userId)
        {
            const string sql = @"SELECT
                                    u.FirstName,
                                    u.Surname,
                                    u.Email,
                                    s.Name AS ShadeName,
                                    ut.Name AS UndertoneName,
                                    sp.HasTint
                                FROM auth.[User] u
                                INNER JOIN dbo.SkinProfile sp ON sp.SkinProfileId = u.SkinProfileId
                                INNER JOIN skin.Shade s ON s.ShadeId = sp.ShadeId
                                INNER JOIN skin.Undertone ut ON ut.UndertoneId = sp.UndertoneId
                                WHERE u.UserId = @UserId";

            return await _dbContext.QuerySingleOrDefaultAsync<UserProfileDto>(sql, new { UserId = userId });
        }

        public async Task<bool> UpdateProfileAsync(Guid userId, Guid skinProfileId, UpdateProfileDto dto)
        {
            const string sql = @"UPDATE auth.[User]
                                SET FirstName = @FirstName,
                                    Surname = @Surname,
                                    Email = @Email,
                                    SkinProfileId = @SkinProfileId
                                WHERE UserId = @UserId";

            var rowsAffected = await _dbContext.ExecuteAsync(sql, new
            {
                UserId = userId,
                SkinProfileId = skinProfileId,
                dto.FirstName,
                dto.Surname,
                dto.Email
            });

            return rowsAffected > 0;
        }
    }
}
