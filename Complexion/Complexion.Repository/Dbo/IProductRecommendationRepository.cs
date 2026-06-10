using Complexion.Models.Dbo;
using Complexion.DTOs.Dbo;

namespace Complexion.Repository.Dbo
{
    public interface IProductRecommendationRepository
    {
        Task<IEnumerable<ProductRecommendation>> GetAllAsync();
        Task<ProductRecommendation?> GetByIdAsync(Guid id);
        Task<ProductRecommendation> CreateAsync(CreateProductRecommendationDto dto);
        Task DeleteAsync(Guid id);
    }
}