using Complexion.Models.Catalogue;
using Complexion.Repository.Catalogue;

namespace Complexion.Services.Catalogue
{
    public class CatalogueCategoryService : ICatalogueCategoryService
    {
        private readonly ICatalogueCategoryRepository _catalogueCategoryRepository;

        public CatalogueCategoryService(ICatalogueCategoryRepository catalogueCategoryRepository)
        {
            _catalogueCategoryRepository = catalogueCategoryRepository;
        }

        public async Task<IEnumerable<CatalogueCategory>> GetAllCategoriesAsync()
        {
            return await _catalogueCategoryRepository.GetAllCategoriesAsync();
        }
    }
}
