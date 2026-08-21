using Dapper;
using Microsoft.Data.SqlClient;
using Complexion.Models.Catalogue;

namespace Complexion.Repository.Catalogue
{
    public class CatalogueCategoryRepository : ICatalogueCategoryRepository
    {
        private readonly SqlConnection _connection;

        public CatalogueCategoryRepository(string connectionString)
        {
            _connection= new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<CatalogueCategory>> GetAllCategoriesAsync()
        {
            var sql = "SELECT CategoryId, Name FROM catalogue.Category";

            return await _connection.QueryAsync<CatalogueCategory>(sql);
        }
    }
}
