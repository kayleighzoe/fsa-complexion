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
            return await _repository.GetAllProductsAsync(search);
        }

        public async Task<Product?> GetProductAsync(Guid id)
        {
            var result = await _repository.GetProductAsync(id);

            if (result == null)
            {
                throw new KeyNotFoundException($"Product with id {id} was not found.");
            }

            return result;
        }

        public async Task CreateProductAsync(CreateProductDto dto)
        {
            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            await _repository.CreateProductAsync(dto);
        }

    }
}