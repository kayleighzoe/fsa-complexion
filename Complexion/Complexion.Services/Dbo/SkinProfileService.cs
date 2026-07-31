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

        public async Task<IEnumerable<SkinProfile>> GetAllSkinProfilesAsync()
        {
            return await _repository.GetAllSkinProfilesAsync();
        }

        public async Task<SkinProfile?> GetSkinProfilesByIdAsync(Guid id)
        {
            var result = await _repository.GetSkinProfilesByIdAsync(id);

            if (result == null)
            {
                throw new KeyNotFoundException($"SkinProfile with id {id} was not found.");
            }

            return result;
        }

        public async Task<SkinProfile> CreateSkinProfileAsync(CreateSkinProfileDto dto)
        {
            return await _repository.CreateSkinProfileAsync(dto);
        }
    }
}
