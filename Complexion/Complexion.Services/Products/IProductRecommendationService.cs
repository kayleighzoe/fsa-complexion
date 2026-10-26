using Complexion.Models.Auth;
using Complexion.Models.Products;

namespace Complexion.Services.Products
{
    public interface IProductRecommendationService
    {
        Task<ProductRecommendation?> GetProductRecommendationAsync(Guid id);
        Task CreateProductRecommendationAsync(Guid userId, CreateProductRecommendationDto dto);
        Task UpdateProductRecommendationAsync(Guid userId, Guid id, UpdateProductRecommendationDto dto);
        Task DeleteProductRecommendationAsync(Guid userId, Guid id);
        Task<IEnumerable<MyRecommendationDto>> GetMyProductRecommendationsAsync(Guid userId);
    }
}