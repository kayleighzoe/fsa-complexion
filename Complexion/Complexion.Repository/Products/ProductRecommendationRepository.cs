using Complexion.Models.Products;
using Complexion.Repository.Data;

namespace Complexion.Repository.Products
{
    public class ProductRecommendationRepository : IProductRecommendationRepository
    {
        private readonly IDbContext _dbContext;

        public ProductRecommendationRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductRecommendation>> GetAllProductRecommendationsAsync()
        {
            const string sql = @"SELECT 
                                    RecommendationId, 
                                    UserId, 
                                    ProductId, 
                                    SkinProfileId, 
                                    Comment, 
                                    CreatedAt 
                                FROM dbo.ProductRecommendation";

            return await _dbContext.QueryAsync<ProductRecommendation>(sql);
        }

        public async Task<ProductRecommendation?> GetProductRecommendationAsync(Guid id)
        {
            const string sql = @"SELECT 
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

        public async Task CreateProductRecommendationAsync(Guid userId, CreateProductRecommendationDto dto)
        {
            const string sql = @"INSERT INTO dbo.ProductRecommendation
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

            await _dbContext.ExecuteAsync(sql, new { UserId = userId, dto.ProductId, dto.SkinProfileId, dto.Comment });
        }

        public async Task UpdateProductRecommendationAsync(Guid id, UpdateProductRecommendationDto dto)
        {
            const string sql = @"UPDATE dbo.ProductRecommendation
                                SET Comment = @Comment
                                WHERE RecommendationId = @Id";

            await _dbContext.ExecuteAsync(sql, new { Id = id, dto.Comment });
        }

        public async Task DeleteProductRecommendationAsync(Guid id)
        {
            const string sql = @"DELETE FROM dbo.ProductRecommendation 
                                WHERE RecommendationId = @Id";

            await _dbContext.ExecuteAsync(sql, new { Id = id });
        }
    }
}