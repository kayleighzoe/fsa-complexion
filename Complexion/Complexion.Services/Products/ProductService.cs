using Complexion.Models.Products;
using Complexion.Repository.Products;
using FluentValidation;

namespace Complexion.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IValidator<CreateProductDto> _validator;

        public ProductService(IProductRepository repository, IValidator<CreateProductDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return await _repository.GetAllProductsAsync();
            }

            return await _repository.SearchProductsAsync(search);
        }

        public async Task<Product?> GetProductAsync(Guid id)
        {
            var product = await _repository.GetProductAsync(id);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with id {id} was not found.");
            }

            return product;
        }

        public async Task CreateProductAsync(CreateProductDto dto)
        {
            var product = await _validator.ValidateAsync(dto);

            if (!product.IsValid)
            {
                throw new ValidationException(product.Errors);
            }

            await _repository.CreateProductAsync(dto);
        }

    }
}