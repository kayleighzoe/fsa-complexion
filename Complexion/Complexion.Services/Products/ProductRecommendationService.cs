using Complexion.Models.Products;
using Complexion.Repository.Products;
using FluentValidation;

namespace Complexion.Services.Products
{
    public class ProductRecommendationService : IProductRecommendationService
    {
        private readonly IProductRecommendationRepository _repository;
        private readonly IValidator<CreateProductRecommendationDto> _createValidator;
        private readonly IValidator<UpdateProductRecommendationDto> _updateValidator;

        public ProductRecommendationService(IProductRecommendationRepository repository, IValidator<CreateProductRecommendationDto> createValidator, IValidator<UpdateProductRecommendationDto> updateValidator)
        {
            _repository = repository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IEnumerable<ProductRecommendation>> GetAllProductReccommendationsAsync()
        {
            return await _repository.GetAllProductReccommendationsAsync();
        }

        public async Task<ProductRecommendation?> GetProductReccommendationAsync(Guid id)
        {
            var result = await _repository.GetProductReccommendationAsync(id);

            if (result == null)
            {
                throw new KeyNotFoundException($"ProductRecommendation with id {id} was not found.");
            }

            return result;
        }

        public async Task CreateProductReccommendationAsync(CreateProductRecommendationDto dto)
        {
            var result = await _createValidator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            await _repository.CreateProductReccommendationAsync(dto);
        }

        public async Task UpdateProductReccommendationAsync(Guid id, UpdateProductRecommendationDto dto)
        {
            if (dto.Comment == null)
            {
                return;
            }

            var result = await _updateValidator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            await _repository.UpdateProductReccommendationAsync(id, dto);
        }

        public async Task DeleteProductReccommendationAsync(Guid id)
        {
            await _repository.DeleteProductReccommendationAsync(id);
        }
    }
}