using Complexion.Models.Skin;

namespace Complexion.Repository.Skin
{
    public interface ISkinProfileRepository
    {
        Task<IEnumerable<SkinProfile>> GetAllSkinProfilesAsync();
        Task<SkinProfile?> GetSkinProfileAsync(Guid id);
        Task<Guid> CreateSkinProfileAsync(CreateSkinProfileDto dto);
        Task<SkinProfile?> GetSkinProfileByCombinationAsync(int shadeId, int undertoneId, bool hasTint);
    }
}