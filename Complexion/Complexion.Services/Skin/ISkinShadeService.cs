using Complexion.Models.Skin;

namespace Complexion.Services.Skin
{
    public interface ISkinShadeService
    {
        Task<IEnumerable<SkinShade>> GetAllSkinShadesAsync();
    }
}