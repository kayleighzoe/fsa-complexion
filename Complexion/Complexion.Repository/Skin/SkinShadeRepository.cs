using Complexion.Models.Skin;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Complexion.Repository.Skin
{
    public class SkinShadeRepository : ISkinShadeRepository
    {
        private readonly string _connectionString;

        public SkinShadeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<SkinShade>> GetAllSkinShades()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<SkinShade>("SELECT * FROM skin.Shade");
        }
    }
}