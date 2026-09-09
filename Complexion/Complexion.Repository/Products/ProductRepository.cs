using Complexion.Models.Products;
using Complexion.Repository.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContext _dbContext;

        public ProductRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
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
                return await _dbContext.QueryAsync<Product>(getAllSql);
            }

            return await _dbContext.QueryAsync<Product>(seachSql, new { Search = $"%{search}%" });
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

            return await _dbContext.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
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

            await _dbContext.ExecuteAsync(sql, dto);
        }
    }
}