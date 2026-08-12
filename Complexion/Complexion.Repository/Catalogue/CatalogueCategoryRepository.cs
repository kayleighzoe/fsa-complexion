using Dapper;
using Microsoft.Data.SqlClient;
using Complexion.Models.Catalogue;

namespace Complexion.Repository.Catalogue
{
    public class CatalogueCategoryRepository : ICatalogueCategoryRepository
    {
        private readonly string _connectionString;

        public CatalogueCategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CatalogueCategory>> GetAllCategoriesAsync()
        {
            var sql = "SELECT * FROM catalogue.Category";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CatalogueCategory>(sql);
        }
    }
}
