using Complexion.Models.Products;

namespace Complexion.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(string? search);
        Task<Product?> GetProductAsync(Guid id);
        Task CreateProductAsync(CreateProductDto dto);
    }
}