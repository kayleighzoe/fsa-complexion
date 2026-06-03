using Complexion.Api.Models;
using Complexion.Api.Repositories;

namespace Complexion.Api.Services;

public class SkinShadeService : ISkinShadeService
{
    private readonly ISkinShadeRepository _skinShadeRepository;

    public SkinShadeService(ISkinShadeRepository skinShadeRepository)
    {
        _skinShadeRepository = skinShadeRepository;
    }

    public async Task<IEnumerable<SkinShade>> GetAllSkinShades()
    {
        return await _skinShadeRepository.GetAllSkinShades();
    }
}
