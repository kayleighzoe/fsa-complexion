using Complexion.Models.Catalogue;

namespace Complexion.Services.Catalogue
{
    public interface ICatalogueCategoryService
    {
        Task<IEnumerable<CatalogueCategory>> GetAllAsync();
    }
}