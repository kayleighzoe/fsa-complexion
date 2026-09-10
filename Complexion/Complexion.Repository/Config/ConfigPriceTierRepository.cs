using System.Data.Common;
using Complexion.Models.Config;
using Complexion.Repository.Data;
using Dapper;
using Microsoft.Data.SqlClient;


namespace Complexion.Repository.Config
{
    public class ConfigPriceTierRepository : IConfigPriceTierRepository
    {
        private readonly IDbContext _dbContext;

        public ConfigPriceTierRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ConfigPriceTier>> GetAllPriceTiersAsync()
        {
            var sql = @"SELECT 
                            PriceTierId, 
                            Name 
                        FROM config.PriceTier";

            return await _dbContext.QueryAsync<ConfigPriceTier>(sql);
        }
    }
}