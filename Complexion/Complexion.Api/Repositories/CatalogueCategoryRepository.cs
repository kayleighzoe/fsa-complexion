using Dapper;
using Microsoft.Data.SqlClient;
using Complexion.Api.Models;


namespace Complexion.Api.Repositories
{
    public class CatalogueCategoryRepository : ICatalogueCategoryRepository
    {
        private readonly string _connectionString;

        public CatalogueCategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CatalogueCategory>> GetAllCategories()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CatalogueCategory>("SELECT * FROM catalogue.Category");
        }
    }
}
