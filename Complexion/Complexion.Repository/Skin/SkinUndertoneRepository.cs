using Complexion.Models.Skin;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Complexion.Repository.Skin
{
    public class SkinUndertoneRepository : ISkinUndertoneRepository
    {
        private readonly string _connectionString;

        public SkinUndertoneRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<SkinUndertone>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<SkinUndertone>("SELECT * FROM skin.Undertone");
        }
    }
}