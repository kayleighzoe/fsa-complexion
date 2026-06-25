using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;

namespace Complexion.Services.Dbo
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(string? search);
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product> CreateAsync(CreateProductDto dto);
    }
}