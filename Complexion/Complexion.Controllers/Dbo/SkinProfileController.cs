using Complexion.DTOs.Dbo;
using Complexion.Repository.Dbo;
using Complexion.Services.Dbo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Controllers.Dbo
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SkinProfileController : ControllerBase
    {
        private readonly ISkinProfileService _service;
        private readonly ISkinProfileRepository _repository;

        public SkinProfileController(ISkinProfileService service, ISkinProfileRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkinProfiles()
        {
            var skinProfiles = await _repository.GetAllSkinProfilesAsync();

            return Ok(skinProfiles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkinProfile(Guid id)
        {
            var skinProfile = await _service.GetSkinProfileAsync(id);

            return Ok(skinProfile);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkinProfile([FromBody] CreateSkinProfileDto dto)
        {
            await _service.CreateSkinProfileAsync(dto);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
