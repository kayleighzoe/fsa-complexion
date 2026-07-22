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

        public async Task<IEnumerable<ProductRecommendation>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProductRecommendation?> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                throw new KeyNotFoundException($"ProductRecommendation with id {id} was not found.");
            }

            return result;
        }

        public async Task<ProductRecommendation> CreateAsync(CreateProductRecommendationDto dto)
        {
            return await _repository.CreateAsync(dto);
        }

        public async Task<ProductRecommendation> UpdateAsync(Guid id, UpdateProductRecommendationDto dto)
        {
            return await _repository.UpdateAsync(id, dto);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}