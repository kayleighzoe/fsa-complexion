using Complexion.Models.Skin;

namespace Complexion.Services.Skin
{
    public interface ISkinProfileService
    {
        Task<IEnumerable<SkinProfile>> GetAllSkinProfilesAsync();
        Task<SkinProfile?> GetSkinProfileAsync(Guid id);
        Task CreateSkinProfileAsync(CreateSkinProfileDto dto);
    }
}