using Complexion.Models.Skin;
using Complexion.Repository.Skin;
using FluentValidation;
using Complexion.Exceptions;

namespace Complexion.Services.Skin
{
    public class SkinProfileService : ISkinProfileService
    {
        private readonly ISkinProfileRepository _repository;
        private readonly IValidator<CreateSkinProfileDto> _validator;

        public SkinProfileService(ISkinProfileRepository repository, IValidator<CreateSkinProfileDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<SkinProfile>> GetAllSkinProfilesAsync()
        {
            return await _repository.GetAllSkinProfilesAsync();
        }

        public async Task<SkinProfile?> GetSkinProfileAsync(Guid id)
        {
            var skinProfile = await _repository.GetSkinProfileAsync(id);

            if (skinProfile == null)
            {
                throw new NotFoundException($"SkinProfile with id {id} was not found.");
            }

            return skinProfile;
        }

        public async Task CreateSkinProfileAsync(CreateSkinProfileDto dto)
        {
            var skinProfile = await _validator.ValidateAsync(dto);

            if (!skinProfile.IsValid)
            {
                throw new ValidationException(skinProfile.Errors);
            }

            await _repository.CreateSkinProfileAsync(dto);
        }
    }
}
