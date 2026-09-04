using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Complexion.Repository.Dbo;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Skin
{
    public class SkinProfileRepository : ISkinProfileRepository
    {
        private readonly SqlConnection _connection;

        public SkinProfileRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<SkinProfile>> GetAllSkinProfilesAsync()
        {
            var sql = @"SELECT 
                            SkinProfileId, 
                            ShadeId, 
                            UndertoneId, 
                            HasTint 
                        FROM dbo.SkinProfile";

            return await _connection.QueryAsync<SkinProfile>(sql);
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

            return await _connection.QuerySingleOrDefaultAsync<SkinProfile>(sql, new { Id = id });
        }

        public async Task<SkinProfile> CreateSkinProfileAsync(CreateSkinProfileDto dto)
        {
            var sql = @"INSERT INTO dbo.SkinProfile 
                        (
                            SkinProfileId, 
                            ShadeId, 
                            UndertoneId, 
                            HasTint
                        )
                        OUTPUT INSERTED.*
                        VALUES 
                        (
                            NEWID(), 
                            @ShadeId, 
                            @UndertoneId, 
                            @HasTint
                        )";

            return await _connection.QuerySingleAsync<SkinProfile>(sql, dto);
        }
    }
}
