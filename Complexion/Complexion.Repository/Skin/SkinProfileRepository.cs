using Complexion.Models.Skin;
using Complexion.Repository.Data;

namespace Complexion.Repository.Skin
{
    public class SkinProfileRepository : ISkinProfileRepository
    {
        private readonly IDbContext _dbContext;

        public SkinProfileRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SkinProfile>> GetAllSkinProfilesAsync()
        {
            const string sql = @"SELECT 
                            SkinProfileId, 
                            ShadeId, 
                            UndertoneId, 
                            HasTint 
                        FROM dbo.SkinProfile";

            return await _dbContext.QueryAsync<SkinProfile>(sql);
        }

        public async Task<SkinProfile?> GetSkinProfileAsync(Guid id)
        {
            const string sql = @"SELECT 
                            SkinProfileId, 
                            ShadeId, 
                            UndertoneId, 
                            HasTint
                        FROM dbo.SkinProfile 
                        WHERE SkinProfileId = @Id";

            return await _dbContext.QuerySingleOrDefaultAsync<SkinProfile>(sql, new { Id = id });
        }

        public async Task CreateSkinProfileAsync(CreateSkinProfileDto dto)
        {
            const string sql = @"INSERT INTO dbo.SkinProfile 
                        (
                            SkinProfileId, 
                            ShadeId, 
                            UndertoneId, 
                            HasTint
                        )
                        VALUES 
                        (
                            NEWID(), 
                            @ShadeId, 
                            @UndertoneId, 
                            @HasTint
                        )";

            await _dbContext.ExecuteAsync(sql, dto);
        }
    }
}
