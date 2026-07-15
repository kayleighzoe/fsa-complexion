using Dapper;
using Microsoft.Data.SqlClient;
using Complexion.Models.Catalogue;
using Microsoft.Extensions.Configuration;


namespace Complexion.Repository.Catalogue
{
    public class CatalogueCategoryRepository : ICatalogueCategoryRepository
    {
        private readonly string _connectionString;

        public CatalogueCategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<CatalogueCategory>> GetAllCategories()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<CatalogueCategory>("SELECT * FROM catalogue.Category");
        }
    }
}
