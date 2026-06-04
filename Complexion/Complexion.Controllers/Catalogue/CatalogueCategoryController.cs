using Complexion.Services.Catalogue;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogueCategoryController : ControllerBase
    {
        private readonly ICatalogueCategoryService _catalogueCategoryService;

        public CatalogueCategoryController(ICatalogueCategoryService catalogueCategoryService)
        {
            _catalogueCategoryService = catalogueCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _catalogueCategoryService.GetAllAsync();
            return Ok(categories);
        }
    }
}