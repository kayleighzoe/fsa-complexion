using Complexion.Models.Skin;

namespace Complexion.Api.Services
{
    public interface ISkinShadeService
    {
        Task<IEnumerable<SkinShade>> GetAllSkinShades();
    }
}