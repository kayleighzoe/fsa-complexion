using Complexion.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkinUndertoneController : ControllerBase
{
    private readonly ISkinUndertoneRepository _skinUndertoneRepository;

    public SkinUndertoneController(ISkinUndertoneRepository skinUndertoneRepository)
    {
        _skinUndertoneRepository = skinUndertoneRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var skinUndertones = await _skinUndertoneRepository.GetAllSkinUndertones();
        return Ok(skinUndertones);
    }
}
