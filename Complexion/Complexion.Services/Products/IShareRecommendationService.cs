using Complexion.Models.Products;

namespace Complexion.Services.Products
{
    public interface IShareRecommendationService
    {
        Task ShareRecommendationAsync(Guid userId, ShareRecommendationDto dto);
    }
}