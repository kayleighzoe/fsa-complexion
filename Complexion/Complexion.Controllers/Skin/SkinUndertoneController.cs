using Complexion.Repository.Skin;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Controllers.Skin
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SkinUndertoneController : ControllerBase
    {
        private readonly ISkinUndertoneRepository _repository;

        public SkinUndertoneController(ISkinUndertoneRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkinUndertones()
        {
            var skinUndertones = await _repository.GetAllUndertonesAsync();

            return Ok(skinUndertones);
        }
    }
}