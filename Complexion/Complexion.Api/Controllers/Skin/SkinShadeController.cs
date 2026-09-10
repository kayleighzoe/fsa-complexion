using Complexion.Repository.Skin;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers.Skin
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SkinShadeController : ControllerBase
    {
        private readonly ISkinShadeRepository _repository;

        public SkinShadeController(ISkinShadeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkinShades()
        {
            var skinShades = await _repository.GetAllSkinShadesAsync();

            return Ok(skinShades);
        }
    }
}