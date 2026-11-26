using Complexion.Exceptions;
using Complexion.Exceptions.Constants;
using Complexion.Models.Skin;
using Complexion.Repository.Skin;

namespace Complexion.Services.Skin
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

        public async Task<SkinProfile?> GetSkinProfileAsync(Guid id)
        {
            var skinProfile = await _repository.GetSkinProfileAsync(id);

            if (skinProfile is null)
            {
                throw new NotFoundException($"SkinProfile with id {id} was not found.");
            }

            return skinProfile;
        }

        public async Task<Guid> GetSkinProfileIdAsync(int shadeId, int undertoneId, bool hasTint)
        {
            var skinProfile = await _repository.GetSkinProfileByCombinationAsync(shadeId, undertoneId, hasTint);

            if (skinProfile is null)
            {
                throw new NotFoundException(ErrorMessages.SkinProfileNotFound);
            }

            return skinProfile.SkinProfileId;
        }
    }
}