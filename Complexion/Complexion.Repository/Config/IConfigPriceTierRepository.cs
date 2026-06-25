using Complexion.Models.Config;

namespace Complexion.Repository.Config
{
    public interface IConfigPriceTierRepository
    {
        Task<IEnumerable<ConfigPriceTier>> GetAllPriceTiers();
    }
}