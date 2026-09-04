using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;

namespace Complexion.Services.Dbo
{
    public interface ISkinProfileService
    {
        Task<IEnumerable<SkinProfile>> GetAllSkinProfilesAsync();
        Task<SkinProfile?> GetSkinProfileAsync(Guid id);
        Task<SkinProfile> CreateSkinProfileAsync(CreateSkinProfileDto dto);
    }
}