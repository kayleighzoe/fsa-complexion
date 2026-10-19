using Complexion.Exceptions;
using Complexion.Models.Auth;
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

        public async Task<ProductRecommendation?> GetProductRecommendationAsync(Guid id)
        {
            var productRecommendation = await _repository.GetProductRecommendationAsync(id);

            if (productRecommendation == null)
            {
                throw new NotFoundException($"ProductRecommendation with id {id} was not found.");
            }

            return productRecommendation;
        }

        public async Task CreateProductRecommendationAsync(Guid userId, CreateProductRecommendationDto dto)
        {
            var productRecommendation = await _createValidator.ValidateAsync(dto);

            if (!productRecommendation.IsValid)
            {
                throw new ValidationException(productRecommendation.Errors);
            }

            await _repository.CreateProductRecommendationAsync(userId, dto);
        }

        public async Task UpdateProductRecommendationAsync(Guid userId, Guid id, UpdateProductRecommendationDto dto)
        {
            if (dto.Comment is null)
            {
                return;
            }

            var productRecommendation = await _updateValidator.ValidateAsync(dto);

            if (!productRecommendation.IsValid)
            {
                throw new ValidationException(productRecommendation.Errors);
            }

            await GetOwnedRecommendationAsync(userId, id);
            await _repository.UpdateProductRecommendationAsync(id, dto);
        }

        public async Task DeleteProductRecommendationAsync(Guid userId, Guid id)
        {
            await GetOwnedRecommendationAsync(userId, id);
            await _repository.DeleteProductRecommendationAsync(id);
        }

        private async Task<ProductRecommendation> GetOwnedRecommendationAsync(Guid userId, Guid id)
        {
            var recommendation = await _repository.GetProductRecommendationAsync(id);

            if (recommendation == null || recommendation.UserId != userId)
            {
                throw new NotFoundException($"ProductRecommendation with id {id} was not found.");
            }

            return recommendation;
        }

        public async Task<IEnumerable<MyRecommendationDto>> GetMyProductRecommendationsAsync(Guid userId)
        {
            return await _repository.GetMyProductRecommendationsAsync(userId);
        }
    }
}