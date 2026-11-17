using Complexion.Services.Skin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers.Skin
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SkinProfileController : ControllerBase
    {
        private readonly ISkinProfileService _skinProfileService;

        public SkinProfileController(ISkinProfileService service)
        {
            _skinProfileService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkinProfiles()
        {
            var skinProfiles = await _skinProfileService.GetAllSkinProfilesAsync();

            return Ok(skinProfiles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkinProfile(Guid id)
        {
            var skinProfile = await _skinProfileService.GetSkinProfileAsync(id);

            return Ok(skinProfile);
        }
    }
}