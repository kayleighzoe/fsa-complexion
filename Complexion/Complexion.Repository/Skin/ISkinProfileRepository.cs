using Complexion.Models.Skin;

namespace Complexion.Repository.Skin
{
    public interface ISkinProfileRepository
    {
        Task<IEnumerable<SkinProfile>> GetAllSkinProfilesAsync();
        Task<SkinProfile?> GetSkinProfileAsync(Guid id);
        Task CreateSkinProfileAsync(CreateSkinProfileDto dto);
    }
}