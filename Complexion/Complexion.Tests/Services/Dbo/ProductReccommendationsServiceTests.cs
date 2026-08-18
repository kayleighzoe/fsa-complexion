using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Complexion.Repository.Dbo;
using Complexion.Services.Dbo;
using NSubstitute;

namespace Complexion.Tests.Services.Dbo
{
    public class ProductReccommendationsServiceTests
    {
        private IProductRecommendationRepository _repository;
        private ProductRecommendationService _service;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IProductRecommendationRepository>();
            _service = new ProductRecommendationService(_repository);
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

            _repository.GetAllProductReccommendationsAsync().Returns(expectedRecommendations);

            // Act
            var result = await _service.GetAllProductReccommendationsAsync();

            // Assert
            Assert.That(result, Is.EqualTo(expectedRecommendations));
            await _repository.Received(1).GetAllProductReccommendationsAsync();
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

            _repository.GetProductReccommendationsByIdAsync(id).Returns(expectedRecommendation);

            // Act
            var result = await _service.GetProductReccommendationsByIdAsync(id);

            // Assert
            Assert.That(result, Is.EqualTo(expectedRecommendation));
            await _repository.Received(1).GetProductReccommendationsByIdAsync(id);
        }

        [Test]
        public void GivenRecommendationDoesNotExistInRepository_WhenGetProductReccommendationsByIdAsync_ThrowsKeyNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repository.GetProductReccommendationsByIdAsync(id).Returns((ProductRecommendation?)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.GetProductReccommendationsByIdAsync(id));
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

            _repository.CreateProductReccommendationAsync(dto).Returns(expectedRecommendation);

            // Act
            var result = await _service.CreateProductReccommendationAsync(dto);

            // Assert
            Assert.That(result, Is.EqualTo(expectedRecommendation));
            await _repository.Received(1).CreateProductReccommendationAsync(dto);
        }

        [Test]
        public async Task GivenDtoWithNonNullComment_WhenUpdateProductReccommendationAsync_CallsRepositoryUpdate()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new UpdateProductRecommendationDto { Comment = "Updated: great match for warm undertones" };

            // Act
            await _service.UpdateProductReccommendationAsync(id, dto);

            // Assert
            await _repository.Received(1).UpdateProductReccommendationAsync(id, dto);
        }

        [Test]
        public async Task GivenDtoWithNullComment_WhenUpdateProductReccommendationAsync_DoesNotCallRepositoryUpdate()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new UpdateProductRecommendationDto { Comment = null };

            // Act
            await _service.UpdateProductReccommendationAsync(id, dto);

            // Assert
            await _repository.DidNotReceive().UpdateProductReccommendationAsync(id, dto);
        }

        [Test]
        public async Task GivenValidId_WhenDeleteProductReccommendationAsync_CallsRepositoryDelete()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            await _service.DeleteProductReccommendationAsync(id);

            // Assert
            await _repository.Received(1).DeleteProductReccommendationAsync(id);
        }
    }
}
