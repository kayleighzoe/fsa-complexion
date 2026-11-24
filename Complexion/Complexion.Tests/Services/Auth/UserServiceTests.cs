using Complexion.Exceptions;
using Complexion.Exceptions.Constants;
using Complexion.Models.Auth;
using Complexion.Repository.Auth;
using Complexion.Services.Auth;
using Complexion.Services.Skin;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace Complexion.Tests.Services.Auth
{
    public class UserServiceTests
    {
        private IUserRepository _userRepository;
        private ISkinProfileService _skinProfileService;
        private IValidator<UpdateProfileDto> _updateProfileValidator;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _skinProfileService = Substitute.For<ISkinProfileService>();
            _updateProfileValidator = Substitute.For<IValidator<UpdateProfileDto>>();

            _userService = new UserService(_userRepository, _skinProfileService, _updateProfileValidator);
        }

        private static UserProfileDto CreateProfileDto()
        {
            return new UserProfileDto
            {
                FirstName = "Test",
                Surname = "User",
                Email = "test@example.com",
                ShadeName = "Medium",
                UndertoneName = "Warm",
                HasTint = true
            };
        }

        private static UpdateProfileDto CreateUpdateProfileDto()
        {
            return new UpdateProfileDto
            {
                FirstName = "Test",
                Surname = "User",
                Email = "test@example.com",
                ShadeId = 3,
                UndertoneId = 4,
                HasTint = true
            };
        }

        private static User CreateUser(Guid userId)
        {
            return new User
            {
                UserId = userId,
                Username = "Test123",
                Email = "test@example.com"
            };
        }

        [Test]
        public async Task GIVEN_ProfileExists_WHEN_GettingProfileAsync_THEN_ReturnProfileFromRepository()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedProfile = CreateProfileDto();

            _userRepository.GetUserProfileAsync(userId).Returns(expectedProfile);

            // Act
            var result = await _userService.GetProfileAsync(userId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(expectedProfile));
                _userRepository.Received(1).GetUserProfileAsync(userId);
            });
        }

        [Test]
        public void GIVEN_ProfileDoesNotExist_WHEN_GettingProfileAsync_THEN_ThrowNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepository.GetUserProfileAsync(userId).Returns((UserProfileDto?)null);

            // Act & Assert
            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await _userService.GetProfileAsync(userId));
            Assert.That(exception.Message, Is.EqualTo(string.Format(ErrorMessages.UserNotFoundTemplate, userId)));
        }

        [Test]
        public void GIVEN_InvalidDto_WHEN_UpdatingProfileAsync_THEN_ThrowValidationExceptionAndDoNotCallRepository()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = CreateUpdateProfileDto();

            _updateProfileValidator.ValidateAsync(Arg.Any<UpdateProfileDto>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult(new[] { new ValidationFailure("Email", "Invalid email") }));

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _userService.UpdateProfileAsync(userId, dto));
            Assert.Multiple(() =>
            {
                _userRepository.DidNotReceive().GetUserProfileAsync(Arg.Any<Guid>());
                _userRepository.DidNotReceive().GetUserDetailsByEmailAsync(Arg.Any<string>());
                _userRepository.DidNotReceive().UpdateProfileAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<UpdateProfileDto>());
            });
        }

        [Test]
        public void GIVEN_UserDoesNotExist_WHEN_UpdatingProfileAsync_THEN_ThrowNotFoundExceptionAndDoNotUpdate()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = CreateUpdateProfileDto();

            _updateProfileValidator.ValidateAsync(Arg.Any<UpdateProfileDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _userRepository.GetUserProfileAsync(userId).Returns((UserProfileDto?)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _userService.UpdateProfileAsync(userId, dto));
            Assert.Multiple(() =>
            {
                _userRepository.DidNotReceive().GetUserDetailsByEmailAsync(Arg.Any<string>());
                _userRepository.DidNotReceive().UpdateProfileAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<UpdateProfileDto>());
            });
        }

        [Test]
        public void GIVEN_EmailBelongsToAnotherUser_WHEN_UpdatingProfileAsync_THEN_ThrowConflictExceptionAndDoNotUpdate()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = CreateUpdateProfileDto();

            _updateProfileValidator.ValidateAsync(Arg.Any<UpdateProfileDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _userRepository.GetUserProfileAsync(userId).Returns(CreateProfileDto());
            _userRepository.GetUserDetailsByEmailAsync(dto.Email).Returns(CreateUser(Guid.NewGuid()));

            // Act & Assert
            var exception = Assert.ThrowsAsync<ConflictException>(async () => await _userService.UpdateProfileAsync(userId, dto));
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo(ErrorMessages.EmailTaken));
                _skinProfileService.DidNotReceive().GetSkinProfileIdAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<bool>());
                _userRepository.DidNotReceive().UpdateProfileAsync(Arg.Any<Guid>(), Arg.Any<Guid>(), Arg.Any<UpdateProfileDto>());
            });
        }

        [Test]
        public async Task GIVEN_EmailBelongsToTheSameUser_WHEN_UpdatingProfileAsync_THEN_UpdateProfile()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = CreateUpdateProfileDto();
            var skinProfileId = Guid.NewGuid();

            _updateProfileValidator.ValidateAsync(Arg.Any<UpdateProfileDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _userRepository.GetUserProfileAsync(userId).Returns(CreateProfileDto());
            _userRepository.GetUserDetailsByEmailAsync(dto.Email).Returns(CreateUser(userId));
            _skinProfileService.GetSkinProfileIdAsync(3, 4, true).Returns(skinProfileId);

            // Act
            await _userService.UpdateProfileAsync(userId, dto);

            // Assert
            await _userRepository.Received(1).UpdateProfileAsync(userId, skinProfileId, dto);
        }

        [Test]
        public async Task GIVEN_EmailIsNotUsedByAnyone_WHEN_UpdatingProfileAsync_THEN_ResolveSkinProfileAndUpdateProfile()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var dto = CreateUpdateProfileDto();
            var skinProfileId = Guid.NewGuid();

            _updateProfileValidator.ValidateAsync(Arg.Any<UpdateProfileDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _userRepository.GetUserProfileAsync(userId).Returns(CreateProfileDto());
            _userRepository.GetUserDetailsByEmailAsync(dto.Email).Returns((User?)null);
            _skinProfileService.GetSkinProfileIdAsync(3, 4, true).Returns(skinProfileId);

            // Act
            await _userService.UpdateProfileAsync(userId, dto);

            // Assert
            Assert.Multiple(() =>
            {
                _skinProfileService.Received(1).GetSkinProfileIdAsync(3, 4, true);
                _userRepository.Received(1).UpdateProfileAsync(userId, skinProfileId, dto);
            });
        }
    }
}