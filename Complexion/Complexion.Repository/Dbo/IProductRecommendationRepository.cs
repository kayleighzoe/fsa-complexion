using Complexion.Models.Dbo;
using Complexion.DTOs.Dbo;

namespace Complexion.Repository.Dbo
{
    public interface IProductRecommendationRepository
    {
        Task<IEnumerable<ProductRecommendation>> GetAllProductReccommendationsAsync();
        Task<ProductRecommendation?> GetProductReccommendationsByIdAsync(Guid id);
        Task<ProductRecommendation> CreateProductReccommendationAsync(CreateProductRecommendationDto dto);
        Task UpdateProductReccommendationAsync(Guid id, UpdateProductRecommendationDto dto);
        Task DeleteProductReccommendationAsync(Guid id);
    }
}