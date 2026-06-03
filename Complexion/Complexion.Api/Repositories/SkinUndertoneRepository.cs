using Dapper;
using Microsoft.Data.SqlClient;
using Complexion.Api.Models;

namespace Complexion.Api.Repositories
{
    public class SkinUndertoneRepository : ISkinUndertoneRepository
    {
        private readonly string _connectionString;

        public SkinUndertoneRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<SkinUndertone>> GetAllSkinUndertones()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<SkinUndertone>("SELECT * FROM skin.Undertone");
        }
    }
}