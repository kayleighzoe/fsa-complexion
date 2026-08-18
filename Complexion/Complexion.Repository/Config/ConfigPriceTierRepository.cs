using Complexion.Models.Config;
using Dapper;
using Microsoft.Data.SqlClient;


namespace Complexion.Repository.Config
{
    public class ConfigPriceTierRepository : IConfigPriceTierRepository
    {
        private readonly string _connectionString;

        public ConfigPriceTierRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ConfigPriceTier>> GetAllPriceTiersAsync()
        {
            var sql = "SELECT PriceTierId, Name FROM config.PriceTier";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<ConfigPriceTier>(sql);
        }
    }
}