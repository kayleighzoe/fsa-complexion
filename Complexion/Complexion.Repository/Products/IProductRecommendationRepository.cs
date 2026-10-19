using Complexion.Models.Products;

namespace Complexion.Repository.Products
{
    public interface IProductRecommendationRepository
    {
        Task<IEnumerable<ProductRecommendation>> GetAllProductRecommendationsAsync();
        Task<ProductRecommendation?> GetProductRecommendationAsync(Guid id);
        Task CreateProductRecommendationAsync(Guid userId, CreateProductRecommendationDto dto);
        Task UpdateProductRecommendationAsync(Guid id, UpdateProductRecommendationDto dto);
        Task DeleteProductRecommendationAsync(Guid id);
    }
}