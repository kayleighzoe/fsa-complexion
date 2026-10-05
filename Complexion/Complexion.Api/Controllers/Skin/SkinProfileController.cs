using Complexion.Models.Skin;
using Complexion.Repository.Skin;
using Complexion.Services.Skin;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers.Skin
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SkinProfileController : ControllerBase
    {
        private readonly ISkinProfileService _skinProfileService;
        private readonly ISkinProfileRepository _skinProfileRepository;

        public SkinProfileController(ISkinProfileService service, ISkinProfileRepository repository)
        {
            _skinProfileService = service;
            _skinProfileRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkinProfiles()
        {
            var skinProfiles = await _skinProfileRepository.GetAllSkinProfilesAsync();

            return Ok(skinProfiles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkinProfile(Guid id)
        {
            var skinProfile = await _skinProfileService.GetSkinProfileAsync(id);

            return Ok(skinProfile);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkinProfile([FromBody] CreateSkinProfileDto dto)
        {
            await _skinProfileService.CreateSkinProfileAsync(dto);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
