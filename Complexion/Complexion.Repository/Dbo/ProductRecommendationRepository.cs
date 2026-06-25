using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Complexion.Repository.Dbo
{
    public class ProductRecommendationRepository : IProductRecommendationRepository
    {
        private readonly string _connectionString;

        public ProductRecommendationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<ProductRecommendation>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<ProductRecommendation>(
                "SELECT * FROM dbo.ProductRecommendation");
        }

        public async Task<ProductRecommendation?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleOrDefaultAsync<ProductRecommendation>(
                "SELECT * FROM dbo.ProductRecommendation WHERE RecommendationId = @Id",
                new { Id = id });
        }

        public async Task<ProductRecommendation> CreateAsync(CreateProductRecommendationDto dto)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleAsync<ProductRecommendation>(
                @"INSERT INTO dbo.ProductRecommendation
                    (RecommendationId, UserId, ProductId, SkinProfileId, Comment, CreatedAt)
                  OUTPUT INSERTED.*
                  VALUES (NEWID(), @UserId, @ProductId, @SkinProfileId, @Comment, GETUTCDATE())",
                dto);
        }

        public async Task<ProductRecommendation> UpdateAsync(Guid id, UpdateProductRecommendationDto dto)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QuerySingleAsync<ProductRecommendation>(
                @"UPDATE dbo.ProductRecommendation
                  SET Comment = @Comment
                  OUTPUT INSERTED.*
                  WHERE RecommendationId = @Id",
                new { Id = id, dto.Comment });
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "DELETE FROM dbo.ProductRecommendation WHERE RecommendationId = @Id",
                new { Id = id });
        }
    }
}