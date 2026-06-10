using Complexion.Models.Dbo;

namespace Complexion.Repository.Dbo
{
    public interface IProductRecommendationRepository
    {
        Task<IEnumerable<ProductRecommendation>> GetAllAsync();
    }
}