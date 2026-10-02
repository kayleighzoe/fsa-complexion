using Complexion.Models.Products;
using Complexion.Repository.Data;

namespace Complexion.Repository.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContext _dbContext;

        public ProductRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            const string sql = @"SELECT 
                            ProductId, 
                            CategoryId, 
                            PriceTierId, 
                            Name, 
                            Brand, 
                            ShadeName 
                        FROM dbo.Product";

            return await _dbContext.QueryAsync<Product>(sql);
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string search)
        {
            const string sql = @"SELECT 
                            ProductId, 
                            CategoryId, 
                            PriceTierId, 
                            Name, 
                            Brand, 
                            ShadeName 
                        FROM dbo.Product
                        WHERE Name LIKE @Search
                        OR Brand LIKE @Search";

            return await _dbContext.QueryAsync<Product>(sql, new { Search = $"%{search}%" });
        }

        public async Task<Product?> GetProductAsync(Guid id)
        {
            const string sql = @"SELECT 
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
            const string sql = @"INSERT INTO dbo.Product 
                        (
                            CategoryId,
                            PriceTierId, 
                            Name, 
                            Brand, 
                            ShadeName
                        )
                        VALUES 
                        ( 
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