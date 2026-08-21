using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Complexion.Repository.Dbo;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Skin
{
    public class SkinProfileRepository : ISkinProfileRepository
    {
        private readonly string _connectionString;

        public SkinProfileRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<SkinProfile>> GetAllSkinProfilesAsync()
        {
            var sql = "SELECT SkinProfileId, ShadeId, UndertoneId, HasTint FROM dbo.SkinProfile";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<SkinProfile>(sql);
        }

        public async Task<SkinProfile?> GetSkinProfilesByIdAsync(Guid id)
        {
            var sql = "SELECT SkinProfileId, ShadeId, UndertoneId, HasTint FROM dbo.SkinProfile WHERE SkinProfileId = @Id";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<SkinProfile>(sql, new { Id = id });
        }

        public async Task<SkinProfile> CreateSkinProfileAsync(CreateSkinProfileDto dto)
        {
            var sql = @"INSERT INTO dbo.SkinProfile (SkinProfileId, ShadeId, UndertoneId, HasTint)
                        OUTPUT INSERTED.*
                        VALUES (NEWID(), @ShadeId, @UndertoneId, @HasTint)";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleAsync<SkinProfile>(sql, dto);
        }
    }
}
