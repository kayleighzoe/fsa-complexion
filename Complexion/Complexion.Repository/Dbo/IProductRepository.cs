using Complexion.Models.Dbo;
using Complexion.DTOs.Dbo;

namespace Complexion.Repository.Dbo
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(string? search);
        Task<Product?> GetProductAsync(Guid id);
        Task CreateProductAsync(CreateProductDto dto);
    }
}