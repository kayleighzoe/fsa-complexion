using Complexion.Models.Products;
using Complexion.Repository.Products;
using Complexion.Services.Products;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using Complexion.Exceptions;

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
        public async Task GIVEN_RecommendationExists_WHEN_GettingProductReccommendationAsync_THEN_ReturnRecommendationFromRepository()
        {
            // Arrange
            var recommendationId = Guid.NewGuid();
            var expectedRecommendation = new ProductRecommendation
            {
                RecommendationId = recommendationId,
                UserId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                SkinProfileId = Guid.NewGuid(),
                Comment = "Perfect shade match",
                CreatedAt = DateTime.UtcNow
            };

            _productRecommendationRepository.GetProductRecommendationAsync(recommendationId).Returns(expectedRecommendation);

            // Act
            var result = await _productRecommendationService.GetProductRecommendationAsync(recommendationId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(expectedRecommendation));
                _productRecommendationRepository.Received(1).GetProductRecommendationAsync(recommendationId);
            });
        }

        [Test]
        public void GIVEN_RecommendationDoesNotExist_WHEN_GettingProductReccommendationAsync_THEN_ThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _productRecommendationRepository.GetProductRecommendationAsync(id).Returns((ProductRecommendation?)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _productRecommendationService.GetProductRecommendationAsync(id));
        }

        [Test]
        public async Task GIVEN_ValidDto_WHEN_CreatingProductReccommendationAsync_THEN_CallRepositoryCreate()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = new CreateProductRecommendationDto
            {
                ProductId = Guid.NewGuid(),
                SkinProfileId = Guid.NewGuid(),
                Comment = "This shade is a great warm-neutral match"
            };

            _createValidator.ValidateAsync(Arg.Any<CreateProductRecommendationDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());

            // Act
            await _productRecommendationService.CreateProductRecommendationAsync(userId, dto);

            // Assert
            await _productRecommendationRepository.Received(1).CreateProductRecommendationAsync(userId, dto);
        }

        [Test]
        public async Task GIVEN_DtoWithNonNullComment_WHEN_UpdatingProductReccommendationAsync_THEN_CallRepositoryUpdate()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var id = Guid.NewGuid();
            var dto = new UpdateProductRecommendationDto { Comment = "Updated: great match for warm undertones" };

            var existingRecommendation = new ProductRecommendation
            {
                RecommendationId = id,
                UserId = userId,
                ProductId = Guid.NewGuid(),
                SkinProfileId = Guid.NewGuid(),
                Comment = "Old comment",
                CreatedAt = DateTime.UtcNow
            };

            _productRecommendationRepository.GetProductRecommendationAsync(id).Returns(existingRecommendation);
            _updateValidator.ValidateAsync(Arg.Any<UpdateProductRecommendationDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());

            // Act
            await _productRecommendationService.UpdateProductRecommendationAsync(userId, id, dto);

            // Assert
            await _productRecommendationRepository.Received(1).UpdateProductRecommendationAsync(id, dto);
        }

        [Test]
        public async Task GIVEN_DtoWithNullComment_WHEN_UpdatingProductReccommendationAsync_THEN_DoesNotCallRepositoryUpdate()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var id = Guid.NewGuid();
            var dto = new UpdateProductRecommendationDto { Comment = null };

            // Act
            await _productRecommendationService.UpdateProductRecommendationAsync(userId, id, dto);

            // Assert
            await _productRecommendationRepository.DidNotReceive().UpdateProductRecommendationAsync(id, dto);
        }
    }
}
