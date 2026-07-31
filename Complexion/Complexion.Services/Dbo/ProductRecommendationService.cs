using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Complexion.Repository.Dbo;

namespace Complexion.Services.Dbo
{
    public class ProductRecommendationService : IProductRecommendationService
    {
        private readonly IProductRecommendationRepository _repository;

        public ProductRecommendationService(IProductRecommendationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductRecommendation>> GetAllProductReccommendationsAsync()
        {
            return await _repository.GetAllProductReccommendationsAsync();
        }

        public async Task<ProductRecommendation?> GetProductReccommendationsByIdAsync(Guid id)
        {
            var result = await _repository.GetProductReccommendationsByIdAsync(id);

            if (result == null)
            {
                throw new KeyNotFoundException($"ProductRecommendation with id {id} was not found.");
            }

            return result;
        }

        public async Task<ProductRecommendation> CreateProductReccommendationAsync(CreateProductRecommendationDto dto)
        {
            return await _repository.CreateProductReccommendationAsync(dto);
        }

        public async Task UpdateProductReccommendationAsync(Guid id, UpdateProductRecommendationDto dto)
        {
            if (dto.Comment == null)
            {
                return;
            }

            await _repository.UpdateProductReccommendationAsync(id, dto);
        }

        public async Task DeleteProductReccommendationAsync(Guid id)
        {
            await _repository.DeleteProductReccommendationAsync(id);
        }
    }
}