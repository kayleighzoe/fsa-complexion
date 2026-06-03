using Complexion.Api.Models;

namespace Complexion.Api.Services;

public interface ISkinShadeService
{
    Task<IEnumerable<SkinShade>> GetAllSkinShades();
}