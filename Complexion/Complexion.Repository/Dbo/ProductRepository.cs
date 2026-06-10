using Dapper;
using Microsoft.Data.SqlClient;
using Complexion.Models.Dbo;

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
    }
}