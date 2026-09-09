using Complexion.Models.Products;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection _connection;

        public ProductRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(string? search)
        {
            var getAllSql = @"SELECT 
                                ProductId, 
                                CategoryId, 
                                PriceTierId, 
                                Name, 
                                Brand, 
                                ShadeName 
                            FROM dbo.Product";

            var seachSql = @"SELECT 
                                ProductId, 
                                CategoryId, 
                                PriceTierId, 
                                Name, 
                                Brand, 
                                ShadeName 
                            FROM dbo.Product
                            WHERE Name LIKE @Search
                            OR Brand LIKE @Search";

            if (string.IsNullOrWhiteSpace(search))
            {
                return await _connection.QueryAsync<Product>(getAllSql);
            }

            return await _connection.QueryAsync<Product>(seachSql, new { Search = $"%{search}%" });
        }

        public async Task<Product?> GetProductAsync(Guid id)
        {
            var sql = @"SELECT 
                            ProductId, 
                            CategoryId, 
                            PriceTierId, 
                            Name, 
                            Brand, 
                            ShadeName 
                        FROM dbo.Product 
                        WHERE ProductId = @Id";

            return await _connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task CreateProductAsync(CreateProductDto dto)
        {
            var sql = @"INSERT INTO dbo.Product 
                        (
                            ProductId, 
                            CategoryId,
                            PriceTierId, 
                            Name, 
                            Brand, 
                            ShadeName
                        )
                        VALUES 
                        (
                            NEWID(), 
                            @CategoryId, 
                            @PriceTierId, 
                            @Name, 
                            @Brand, 
                            @ShadeName
                        )";

            await _connection.ExecuteAsync(sql, dto);
        }
    }
}