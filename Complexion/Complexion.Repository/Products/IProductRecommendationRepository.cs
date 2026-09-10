using Complexion.Models.Products;

namespace Complexion.Repository.Products
{
    public interface IProductRecommendationRepository
    {
        Task<IEnumerable<ProductRecommendation>> GetAllProductReccommendationsAsync();
        Task<ProductRecommendation?> GetProductReccommendationAsync(Guid id);
        Task CreateProductReccommendationAsync(CreateProductRecommendationDto dto);
        Task UpdateProductReccommendationAsync(Guid id, UpdateProductRecommendationDto dto);
        Task DeleteProductReccommendationAsync(Guid id);
    }
}