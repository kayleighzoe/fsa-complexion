using System.Data.Common;
using Complexion.Models.Skin;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Skin
{
    public class SkinShadeRepository : ISkinShadeRepository
    {
        private readonly SqlConnection _connection;

        public SkinShadeRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<SkinShade>> GetAllSkinShadesAsync()
        {
            var sql = "SELECT ShadeId, Name FROM skin.Shade";

            return await _connection.QueryAsync<SkinShade>(sql);
        }
    }
}