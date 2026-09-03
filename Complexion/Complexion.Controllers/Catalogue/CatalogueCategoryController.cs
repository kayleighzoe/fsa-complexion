using Complexion.Services.Catalogue;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Controllers.Catalogue
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CatalogueCategoryController : ControllerBase
    {
        private readonly ICatalogueCategoryService _catalogueCategoryService;

        public CatalogueCategoryController(ICatalogueCategoryService catalogueCategoryService)
        {
            _catalogueCategoryService = catalogueCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _catalogueCategoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
    }
}