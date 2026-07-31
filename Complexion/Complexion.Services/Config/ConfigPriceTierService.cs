using Complexion.Models.Config;
using Complexion.Repository.Config;

namespace Complexion.Services.Config
{
    public class ConfigPriceTierService : IConfigPriceTierService
    {
        private readonly IConfigPriceTierRepository _configPriceTierRepository;

        public ConfigPriceTierService(IConfigPriceTierRepository configPriceTierRepository)
        {
            _configPriceTierRepository = configPriceTierRepository;
        }

        public async Task<IEnumerable<ConfigPriceTier>> GetAllPriceTiersAsync()
        {
            return await _configPriceTierRepository.GetAllPriceTiersAsync();
        }
    }
}