using Complexion.Repository.Skin;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers.Skin
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SkinShadeController : ControllerBase
    {
        private readonly ISkinShadeRepository _skinShadeRepository;

        public SkinShadeController(ISkinShadeRepository repository)
        {
            _skinShadeRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkinShades()
        {
            var skinShades = await _skinShadeRepository.GetAllSkinShadesAsync();

            return Ok(skinShades);
        }
    }
}