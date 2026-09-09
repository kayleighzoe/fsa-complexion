using Complexion.Models.Products;
using Complexion.Repository.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Products
{
    public class ProductRecommendationRepository : IProductRecommendationRepository
    {
        private readonly IDbContext _dbContext;

        public ProductRecommendationRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductRecommendation>> GetAllProductReccommendationsAsync()
        {
            var sql = @"SELECT 
                            RecommendationId, 
                            UserId, 
                            ProductId, 
                            SkinProfileId, 
                            Comment, 
                            CreatedAt 
                        FROM dbo.ProductRecommendation";

            return await _dbContext.QueryAsync<ProductRecommendation>(sql);
        }

        public async Task<ProductRecommendation?> GetProductReccommendationAsync(Guid id)
        {
            var sql = @"SELECT 
                            RecommendationId, 
                            UserId, 
                            ProductId, 
                            SkinProfileId, 
                            Comment, 
                            CreatedAt 
                        FROM dbo.ProductRecommendation 
                        WHERE RecommendationId = @Id";

            return await _dbContext.QuerySingleOrDefaultAsync<ProductRecommendation>(sql, new { Id = id });
        }

        public async Task CreateProductReccommendationAsync(CreateProductRecommendationDto dto)
        {
            var sql = @"INSERT INTO dbo.ProductRecommendation
                        (
                            RecommendationId, 
                            UserId, 
                            ProductId, 
                            SkinProfileId, 
                            Comment, 
                            CreatedAt
                        )
                        VALUES 
                        (
                            NEWID(), 
                            @UserId, 
                            @ProductId, 
                            @SkinProfileId, 
                            @Comment, 
                            GETUTCDATE()
                        )";

            await _dbContext.ExecuteAsync(sql, dto);
        }

        public async Task UpdateProductReccommendationAsync(Guid id, UpdateProductRecommendationDto dto)
        {
            var sql = @"UPDATE dbo.ProductRecommendation
                        SET Comment = @Comment
                        WHERE RecommendationId = @Id";

            await _dbContext.ExecuteAsync(sql, new { Id = id, dto.Comment });
        }

        public async Task DeleteProductReccommendationAsync(Guid id)
        {
            var sql = @"DELETE FROM dbo.ProductRecommendation 
                        WHERE RecommendationId = @Id";

            await _dbContext.ExecuteAsync(sql, new { Id = id });
        }
    }
}