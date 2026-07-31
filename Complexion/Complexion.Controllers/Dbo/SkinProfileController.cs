using Complexion.DTOs.Dbo;
using Complexion.Services.Dbo;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Controllers.Dbo
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SkinProfileController : ControllerBase
    {
        private readonly ISkinProfileService _service;

        public SkinProfileController(ISkinProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllSkinProfilesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetSkinProfilesByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSkinProfileDto dto)
        {
            var result = await _service.CreateSkinProfileAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.SkinProfileId }, result);
        }
    }
}
