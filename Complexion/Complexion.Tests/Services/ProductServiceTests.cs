using Complexion.Models.Dbo;
using Complexion.Repository.Dbo;
using Complexion.Services;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace Complexion.Tests.Services
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
        public async Task GIVEN_ProductsExist_WHEN_GettingAllProductsAsync_THEN_ReturnProductsFromRepository()
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
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(expectedProducts));
                _productRepository.Received(1).GetAllProductsAsync(search);
            });
            
        }

        [Test]
        public async Task GIVEN_ProductExists_WHEN_GettingProductAsync_THEN_ReturnProductFromRepository()
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

            _productRepository.GetProductAsync(id).Returns(expectedProduct);

            // Act
            var result = await _productService.GetProductAsync(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(expectedProduct));
                _productRepository.Received(1).GetProductAsync(id);
            });
            
        }

        [Test]
        public void GIVEN_ProductDoesNotExist_WHEN_GettingProductAsync_THEN_ThrowException()
        {
            // Arrange
            var id = Guid.NewGuid();
            _productRepository.GetProductAsync(id).Returns((Product?)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _productService.GetProductAsync(id));
        }

        [Test]
        public async Task GIVEN_ValidDto_WHEN_CreatingProductAsync_THEN_CallRepositoryCreate()
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

