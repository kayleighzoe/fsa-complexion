using Complexion.Exceptions;
using Complexion.Models.Products;
using Complexion.Models.Skin;
using Complexion.Repository.Lookups;
using Complexion.Services.Skin;
using FluentValidation;
using FluentValidation.Results;

namespace Complexion.Services.Products
{
    public class ShareRecommendationService : IShareRecommendationService
    {
        private readonly IValidator<ShareRecommendationDto> _validator;
        private readonly ILookupRepository _lookupRepository;
        private readonly ISkinProfileService _skinProfileService;
        private readonly IProductService _productService;
        private readonly IProductRecommendationService _productRecommendationService;

        public ShareRecommendationService(
            IValidator<ShareRecommendationDto> validator,
            ILookupRepository lookupRepository,
            ISkinProfileService skinProfileService,
            IProductService productService,
            IProductRecommendationService productRecommendationService)
        {
            _validator = validator;
            _lookupRepository = lookupRepository;
            _skinProfileService = skinProfileService;
            _productService = productService;
            _productRecommendationService = productRecommendationService;
        }

        public async Task ShareRecommendationAsync(Guid userId, ShareRecommendationDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var cleaned = dto with
            {
                CategoryName = dto.CategoryName.Trim(),
                PriceTierName = dto.PriceTierName.Trim(),
                Brand = dto.Brand.Trim(),
                ProductName = dto.ProductName.Trim(),
                ShadeName = dto.ShadeName.Trim(),
                MatchedShadeName = dto.MatchedShadeName.Trim(),
                MatchedUndertoneName = dto.MatchedUndertoneName.Trim(),
                Comment = dto.Comment?.Trim()
            };

            var ids = await _lookupRepository.GetShareLookupIdsAsync(cleaned);

            if (ids.CategoryId is null || ids.PriceTierId is null || ids.ShadeId is null || ids.UndertoneId is null)
            {
                throw new NotFoundException("Category, price tier, skin shade or undertone was not found.");
            }

            var skinProfileId = await _skinProfileService.GetOrCreateSkinProfileAsync(new CreateSkinProfileDto
            {
                ShadeId = ids.ShadeId.GetValueOrDefault(),
                UndertoneId = ids.UndertoneId.GetValueOrDefault(),
                HasTint = cleaned.HasTint
            });

            var productId = await _productService.GetOrCreateProductAsync(new CreateProductDto
            {
                CategoryId = ids.CategoryId.GetValueOrDefault(),
                PriceTierId = ids.PriceTierId.GetValueOrDefault(),
                Name = cleaned.ProductName,
                Brand = cleaned.Brand,
                ShadeName = cleaned.ShadeName
            });


            await _productRecommendationService.CreateProductRecommendationAsync(userId, new CreateProductRecommendationDto
            {
                ProductId = productId,
                SkinProfileId = skinProfileId,
                Comment = cleaned.Comment
            });
        }
    }
}