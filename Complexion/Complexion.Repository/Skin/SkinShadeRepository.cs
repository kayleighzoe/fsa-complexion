using Complexion.Models.Skin;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Skin
{
    public class SkinShadeRepository : ISkinShadeRepository
    {
        private readonly string _connectionString;

        public SkinShadeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<SkinShade>> GetAllSkinShadesAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<SkinShade>("SELECT * FROM skin.Shade");
        }
    }
}