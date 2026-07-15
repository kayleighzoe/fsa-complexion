using Complexion.Models.Config;

namespace Complexion.Services.Config
{
    public interface IConfigPriceTierService
    {
        Task<IEnumerable<ConfigPriceTier>> GetAllAsync();
    }
}