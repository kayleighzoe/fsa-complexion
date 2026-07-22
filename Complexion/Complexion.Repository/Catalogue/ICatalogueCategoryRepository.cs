using Complexion.Models.Catalogue;

namespace Complexion.Repository.Catalogue
{
    public interface ICatalogueCategoryRepository
    {
        Task<IEnumerable<CatalogueCategory>> GetAllAsync();
    }
}