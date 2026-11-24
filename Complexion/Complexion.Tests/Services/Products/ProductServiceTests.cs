using Complexion.Models.Products;
using Complexion.Repository.Products;
using Complexion.Services.Products;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using Complexion.Exceptions;

namespace Complexion.Tests.Services.Products
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

        private static CreateProductDto CreateValidDto()
        {
            return new CreateProductDto
            {
                CategoryId = 1,
                PriceTierId = 1,
                Name = "Studio Fix Fluid Foundation",
                Brand = "MAC",
                ShadeName = "NC42"
            };
        }

        [Test]
        public async Task GIVEN_SearchTerm_WHEN_GettingAllProductsAsync_THEN_ReturnSearchResultsFromRepository()
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

            _productRepository.SearchProductsAsync(search).Returns(expectedProducts);

            // Act
            var result = await _productService.GetAllProductsAsync(search);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(expectedProducts));
                _productRepository.Received(1).SearchProductsAsync(search);
            });

        }

        [Test]
        public async Task GIVEN_NoSearchTerm_WHEN_GettingAllProductsAsync_THEN_ReturnAllProductsFromRepository()
        {
            // Arrange
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
                }
            };

            _productRepository.GetAllProductsAsync().Returns(expectedProducts);

            // Act
            var result = await _productService.GetAllProductsAsync(null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(expectedProducts));
                _productRepository.Received(1).GetAllProductsAsync();
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
            Assert.ThrowsAsync<NotFoundException>(async () => await _productService.GetProductAsync(id));
        }

        [Test]
        public async Task GIVEN_ValidDto_WHEN_CreatingProductAsync_THEN_CallRepositoryCreate()
        {
            // Arrange
            var dto = CreateValidDto();

            _createValidator.ValidateAsync(Arg.Any<CreateProductDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());

            // Act
            await _productService.CreateProductAsync(dto);

            // Assert
            await _productRepository.Received(1).CreateProductAsync(dto);
        }

        [Test]
        public async Task GIVEN_InvalidDto_WHEN_CreatingProductAsync_THEN_ThrowValidationExceptionAndDoNotCallRepository()
        {
            // Arrange
            var dto = CreateValidDto();

            _createValidator.ValidateAsync(Arg.Any<CreateProductDto>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult(new[] { new ValidationFailure("Name", "Invalid name") }));

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _productService.CreateProductAsync(dto));
            await _productRepository.DidNotReceive().CreateProductAsync(Arg.Any<CreateProductDto>());
        }

        [Test]
        public async Task GIVEN_InvalidDto_WHEN_GettingOrCreatingProductAsync_THEN_ThrowValidationExceptionAndDoNotCallRepository()
        {
            // Arrange
            var dto = CreateValidDto();

            _createValidator.ValidateAsync(Arg.Any<CreateProductDto>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult(new[] { new ValidationFailure("Name", "Invalid name") }));

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _productService.GetOrCreateProductAsync(dto));
            await _productRepository.DidNotReceive().FindProductAsync(Arg.Any<CreateProductDto>());
            await _productRepository.DidNotReceive().CreateProductAsync(Arg.Any<CreateProductDto>());
        }

        [Test]
        public async Task GIVEN_ProductAlreadyExists_WHEN_GettingOrCreatingProductAsync_THEN_ReturnExistingProductIdAndDoNotCreate()
        {
            // Arrange
            var dto = CreateValidDto();
            var existingProduct = new Product
            {
                ProductId = Guid.NewGuid(),
                CategoryId = 1,
                PriceTierId = 1,
                Name = "Studio Fix Fluid Foundation",
                Brand = "MAC",
                ShadeName = "NC42"
            };

            _createValidator.ValidateAsync(Arg.Any<CreateProductDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _productRepository.FindProductAsync(dto).Returns(existingProduct);

            // Act
            var result = await _productService.GetOrCreateProductAsync(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(existingProduct.ProductId));
                _productRepository.DidNotReceive().CreateProductAsync(Arg.Any<CreateProductDto>());
            });
        }

        [Test]
        public async Task GIVEN_ProductDoesNotExist_WHEN_GettingOrCreatingProductAsync_THEN_CreateProductAndReturnNewId()
        {
            // Arrange
            var dto = CreateValidDto();
            var newProductId = Guid.NewGuid();

            _createValidator.ValidateAsync(Arg.Any<CreateProductDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _productRepository.FindProductAsync(dto).Returns((Product?)null);
            _productRepository.CreateProductAsync(dto).Returns(newProductId);

            // Act
            var result = await _productService.GetOrCreateProductAsync(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(newProductId));
                _productRepository.Received(1).CreateProductAsync(dto);
            });
        }
    }
}