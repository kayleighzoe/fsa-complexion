using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Complexion.Repository.Dbo;
using Complexion.Services.Dbo;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace Complexion.Tests.Services.Dbo
{
    public class ProductServiceTests
    {
        private IProductRepository _productRepository;
        private ProductService _productService;
        private IValidator<CreateProductDto> _createValidator;

        [SetUp]
        public void Setup()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _createValidator = Substitute.For<IValidator<CreateProductDto>>();
            _productService = new ProductService(_productRepository, _createValidator);
        }

        [Test]
        public async Task GivenProductsExistInRepository_WhenGetAllProductsAsync_ReturnsProductsFromRepository()
        {
            // Arrange
            var search = "concealer";
            var expectedProducts = new List<Product>
            {
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = 1,
                    PriceTierId = 1,
                    Name = "Full Coverage Concealer",
                    Brand = "Fenty Beauty",
                    ShadeName = "220"
                },
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    CategoryId = 2,
                    PriceTierId = 2,
                    Name = "Radiant Creamy Concealer",
                    Brand = "NARS",
                    ShadeName = "Vanilla"
                }
            };

            _productRepository.GetAllProductsAsync(search).Returns(expectedProducts);

            // Act
            var result = await _productService.GetAllProductsAsync(search);

            // Assert
            Assert.That(result, Is.EqualTo(expectedProducts));
            await _productRepository.Received(1).GetAllProductsAsync(search);
        }

        [Test]
        public async Task GivenProductExistsInRepository_WhenGetProductsByIdAsync_ReturnsProductFromRepository()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedProduct = new Product
            {
                ProductId = id,
                CategoryId = 1,
                PriceTierId = 1,
                Name = "Pro Filt'r Soft Matte Foundation",
                Brand = "Fenty Beauty",
                ShadeName = "310"
            };

            _productRepository.GetProductsByIdAsync(id).Returns(expectedProduct);

            // Act
            var result = await _productService.GetProductsByIdAsync(id);

            // Assert
            Assert.That(result, Is.EqualTo(expectedProduct));
            await _productRepository.Received(1).GetProductsByIdAsync(id);
        }

        [Test]
        public void GivenProductDoesNotExistInRepository_WhenGetProductsByIdAsync_ThrowsKeyNotFoundException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _productRepository.GetProductsByIdAsync(id).Returns((Product?)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _productService.GetProductsByIdAsync(id));
        }

        [Test]
        public async Task GivenValidDto_WhenCreateProductAsync_CallsRepositoryCreate()
        {
            // Arrange
            var dto = new CreateProductDto
            {
                CategoryId = 1,
                PriceTierId = 1,
                Name = "Studio Fix Fluid Foundation",
                Brand = "MAC",
                ShadeName = "NC42"
            };

            _createValidator.ValidateAsync(Arg.Any<CreateProductDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());

            // Act
            await _productService.CreateProductAsync(dto);

            // Assert
            await _productRepository.Received(1).CreateProductAsync(dto);
        }
    }
}

