using Complexion.Api.Models;

namespace Complexion.Api.Repositories
{
    public interface ICatalogueCategoryRepository
    {
        Task<IEnumerable<CatalogueCategory>> GetAllCategories();
    }
}
