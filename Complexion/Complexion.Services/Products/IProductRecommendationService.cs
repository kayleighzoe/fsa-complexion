using Complexion.Models.Products;

namespace Complexion.Services.Products
{
    public interface IProductRecommendationService
    {
        Task<ProductRecommendation?> GetProductReccommendationAsync(Guid id);
        Task CreateProductReccommendationAsync(CreateProductRecommendationDto dto);
        Task UpdateProductReccommendationAsync(Guid id, UpdateProductRecommendationDto dto);
        Task DeleteProductReccommendationAsync(Guid id);
    }
}