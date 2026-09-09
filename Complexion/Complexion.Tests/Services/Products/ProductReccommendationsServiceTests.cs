using Complexion.Models.Products;
using Complexion.Repository.Products;
using Complexion.Services.Products;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace Complexion.Tests.Services.Products
{
    public class ProductReccommendationsServiceTests
    {
        private IProductRecommendationRepository _productRecommendationRepository;
        private ProductRecommendationService _productRecommendationService;
        private IValidator<UpdateProductRecommendationDto> _updateValidator;
        private IValidator<CreateProductRecommendationDto> _createValidator;

        [SetUp]
        public void Setup()
        {
            _productRecommendationRepository = Substitute.For<IProductRecommendationRepository>();
            _createValidator = Substitute.For<IValidator<CreateProductRecommendationDto>>();
            _updateValidator = Substitute.For<IValidator<UpdateProductRecommendationDto>>();

            _productRecommendationService = new ProductRecommendationService(_productRecommendationRepository, _createValidator, _updateValidator);

        }

        [Test]
        public async Task GIVEN_RecommendationsExist_WHEN_GettingAllProductReccommendationsAsync_THEN_ReturnRecommendationsFromRepository()
        {
            // Arrange
            var expectedRecommendations = new List<ProductRecommendation>
            {
                new ProductRecommendation
                {
                    RecommendationId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    ProductId = Guid.NewGuid(),
                    SkinProfileId = Guid.NewGuid(),
                    Comment = "Great match for cool undertones",
                    CreatedAt = DateTime.UtcNow
                },
                new ProductRecommendation
                {
                    RecommendationId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    ProductId = Guid.NewGuid(),
                    SkinProfileId = Guid.NewGuid(),
                    Comment = null,
                    CreatedAt = DateTime.UtcNow
                }
            };

            _productRecommendationRepository.GetAllProductReccommendationsAsync().Returns(expectedRecommendations);

            // Act
            var result = await _productRecommendationService.GetAllProductReccommendationsAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedRecommendations));
            await _productRecommendationRepository.Received(1).GetAllProductReccommendationsAsync();
        }

        [Test]
        public async Task GIVEN_RecommendationExists_WHEN_GettingProductReccommendationAsync_THEN_ReturnRecommendationFromRepository()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedRecommendation = new ProductRecommendation
            {
                RecommendationId = id,
                UserId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                SkinProfileId = Guid.NewGuid(),
                Comment = "Perfect shade match",
                CreatedAt = DateTime.UtcNow
            };

            _productRecommendationRepository.GetProductReccommendationAsync(id).Returns(expectedRecommendation);

            // Act
            var result = await _productRecommendationService.GetProductReccommendationAsync(id);

            // Assert
            Assert.That(result, Is.EqualTo(expectedRecommendation));
            await _productRecommendationRepository.Received(1).GetProductReccommendationAsync(id);
        }

        [Test]
        public void GIVEN_RecommendationDoesNotExist_WHEN_GettingProductReccommendationAsync_THEN_ThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _productRecommendationRepository.GetProductReccommendationAsync(id).Returns((ProductRecommendation?)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _productRecommendationService.GetProductReccommendationAsync(id));
        }

        [Test]
        public async Task GIVEN_ValidDto_WHEN_CreatingProductReccommendationAsync_THEN_CallRepositoryCreate()
        {
            // Arrange
            var dto = new CreateProductRecommendationDto
            {
                UserId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                SkinProfileId = Guid.NewGuid(),
                Comment = "This shade is a great warm-neutral match"
            };

            var expectedRecommendation = new ProductRecommendation
            {
                RecommendationId = Guid.NewGuid(),
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                SkinProfileId = dto.SkinProfileId,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            _createValidator.ValidateAsync(Arg.Any<CreateProductRecommendationDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());

            // Act
            await _productRecommendationService.CreateProductReccommendationAsync(dto);

            // Assert
            await _productRecommendationRepository.Received(1).CreateProductReccommendationAsync(dto);
        }

        [Test]
        public async Task GIVEN_DtoWithNonNullComment_WHEN_UpdatingProductReccommendationAsync_THEN_CallRepositoryUpdate()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new UpdateProductRecommendationDto { Comment = "Updated: great match for warm undertones" };

            _createValidator.ValidateAsync(Arg.Any<CreateProductRecommendationDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _updateValidator.ValidateAsync(Arg.Any<UpdateProductRecommendationDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());

            // Act
            await _productRecommendationService.UpdateProductReccommendationAsync(id, dto);

            // Assert
            await _productRecommendationRepository.Received(1).UpdateProductReccommendationAsync(id, dto);
        }

        [Test]
        public async Task GIVEN_DtoWithNullComment_WHEN_UpdatingProductReccommendationAsync_THEN_DoesNotCallRepositoryUpdate()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new UpdateProductRecommendationDto { Comment = null };

            // Act
            await _productRecommendationService.UpdateProductReccommendationAsync(id, dto);

            // Assert
            await _productRecommendationRepository.DidNotReceive().UpdateProductReccommendationAsync(id, dto);
        }

        [Test]
        public async Task GIVEN_ValidId_WHEN_DeletingProductReccommendationAsync_THEN_CallRepositoryDelete()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            await _productRecommendationService.DeleteProductReccommendationAsync(id);

            // Assert
            await _productRecommendationRepository.Received(1).DeleteProductReccommendationAsync(id);
        }
    }
}
