using System.Runtime.CompilerServices;
using Complexion.Models.Catalogue;
using Complexion.Repository.Catalogue;
using Microsoft.Extensions.Logging;

namespace Complexion.Services.Catalogue
{
    public class CatalogueCategoryService : ICatalogueCategoryService
    {
        private readonly ICatalogueCategoryRepository _catalogueCategoryRepository;
        private readonly ILogger<CatalogueCategoryService> _logger;

        public CatalogueCategoryService(ICatalogueCategoryRepository catalogueCategoryRepository, ILogger<CatalogueCategoryService> logger)
        {
            _catalogueCategoryRepository = catalogueCategoryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogueCategory>> GetAllCategoriesAsync()
        {
            return await _catalogueCategoryRepository.GetAllCategoriesAsync();
        }
    }
}
