using Complexion.Repository.Catalogue;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Controllers.Catalogue
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CatalogueCategoryController : ControllerBase
    {
        private readonly ICatalogueCategoryRepository _catalogueCategoryRepository;

        public CatalogueCategoryController(ICatalogueCategoryRepository catalogueCategoryRepository)
        {
            _catalogueCategoryRepository = catalogueCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _catalogueCategoryRepository.GetAllCategoriesAsync();

            return Ok(categories);
        }
    }
}