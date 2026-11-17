using Complexion.Exceptions;
using Complexion.Exceptions.Constants;
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

            if (profile is null)
            {
                throw new NotFoundException(string.Format(ErrorMessages.UserNotFoundTemplate, userId));
            }

            return profile;
        }

        public async Task UpdateProfileAsync(Guid userId, UpdateProfileDto updateProfileDto)
        {
            var userProfile = await _userRepository.GetUserProfileAsync(userId);

            if (userProfile is null)
            {
                throw new NotFoundException(string.Format(ErrorMessages.UserNotFoundTemplate, userId));
            }

            var userWithEmail = await _userRepository.GetUserDetailsByEmailAsync(updateProfileDto.Email);

            if (userWithEmail is not null && userWithEmail.UserId != userId)
            {
                throw new ConflictException(ErrorMessages.EmailTaken);
            }

            var skinProfileId = await _skinProfileService.GetSkinProfileIdAsync(updateProfileDto.ShadeId, updateProfileDto.UndertoneId, updateProfileDto.HasTint);

            await _userRepository.UpdateProfileAsync(userId, skinProfileId, updateProfileDto);
        }
    }
}
