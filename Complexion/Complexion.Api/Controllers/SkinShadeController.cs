using Complexion.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkinShadeController : ControllerBase
    {
        private readonly ISkinShadeService _skinShadeService;

        public SkinShadeController(ISkinShadeService skinShadeService)
        {
            _skinShadeService = skinShadeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var skinShades = await _skinShadeService.GetAllSkinShades();
            return Ok(skinShades);
        }
    }
}