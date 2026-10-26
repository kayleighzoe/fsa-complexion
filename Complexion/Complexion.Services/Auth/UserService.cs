using Complexion.Exceptions;
using Complexion.Models.Auth;
using Complexion.Models.Skin;
using Complexion.Repository.Auth;
using Complexion.Services.Skin;
using FluentValidation;

namespace Complexion.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISkinProfileService _skinProfileService;
        private readonly IValidator<UpdateProfileDto> _updateProfileValidator;

        public UserService(IUserRepository userRepository, ISkinProfileService skinProfileService, IValidator<UpdateProfileDto> updateProfileValidator)
        {
            _userRepository = userRepository;
            _skinProfileService = skinProfileService;
            _updateProfileValidator = updateProfileValidator;
        }

        public async Task<UserProfileDto> GetProfileAsync(Guid userId)
        {
            var profile = await _userRepository.GetUserProfileAsync(userId);

            if (profile == null)
            {
                throw new NotFoundException($"User with id {userId} was not found.");
            }

            return profile;
        }

        public async Task UpdateProfileAsync(Guid userId, UpdateProfileDto updateProfileDto)
        {
            var validationResult = await _updateProfileValidator.ValidateAsync(updateProfileDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingUser = await _userRepository.GetByEmailAsync(updateProfileDto.Email);

            if (existingUser != null && existingUser.UserId != userId)
            {
                throw new ConflictException("Email is already registered.");
            }

            var skinProfileId = await _skinProfileService.GetOrCreateSkinProfileAsync(new CreateSkinProfileDto
            {
                ShadeId = updateProfileDto.ShadeId,
                UndertoneId = updateProfileDto.UndertoneId,
                HasTint = updateProfileDto.HasTint
            });

            var updated = await _userRepository.UpdateProfileAsync(userId, skinProfileId, updateProfileDto);

            if (!updated)
            {
                throw new NotFoundException($"User with id {userId} was not found.");
            }

        }
    }
}
