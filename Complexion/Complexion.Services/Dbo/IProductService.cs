using Complexion.Models.Dbo;

namespace Complexion.Services.Dbo
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(string? search);
    }
}