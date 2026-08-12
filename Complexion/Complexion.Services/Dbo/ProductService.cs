using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Complexion.Repository.Dbo;

namespace Complexion.Services.Dbo
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(string? search)
        {
            return await _repository.GetAllProductsAsync(search);
        }

        public async Task<Product?> GetProductsByIdAsync(Guid id)
        {
            var result = await _repository.GetProductsByIdAsync(id);

            if (result == null)
            {
                throw new KeyNotFoundException($"Product with id {id} was not found.");
            }

            return result;
        }

        public async Task CreateProductAsync(CreateProductDto dto)
        {
            await _repository.CreateProductAsync(dto);
        }

    }
}