using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;

namespace Complexion.Services.Dbo
{
    public interface IProductRecommendationService
    {
        Task<IEnumerable<ProductRecommendation>> GetAllAsync();
        Task<ProductRecommendation?> GetByIdAsync(Guid id);
        Task<ProductRecommendation> CreateAsync(CreateProductRecommendationDto dto);
        Task DeleteAsync(Guid id);
    }
}