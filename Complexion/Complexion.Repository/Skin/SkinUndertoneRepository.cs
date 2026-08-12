using Complexion.Models.Skin;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Skin
{
    public class SkinUndertoneRepository : ISkinUndertoneRepository
    {
        private readonly string _connectionString;

        public SkinUndertoneRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<SkinUndertone>> GetAllUndertonesAsync()
        {
            var sql = "SELECT * FROM skin.Undertone";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<SkinUndertone>(sql);
        }
    }
}