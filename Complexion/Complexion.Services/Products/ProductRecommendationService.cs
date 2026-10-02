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

        public async Task<ProductRecommendation?> GetProductReccommendationAsync(Guid id)
        {
            var productRecommendation = await _repository.GetProductReccommendationAsync(id);

            if (productRecommendation == null)
            {
                throw new KeyNotFoundException($"ProductRecommendation with id {id} was not found.");
            }

            return productRecommendation;
        }

        public async Task CreateProductReccommendationAsync(CreateProductRecommendationDto dto)
        {
            var productRecommendation = await _createValidator.ValidateAsync(dto);

            if (!productRecommendation.IsValid)
            {
                throw new ValidationException(productRecommendation.Errors);
            }

            await _repository.CreateProductReccommendationAsync(dto);
        }

        public async Task UpdateProductReccommendationAsync(Guid id, UpdateProductRecommendationDto dto)
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

            await _repository.UpdateProductReccommendationAsync(id, dto);
        }
    }
}