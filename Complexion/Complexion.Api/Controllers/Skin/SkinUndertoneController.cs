using Complexion.Repository.Skin;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers.Skin
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SkinUndertoneController : ControllerBase
    {
        private readonly ISkinUndertoneRepository _skinUndertoneRepository;

        public SkinUndertoneController(ISkinUndertoneRepository repository)
        {
            _skinUndertoneRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkinUndertones()
        {
            var skinUndertones = await _skinUndertoneRepository.GetAllUndertonesAsync();

            return Ok(skinUndertones);
        }
    }
}