using Complexion.Services.Skin;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Controllers.Skin
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SkinUndertoneController : ControllerBase
    {
        private readonly ISkinUndertoneService _skinUndertoneService;

        public SkinUndertoneController(ISkinUndertoneService skinUndertoneService)
        {
            _skinUndertoneService = skinUndertoneService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkinUndertones()
        {
            var skinUndertones = await _skinUndertoneService.GetAllUndertonesAsync();
            return Ok(skinUndertones);
        }
    }
}