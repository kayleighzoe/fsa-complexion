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

        public async Task<IEnumerable<Product>> GetAllAsync(string? search)
        {
            return await _repository.GetAllAsync(search);
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                throw new KeyNotFoundException($"SkinProfile with id {id} was not found.");
            }

            return result;
        }

        public async Task<Product> CreateAsync(CreateProductDto dto)
        {
            return await _repository.CreateAsync(dto);
        }

    }
}