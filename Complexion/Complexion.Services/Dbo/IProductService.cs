using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;

namespace Complexion.Services.Dbo
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(string? search);
        Task<Product?> GetProductAsync(Guid id);
        Task CreateProductAsync(CreateProductDto dto);
    }
}