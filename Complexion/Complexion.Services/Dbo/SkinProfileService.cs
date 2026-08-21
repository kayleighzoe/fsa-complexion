using Complexion.DTOs.Dbo;
using Complexion.Models.Dbo;
using Complexion.Repository.Dbo;
using FluentValidation;

namespace Complexion.Services.Dbo
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
            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            return await _repository.CreateSkinProfileAsync(dto);
        }
    }
}
