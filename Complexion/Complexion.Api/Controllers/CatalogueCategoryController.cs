using Complexion.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogueCategoryController : ControllerBase
    {
        private readonly ICatalogueCategoryRepository _catalogueCategoryRepository;

        public CatalogueCategoryController(ICatalogueCategoryRepository skinUndertoneRepository)
        {
            _catalogueCategoryRepository = skinUndertoneRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _catalogueCategoryRepository.GetAllCategories();
            return Ok(categories);
        }
    }
}