using Complexion.Exceptions;
using Complexion.Models.Products;
using Complexion.Repository.Lookups;
using Complexion.Services.Products;
using Complexion.Services.Skin;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace Complexion.Tests.Services.Products
{
    public class ShareRecommendationServiceTests
    {
        private IValidator<ShareRecommendationDto> _validator;
        private ILookupRepository _lookupRepository;
        private ISkinProfileService _skinProfileService;
        private IProductService _productService;
        private IProductRecommendationService _productRecommendationService;
        private ShareRecommendationService _shareRecommendationService;

        [SetUp]
        public void Setup()
        {
            _validator = Substitute.For<IValidator<ShareRecommendationDto>>();
            _lookupRepository = Substitute.For<ILookupRepository>();
            _skinProfileService = Substitute.For<ISkinProfileService>();
            _productService = Substitute.For<IProductService>();
            _productRecommendationService = Substitute.For<IProductRecommendationService>();

            _shareRecommendationService = new ShareRecommendationService(
                _validator,
                _lookupRepository,
                _skinProfileService,
                _productService,
                _productRecommendationService);
        }

        private static ShareRecommendationDto CreateValidDto()
        {
            return new ShareRecommendationDto
            {
                CategoryName = "Foundation",
                PriceTierName = "Mid-range",
                Brand = "Test Brand",
                ProductName = "Test Product",
                ShadeName = "Test Shade",
                MatchedShadeName = "Medium",
                MatchedUndertoneName = "Warm",
                HasTint = true,
                Comment = "Great match"
            };
        }

        private static ShareLookupIds CreateLookupIds(int? categoryId = 1, int? priceTierId = 2, int? shadeId = 3, int? undertoneId = 4)
        {
            return new ShareLookupIds
            {
                CategoryId = categoryId,
                PriceTierId = priceTierId,
                ShadeId = shadeId,
                UndertoneId = undertoneId
            };
        }

        [Test]
        public void GIVEN_InvalidDto_WHEN_SharingRecommendationAsync_THEN_ThrowValidationExceptionAndDoNotCallAnything()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = CreateValidDto();

            _validator.ValidateAsync(Arg.Any<ShareRecommendationDto>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult(new[] { new ValidationFailure("Brand", "Brand is required.") }));

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _shareRecommendationService.ShareRecommendationAsync(userId, dto));
            Assert.Multiple(() =>
            {
                _lookupRepository.DidNotReceive().GetShareLookupIdsAsync(Arg.Any<ShareRecommendationDto>());
                _skinProfileService.DidNotReceive().GetSkinProfileIdAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<bool>());
                _productService.DidNotReceive().GetOrCreateProductAsync(Arg.Any<CreateProductDto>());
                _productRecommendationService.DidNotReceive().CreateProductRecommendationAsync(Arg.Any<Guid>(), Arg.Any<CreateProductRecommendationDto>());
            });
        }

        [Test]
        public void GIVEN_CategoryNotFound_WHEN_SharingRecommendationAsync_THEN_ThrowNotFoundExceptionAndDoNotCreateAnything()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = CreateValidDto();

            _validator.ValidateAsync(Arg.Any<ShareRecommendationDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _lookupRepository.GetShareLookupIdsAsync(Arg.Any<ShareRecommendationDto>()).Returns(CreateLookupIds(categoryId: null));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _shareRecommendationService.ShareRecommendationAsync(userId, dto));
            Assert.Multiple(() =>
            {
                _skinProfileService.DidNotReceive().GetSkinProfileIdAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<bool>());
                _productService.DidNotReceive().GetOrCreateProductAsync(Arg.Any<CreateProductDto>());
                _productRecommendationService.DidNotReceive().CreateProductRecommendationAsync(Arg.Any<Guid>(), Arg.Any<CreateProductRecommendationDto>());
            });
        }

        [Test]
        public void GIVEN_PriceTierNotFound_WHEN_SharingRecommendationAsync_THEN_ThrowNotFoundExceptionAndDoNotCreateAnything()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = CreateValidDto();

            _validator.ValidateAsync(Arg.Any<ShareRecommendationDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _lookupRepository.GetShareLookupIdsAsync(Arg.Any<ShareRecommendationDto>()).Returns(CreateLookupIds(priceTierId: null));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _shareRecommendationService.ShareRecommendationAsync(userId, dto));
            Assert.Multiple(() =>
            {
                _skinProfileService.DidNotReceive().GetSkinProfileIdAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<bool>());
                _productService.DidNotReceive().GetOrCreateProductAsync(Arg.Any<CreateProductDto>());
                _productRecommendationService.DidNotReceive().CreateProductRecommendationAsync(Arg.Any<Guid>(), Arg.Any<CreateProductRecommendationDto>());
            });
        }

        [Test]
        public void GIVEN_ShadeNotFound_WHEN_SharingRecommendationAsync_THEN_ThrowNotFoundExceptionAndDoNotCreateAnything()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = CreateValidDto();

            _validator.ValidateAsync(Arg.Any<ShareRecommendationDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _lookupRepository.GetShareLookupIdsAsync(Arg.Any<ShareRecommendationDto>()).Returns(CreateLookupIds(shadeId: null));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _shareRecommendationService.ShareRecommendationAsync(userId, dto));
            Assert.Multiple(() =>
            {
                _skinProfileService.DidNotReceive().GetSkinProfileIdAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<bool>());
                _productService.DidNotReceive().GetOrCreateProductAsync(Arg.Any<CreateProductDto>());
                _productRecommendationService.DidNotReceive().CreateProductRecommendationAsync(Arg.Any<Guid>(), Arg.Any<CreateProductRecommendationDto>());
            });
        }

        [Test]
        public void GIVEN_UndertoneNotFound_WHEN_SharingRecommendationAsync_THEN_ThrowNotFoundExceptionAndDoNotCreateAnything()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = CreateValidDto();

            _validator.ValidateAsync(Arg.Any<ShareRecommendationDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _lookupRepository.GetShareLookupIdsAsync(Arg.Any<ShareRecommendationDto>()).Returns(CreateLookupIds(undertoneId: null));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _shareRecommendationService.ShareRecommendationAsync(userId, dto));
            Assert.Multiple(() =>
            {
                _skinProfileService.DidNotReceive().GetSkinProfileIdAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<bool>());
                _productService.DidNotReceive().GetOrCreateProductAsync(Arg.Any<CreateProductDto>());
                _productRecommendationService.DidNotReceive().CreateProductRecommendationAsync(Arg.Any<Guid>(), Arg.Any<CreateProductRecommendationDto>());
            });
        }

        [Test]
        public async Task GIVEN_ValidDtoAndAllLookupsFound_WHEN_SharingRecommendationAsync_THEN_CreateRecommendationWithResolvedIds()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = CreateValidDto();
            var skinProfileId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            _validator.ValidateAsync(Arg.Any<ShareRecommendationDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _lookupRepository.GetShareLookupIdsAsync(Arg.Any<ShareRecommendationDto>()).Returns(CreateLookupIds(1, 2, 3, 4));
            _skinProfileService.GetSkinProfileIdAsync(3, 4, true).Returns(skinProfileId);
            _productService.GetOrCreateProductAsync(Arg.Any<CreateProductDto>()).Returns(productId);

            // Act
            await _shareRecommendationService.ShareRecommendationAsync(userId, dto);

            // Assert
            Assert.Multiple(() =>
            {
                _skinProfileService.Received(1).GetSkinProfileIdAsync(3, 4, true);
                _productService.Received(1).GetOrCreateProductAsync(Arg.Is<CreateProductDto>(p =>
                    p.CategoryId == 1 &&
                    p.PriceTierId == 2 &&
                    p.Name == dto.ProductName &&
                    p.Brand == dto.Brand &&
                    p.ShadeName == dto.ShadeName));
                _productRecommendationService.Received(1).CreateProductRecommendationAsync(userId, Arg.Is<CreateProductRecommendationDto>(r =>
                    r.ProductId == productId &&
                    r.SkinProfileId == skinProfileId &&
                    r.Comment == dto.Comment));
            });
        }

        [Test]
        public async Task GIVEN_DtoWithExtraWhitespace_WHEN_SharingRecommendationAsync_THEN_TrimValuesBeforeUsingThem()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = new ShareRecommendationDto
            {
                CategoryName = "  Foundation  ",
                PriceTierName = "  Mid-range  ",
                Brand = "  Test Brand  ",
                ProductName = "  Test Product  ",
                ShadeName = "  Test Shade  ",
                MatchedShadeName = "  Medium  ",
                MatchedUndertoneName = "  Warm  ",
                HasTint = false,
                Comment = "  Great match  "
            };

            _validator.ValidateAsync(Arg.Any<ShareRecommendationDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _lookupRepository.GetShareLookupIdsAsync(Arg.Any<ShareRecommendationDto>()).Returns(CreateLookupIds());

            // Act
            await _shareRecommendationService.ShareRecommendationAsync(userId, dto);

            // Assert
            Assert.Multiple(() =>
            {
                _lookupRepository.Received(1).GetShareLookupIdsAsync(Arg.Is<ShareRecommendationDto>(d =>
                    d.CategoryName == "Foundation" &&
                    d.PriceTierName == "Mid-range" &&
                    d.Brand == "Test Brand" &&
                    d.ProductName == "Test Product" &&
                    d.ShadeName == "Test Shade" &&
                    d.MatchedShadeName == "Medium" &&
                    d.MatchedUndertoneName == "Warm" &&
                    d.Comment == "Great match"));
                _productService.Received(1).GetOrCreateProductAsync(Arg.Is<CreateProductDto>(p =>
                    p.Name == "Test Product" &&
                    p.Brand == "Test Brand" &&
                    p.ShadeName == "Test Shade"));
                _productRecommendationService.Received(1).CreateProductRecommendationAsync(userId, Arg.Is<CreateProductRecommendationDto>(r =>
                    r.Comment == "Great match"));
            });
        }

        [Test]
        public async Task GIVEN_NullComment_WHEN_SharingRecommendationAsync_THEN_CreateRecommendationWithNullComment()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = CreateValidDto();
            dto.Comment = null;

            _validator.ValidateAsync(Arg.Any<ShareRecommendationDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _lookupRepository.GetShareLookupIdsAsync(Arg.Any<ShareRecommendationDto>()).Returns(CreateLookupIds());

            // Act
            await _shareRecommendationService.ShareRecommendationAsync(userId, dto);

            // Assert
            await _productRecommendationService.Received(1).CreateProductRecommendationAsync(userId, Arg.Is<CreateProductRecommendationDto>(r => r.Comment == null));
        }
    }
}