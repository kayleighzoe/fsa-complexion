using Complexion.Models.Skin;
using Complexion.Repository.Skin;

namespace Complexion.Services.Skin;

public class SkinShadeService : ISkinShadeService
{
    private readonly ISkinShadeRepository _skinShadeRepository;

    public SkinShadeService(ISkinShadeRepository skinShadeRepository)
    {
        _skinShadeRepository = skinShadeRepository;
    }

    public async Task<IEnumerable<SkinShade>> GetAllSkinShadesAsync()
    {
        return await _skinShadeRepository.GetAllSkinShadesAsync();
    }
}
