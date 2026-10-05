using Complexion.Models.Catalogue;
using Complexion.Repository.Data;

namespace Complexion.Repository.Catalogue
{
    public class CatalogueCategoryRepository : ICatalogueCategoryRepository
    {
        private readonly IDbContext _dbContext;

        public CatalogueCategoryRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CatalogueCategory>> GetAllCategoriesAsync()
        {
            const string sql = @"SELECT 
                            CategoryId, 
                            Name 
                        FROM catalogue.Category";

            return await _dbContext.QueryAsync<CatalogueCategory>(sql);
        }
    }
}
