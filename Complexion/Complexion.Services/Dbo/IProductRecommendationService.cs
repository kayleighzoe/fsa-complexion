using Complexion.Models.Dbo;

namespace Complexion.Services.Dbo
{
    public interface IProductRecommendationService
    {
        Task<IEnumerable<ProductRecommendation>> GetAllAsync();
    }
}
