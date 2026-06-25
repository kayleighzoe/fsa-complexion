using Dapper;
using Microsoft.Data.SqlClient;
using Complexion.Models.Config;

namespace Complexion.Repository.Config
{
    public class ConfigPriceTierRepository : IConfigPriceTierRepository
    {
        private readonly string _connectionString;

        public ConfigPriceTierRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ConfigPriceTier>> GetAllPriceTiers()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<ConfigPriceTier>("SELECT * FROM config.PriceTier");
        }
    }
}