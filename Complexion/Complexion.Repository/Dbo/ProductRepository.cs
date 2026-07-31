using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Complexion.Repository.Dbo
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(string? search)
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

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Product>(
                "SELECT * FROM dbo.Product WHERE ProductId = @Id",
                new { Id = id });
        }

        public async Task<Product> CreateAsync(CreateProductDto dto)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleAsync<Product>(
                @"INSERT INTO dbo.Product (ProductId, CategoryId, PriceTierId, Name, Brand, ShadeName)
                  OUTPUT INSERTED.*
                  VALUES (NEWID(), @CategoryId, @PriceTierId, @Name, @Brand, @ShadeName)",
                dto);
        }
    }
}