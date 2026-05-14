using Complexion.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Complexion.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkinShadeController : ControllerBase
{
    private readonly ISkinShadeRepository _skinShadeRepository;

    public SkinShadeController(ISkinShadeRepository skinShadeRepository)
    {
        _skinShadeRepository = skinShadeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var skinShades = await _skinShadeRepository.GetAllSkinShades();
        return Ok(skinShades);
    }
}