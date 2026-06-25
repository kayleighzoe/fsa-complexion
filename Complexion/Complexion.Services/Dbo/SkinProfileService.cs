using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Complexion.Repository.Dbo;

namespace Complexion.Services.Dbo
{
    public class SkinProfileService : ISkinProfileService
    {
        private readonly ISkinProfileRepository _repository;

        public SkinProfileService(ISkinProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SkinProfile>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<SkinProfile?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<SkinProfile> CreateAsync(CreateSkinProfileDto dto)
        {
            return await _repository.CreateAsync(dto);
        }
    }
}
