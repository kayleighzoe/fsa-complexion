using Complexion.Models.Config;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace Complexion.Repository.Config
{
    public class ConfigPriceTierRepository : IConfigPriceTierRepository
    {
        private readonly string _connectionString;

        public ConfigPriceTierRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<ConfigPriceTier>> GetAllPriceTiers()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<ConfigPriceTier>("SELECT * FROM config.PriceTier");
        }
    }
}