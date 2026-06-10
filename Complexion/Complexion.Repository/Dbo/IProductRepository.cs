using Complexion.Models.Dbo;
using Complexion.DTOs.Dbo;

namespace Complexion.Repository.Dbo
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(string? search);
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product> CreateAsync(CreateProductDto dto);
    }
}