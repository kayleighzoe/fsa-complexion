using Complexion.Services.Skin;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Controllers.Skin
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SkinShadeController : ControllerBase
    {
        private readonly ISkinShadeService _skinShadeService;

        public SkinShadeController(ISkinShadeService skinShadeService)
        {
            _skinShadeService = skinShadeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkinShades()
        {
            var skinShades = await _skinShadeService.GetAllSkinShadesAsync();

            return Ok(skinShades);
        }
    }
}