using Complexion.Models.Dbo;

namespace Complexion.Repository.Dbo
{
    public interface ISkinProfileRepository
    {
        Task<IEnumerable<SkinProfile>> GetAllSkinProfilesAsync();
        Task<SkinProfile?> GetSkinProfileAsync(Guid id);
        Task CreateSkinProfileAsync(CreateSkinProfileDto dto);
    }
}