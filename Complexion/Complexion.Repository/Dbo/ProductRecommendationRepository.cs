using Dapper;
using Microsoft.Data.SqlClient;
using Complexion.Models.Dbo;

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
    }
}