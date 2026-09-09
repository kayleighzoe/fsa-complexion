using Complexion.Models.Products;

namespace Complexion.Repository.Products
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(string? search);
        Task<Product?> GetProductAsync(Guid id);
        Task CreateProductAsync(CreateProductDto dto);
    }
}