using Complexion.Models.Dbo;
using Complexion.DTOs.Dbo;

namespace Complexion.Repository.Dbo
{
    public interface ISkinProfileRepository
    {
        Task<IEnumerable<SkinProfile>> GetAllAsync();
        Task<SkinProfile?> GetByIdAsync(Guid id);
        Task<SkinProfile> CreateAsync(CreateSkinProfileDto dto);
    }
}