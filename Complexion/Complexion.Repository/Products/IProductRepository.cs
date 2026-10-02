using Complexion.Models.Products;

namespace Complexion.Repository.Products
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> SearchProductsAsync(string search);
        Task<Product?> GetProductAsync(Guid id);
        Task CreateProductAsync(CreateProductDto dto);
    }
}