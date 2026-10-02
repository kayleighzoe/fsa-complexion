using Complexion.Models.Config;
using Complexion.Repository.Data;


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
            const string sql = @"SELECT 
                            PriceTierId, 
                            Name 
                        FROM config.PriceTier";

            return await _dbContext.QueryAsync<ConfigPriceTier>(sql);
        }
    }
}