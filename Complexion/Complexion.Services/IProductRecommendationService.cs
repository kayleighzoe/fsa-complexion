using Complexion.Models.Dbo;

namespace Complexion.Services
{
    public interface IProductRecommendationService
    {
        Task<IEnumerable<ProductRecommendation>> GetAllProductReccommendationsAsync();
        Task<ProductRecommendation?> GetProductReccommendationAsync(Guid id);
        Task CreateProductReccommendationAsync(CreateProductRecommendationDto dto);
        Task UpdateProductReccommendationAsync(Guid id, UpdateProductRecommendationDto dto); 
        Task DeleteProductReccommendationAsync(Guid id);
    }
}