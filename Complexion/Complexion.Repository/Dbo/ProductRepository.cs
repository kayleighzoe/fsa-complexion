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
            var getAllSql = "SELECT ProductId, CategoryId, PriceTierId, Name, Brand, ShadeName FROM dbo.Product";
            var seachSql = @"SELECT ProductId, CategoryId, PriceTierId, Name, Brand, ShadeName FROM dbo.Product
                        WHERE Name LIKE @Search
                        OR Brand LIKE @Search";

            using var connection = new SqlConnection(_connectionString);

            if (string.IsNullOrWhiteSpace(search))
            {
                return await connection.QueryAsync<Product>(getAllSql);
            }

            return await connection.QueryAsync<Product>(seachSql, new { Search = $"%{search}%" });
        }

        public async Task<Product?> GetProductsByIdAsync(Guid id)
        {
            var sql = "SELECT ProductId, CategoryId, PriceTierId, Name, Brand, ShadeName FROM dbo.Product WHERE ProductId = @Id";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<Product> CreateProductAsync(CreateProductDto dto)
        {
            var sql = @"INSERT INTO dbo.Product (ProductId, CategoryId, PriceTierId, Name, Brand, ShadeName)
                        OUTPUT INSERTED.*
                        VALUES (NEWID(), @CategoryId, @PriceTierId, @Name, @Brand, @ShadeName)";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleAsync<Product>(sql, dto);
        }
    }
}