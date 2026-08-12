using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Dbo
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(string? search)
        {
            using var connection = new SqlConnection(_connectionString);

            if (string.IsNullOrWhiteSpace(search))
            {
                return await connection.QueryAsync<Product>("SELECT * FROM dbo.Product");
            }

            return await connection.QueryAsync<Product>(
                @"SELECT * FROM dbo.Product
                  WHERE Name LIKE @Search
                  OR Brand LIKE @Search",
                new { Search = $"%{search}%" });
        }

        public async Task<Product?> GetProductsByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Product>(
                "SELECT * FROM dbo.Product WHERE ProductId = @Id",
                new { Id = id });
        }

        public async Task CreateProductAsync(CreateProductDto dto)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                @"INSERT INTO dbo.Product (ProductId, CategoryId, PriceTierId, Name, Brand, ShadeName)
                  VALUES (NEWID(), @CategoryId, @PriceTierId, @Name, @Brand, @ShadeName)",
                dto);
        }
    }
}