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
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<SkinProfile>("SELECT * FROM dbo.SkinProfile");
        }

        public async Task<SkinProfile?> GetSkinProfilesByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<SkinProfile>(
                "SELECT * FROM dbo.SkinProfile WHERE SkinProfileId = @Id",
                new { Id = id });
        }

        public async Task CreateSkinProfileAsync(CreateSkinProfileDto dto)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                @"INSERT INTO dbo.SkinProfile (SkinProfileId, ShadeId, UndertoneId, HasTint)
                  OUTPUT INSERTED.*
                  VALUES (NEWID(), @ShadeId, @UndertoneId, @HasTint)",
                dto);
        }
    }
}
