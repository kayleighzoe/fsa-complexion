using Complexion.Models.Skin;
using Complexion.Repository.Data;
using Dapper;
using Microsoft.Data.SqlClient;

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
            var sql = @"SELECT 
                            SkinProfileId, 
                            ShadeId, 
                            UndertoneId, 
                            HasTint 
                        FROM dbo.SkinProfile";

            return await _dbContext.QueryAsync<SkinProfile>(sql);
        }

        public async Task<SkinProfile?> GetSkinProfileAsync(Guid id)
        {
            var sql = @"SELECT 
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
            var sql = @"INSERT INTO dbo.SkinProfile 
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
