using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Complexion.Repository.Dbo;
using Complexion.Services.Dbo;
using NSubstitute;

namespace Complexion.Tests.Services.Dbo
{
    public class ProductReccommendationsServiceTests
    {
        private IProductRecommendationRepository _productRecommendationRepository;
        private ProductRecommendationService _productRecommendationService;

        [SetUp]
        public void Setup()
        {
            _productRecommendationRepository = Substitute.For<IProductRecommendationRepository>();
            _productRecommendationService = new ProductRecommendationService(_productRecommendationRepository);
        }

        [Test]
        public async Task GivenRecommendationsExistInRepository_WhenGetAllProductReccommendationsAsync_ReturnsRecommendationsFromRepository()
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
        public async Task GivenRecommendationExistsInRepository_WhenGetProductReccommendationsByIdAsync_ReturnsRecommendationFromRepository()
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

            _productRecommendationRepository.GetProductReccommendationsByIdAsync(id).Returns(expectedRecommendation);

            // Act
            var result = await _productRecommendationService.GetProductReccommendationsByIdAsync(id);

            // Assert
            Assert.That(result, Is.EqualTo(expectedRecommendation));
            await _productRecommendationRepository.Received(1).GetProductReccommendationsByIdAsync(id);
        }

        [Test]
        public void GivenRecommendationDoesNotExistInRepository_WhenGetProductReccommendationsByIdAsync_ThrowsKeyNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _productRecommendationRepository.GetProductReccommendationsByIdAsync(id).Returns((ProductRecommendation?)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _productRecommendationService.GetProductReccommendationsByIdAsync(id));
        }

        [Test]
        public async Task GivenValidDto_WhenCreateProductReccommendationAsync_ReturnsCreatedRecommendationFromRepository()
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

            _productRecommendationRepository.CreateProductReccommendationAsync(dto).Returns(expectedRecommendation);

            // Act
            var result = await _productRecommendationService.CreateProductReccommendationAsync(dto);

            // Assert
            Assert.That(result, Is.EqualTo(expectedRecommendation));
            await _productRecommendationRepository.Received(1).CreateProductReccommendationAsync(dto);
        }

        [Test]
        public async Task GivenDtoWithNonNullComment_WhenUpdateProductReccommendationAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new UpdateProductRecommendationDto { Comment = "Updated: great match for warm undertones" };

            // Act
            await _productRecommendationService.UpdateProductReccommendationAsync(id, dto);

            // Assert
            await _productRecommendationRepository.Received(1).UpdateProductReccommendationAsync(id, dto);
        }

        [Test]
        public async Task GivenDtoWithNullComment_WhenUpdateProductReccommendationAsync_DoesNotCallRepositoryUpdate()
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
        public async Task GivenValidId_WhenDeleteProductReccommendationAsync_CallsRepositoryDelete()
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
