using System.Data.Common;
using Complexion.Models.Config;
using Dapper;
using Microsoft.Data.SqlClient;


namespace Complexion.Repository.Config
{
    public class ConfigPriceTierRepository : IConfigPriceTierRepository
    {
        private readonly SqlConnection _connection;

        public ConfigPriceTierRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<ConfigPriceTier>> GetAllPriceTiersAsync()
        {
            var sql = @"SELECT 
                            PriceTierId, 
                            Name 
                        FROM config.PriceTier";

            return await _connection.QueryAsync<ConfigPriceTier>(sql);
        }
    }
}