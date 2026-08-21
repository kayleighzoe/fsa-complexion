using System.Data.Common;
using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Dbo
{
    public class ProductRecommendationRepository : IProductRecommendationRepository
    {
        private readonly SqlConnection _connection;

        public ProductRecommendationRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<ProductRecommendation>> GetAllProductReccommendationsAsync()
        {
            var sql = "SELECT RecommendationId, UserId, ProductId, SkinProfileId, Comment, CreatedAt FROM dbo.ProductRecommendation";

            return await _connection.QueryAsync<ProductRecommendation>(sql);
        }

        public async Task<ProductRecommendation?> GetProductReccommendationsByIdAsync(Guid id)
        {
            var sql = "SELECT RecommendationId, UserId, ProductId, SkinProfileId, Comment, CreatedAt FROM dbo.ProductRecommendation WHERE RecommendationId = @Id";

            return await _connection.QuerySingleOrDefaultAsync<ProductRecommendation>(sql, new { Id = id });
        }

        public async Task<ProductRecommendation> CreateProductReccommendationAsync(CreateProductRecommendationDto dto)
        {
            var sql = @"INSERT INTO dbo.ProductRecommendation
                        (RecommendationId, UserId, ProductId, SkinProfileId, Comment, CreatedAt)
                        OUTPUT INSERTED.*
                        VALUES (NEWID(), @UserId, @ProductId, @SkinProfileId, @Comment, GETUTCDATE())";

            return await _connection.QuerySingleAsync<ProductRecommendation>(sql, dto);
        }

        public async Task UpdateProductReccommendationAsync(Guid id, UpdateProductRecommendationDto dto)
        {
            var sql = @"UPDATE dbo.ProductRecommendation
                        SET Comment = @Comment
                        WHERE RecommendationId = @Id";

            await _connection.ExecuteAsync(sql, new { Id = id, dto.Comment });
        }

        public async Task DeleteProductReccommendationAsync(Guid id)
        {
            var sql = "DELETE FROM dbo.ProductRecommendation WHERE RecommendationId = @Id";

            await _connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}