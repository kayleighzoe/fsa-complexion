using Complexion.Models.Dbo;
using Complexion.DTOs.Dbo;

namespace Complexion.Repository.Dbo
{
    public interface ISkinProfileRepository
    {
        Task<IEnumerable<SkinProfile>> GetAllSkinProfilesAsync();
        Task<SkinProfile?> GetSkinProfilesByIdAsync(Guid id);
        Task<SkinProfile> CreateSkinProfileAsync(CreateSkinProfileDto dto);
    }
}