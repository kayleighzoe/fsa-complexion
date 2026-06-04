using Complexion.Models.Skin;
using Complexion.Repository.Skin;

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
