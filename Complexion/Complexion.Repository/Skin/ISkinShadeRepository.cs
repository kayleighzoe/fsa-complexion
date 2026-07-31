using Complexion.Models.Skin;

namespace Complexion.Repository.Skin
{
    public interface ISkinShadeRepository
    {
        Task<IEnumerable<SkinShade>> GetAllSkinShadesAsync();
    }
}
