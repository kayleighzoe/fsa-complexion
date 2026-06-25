using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;

namespace Complexion.Services.Dbo
{
    public interface ISkinProfileService
    {
        Task<IEnumerable<SkinProfile>> GetAllAsync();
        Task<SkinProfile?> GetByIdAsync(Guid id);
        Task<SkinProfile> CreateAsync(CreateSkinProfileDto dto);
    }
}