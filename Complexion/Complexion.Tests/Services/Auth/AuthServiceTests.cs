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
    public class AuthServiceTests
    {
        private IUserRepository _userRepository;
        private ISkinProfileService _skinProfileService;
        private IPasswordHasher _passwordHasher;
        private ITokenService _tokenService;
        private IValidator<RegisterDto> _registerValidator;
        private IValidator<LoginDto> _loginValidator;
        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _skinProfileService = Substitute.For<ISkinProfileService>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _tokenService = Substitute.For<ITokenService>();
            _registerValidator = Substitute.For<IValidator<RegisterDto>>();
            _loginValidator = Substitute.For<IValidator<LoginDto>>();

            _authService = new AuthService(
                _userRepository,
                _skinProfileService,
                _passwordHasher,
                _tokenService,
                _registerValidator,
                _loginValidator);
        }

        private static RegisterDto CreateValidRegisterDto()
        {
            return new RegisterDto
            {
                FirstName = "Test",
                Surname = "User",
                Email = "test@example.com",
                Username = "Test123",
                Password = "Password123!",
                ShadeId = 3,
                UndertoneId = 4,
                HasTint = true
            };
        }

        private static RegistrationConflictsDto CreateConflicts(bool usernameTaken = false, bool emailTaken = false)
        {
            return new RegistrationConflictsDto
            {
                UsernameTaken = usernameTaken,
                EmailTaken = emailTaken
            };
        }

        private static User CreateStoredUser()
        {
            return new User
            {
                UserId = Guid.NewGuid(),
                SkinProfileId = Guid.NewGuid(),
                FirstName = "Test",
                Surname = "User",
                Username = "Test123",
                Email = "test@example.com",
                PasswordHash = "stored-hash"
            };
        }

        // Register

        [Test]
        public void GIVEN_InvalidDto_WHEN_RegisteringAsync_THEN_ThrowValidationExceptionAndDoNotCallRepository()
        {
            // Arrange
            var dto = CreateValidRegisterDto();

            _registerValidator.ValidateAsync(Arg.Any<RegisterDto>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult(new[] { new ValidationFailure("Username", "Invalid username") }));

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _authService.RegisterAsync(dto));
            Assert.Multiple(() =>
            {
                _userRepository.DidNotReceive().GetRegistrationConflictsAsync(Arg.Any<string>(), Arg.Any<string>());
                _userRepository.DidNotReceive().CreateUserAsync(Arg.Any<User>());
            });
        }

        [Test]
        public void GIVEN_UsernameAlreadyTaken_WHEN_RegisteringAsync_THEN_ThrowConflictExceptionAndDoNotCreateUser()
        {
            // Arrange
            var dto = CreateValidRegisterDto();

            _registerValidator.ValidateAsync(Arg.Any<RegisterDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _userRepository.GetRegistrationConflictsAsync(dto.Username, dto.Email).Returns(CreateConflicts(usernameTaken: true));

            // Act & Assert
            var exception = Assert.ThrowsAsync<ConflictException>(async () => await _authService.RegisterAsync(dto));
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo(ErrorMessages.UsernameTaken));
                _userRepository.DidNotReceive().CreateUserAsync(Arg.Any<User>());
            });
        }

        [Test]
        public void GIVEN_EmailAlreadyRegistered_WHEN_RegisteringAsync_THEN_ThrowConflictExceptionAndDoNotCreateUser()
        {
            // Arrange
            var dto = CreateValidRegisterDto();

            _registerValidator.ValidateAsync(Arg.Any<RegisterDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _userRepository.GetRegistrationConflictsAsync(dto.Username, dto.Email).Returns(CreateConflicts(emailTaken: true));

            // Act & Assert
            var exception = Assert.ThrowsAsync<ConflictException>(async () => await _authService.RegisterAsync(dto));
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo(ErrorMessages.EmailTaken));
                _userRepository.DidNotReceive().CreateUserAsync(Arg.Any<User>());
            });
        }

        [Test]
        public async Task GIVEN_ValidDtoAndNoConflicts_WHEN_RegisteringAsync_THEN_CreateUserWithHashedPasswordAndReturnToken()
        {
            // Arrange
            var dto = CreateValidRegisterDto();
            var skinProfileId = Guid.NewGuid();
            var newUserId = Guid.NewGuid();

            _registerValidator.ValidateAsync(Arg.Any<RegisterDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _userRepository.GetRegistrationConflictsAsync(dto.Username, dto.Email).Returns(CreateConflicts());
            _skinProfileService.GetSkinProfileIdAsync(3, 4, true).Returns(skinProfileId);
            _passwordHasher.Hash(dto.Password).Returns("hashed-password");
            _userRepository.CreateUserAsync(Arg.Any<User>()).Returns(newUserId);
            _tokenService.CreateToken(Arg.Any<User>()).Returns("jwt-token");

            // Act
            var result = await _authService.RegisterAsync(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo("jwt-token"));
                _userRepository.Received(1).CreateUserAsync(Arg.Is<User>(u =>
                    u.SkinProfileId == skinProfileId &&
                    u.FirstName == dto.FirstName &&
                    u.Surname == dto.Surname &&
                    u.Username == dto.Username &&
                    u.Email == dto.Email &&
                    u.PasswordHash == "hashed-password"));
                _tokenService.Received(1).CreateToken(Arg.Is<User>(u => u.UserId == newUserId));
            });
        }

        // Login

        [Test]
        public void GIVEN_InvalidDto_WHEN_LoggingInAsync_THEN_ThrowValidationExceptionAndDoNotLookUpUser()
        {
            // Arrange
            var dto = new LoginDto { UsernameOrEmail = "", Password = "Password123" };

            _loginValidator.ValidateAsync(Arg.Any<LoginDto>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult(new[] { new ValidationFailure("UsernameOrEmail", "Required") }));

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _authService.LoginAsync(dto));
            Assert.Multiple(() =>
            {
                _userRepository.DidNotReceive().GetUserDetailsByEmailAsync(Arg.Any<string>());
                _userRepository.DidNotReceive().GetUserDetailsByUsernameAsync(Arg.Any<string>());
            });
        }

        [Test]
        public async Task GIVEN_EmailAndCorrectPassword_WHEN_LoggingInAsync_THEN_LookUpByEmailAndReturnToken()
        {
            // Arrange
            var dto = new LoginDto { UsernameOrEmail = "test@example.com", Password = "Password123" };
            var storedUser = CreateStoredUser();

            _loginValidator.ValidateAsync(Arg.Any<LoginDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _userRepository.GetUserDetailsByEmailAsync("test@example.com").Returns(storedUser);
            _passwordHasher.Verify("Password123", storedUser.PasswordHash).Returns(true);
            _tokenService.CreateToken(storedUser).Returns("jwt-token");

            // Act
            var result = await _authService.LoginAsync(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo("jwt-token"));
                _userRepository.Received(1).GetUserDetailsByEmailAsync("test@example.com");
                _userRepository.DidNotReceive().GetUserDetailsByUsernameAsync(Arg.Any<string>());
            });
        }

        [Test]
        public async Task GIVEN_UsernameAndCorrectPassword_WHEN_LoggingInAsync_THEN_LookUpByUsernameAndReturnToken()
        {
            // Arrange
            var dto = new LoginDto { UsernameOrEmail = "Test123", Password = "Password123" };
            var storedUser = CreateStoredUser();

            _loginValidator.ValidateAsync(Arg.Any<LoginDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _userRepository.GetUserDetailsByUsernameAsync("Test123").Returns(storedUser);
            _passwordHasher.Verify("Password123", storedUser.PasswordHash).Returns(true);
            _tokenService.CreateToken(storedUser).Returns("jwt-token");

            // Act
            var result = await _authService.LoginAsync(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo("jwt-token"));
                _userRepository.Received(1).GetUserDetailsByUsernameAsync("Test123");
                _userRepository.DidNotReceive().GetUserDetailsByEmailAsync(Arg.Any<string>());
            });
        }

        [Test]
        public void GIVEN_UserDoesNotExist_WHEN_LoggingInAsync_THEN_ThrowUnauthorizedAccessException()
        {
            // Arrange
            var dto = new LoginDto { UsernameOrEmail = "Test123", Password = "Password123" };

            _loginValidator.ValidateAsync(Arg.Any<LoginDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _userRepository.GetUserDetailsByUsernameAsync("Test123").Returns((User?)null);

            // Act & Assert
            var exception = Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _authService.LoginAsync(dto));
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo(ErrorMessages.InvalidCredentials));
                _passwordHasher.DidNotReceive().Verify(Arg.Any<string>(), Arg.Any<string>());
                _tokenService.DidNotReceive().CreateToken(Arg.Any<User>());
            });
        }

        [Test]
        public void GIVEN_WrongPassword_WHEN_LoggingInAsync_THEN_ThrowUnauthorizedAccessExceptionAndDoNotCreateToken()
        {
            // Arrange
            var dto = new LoginDto { UsernameOrEmail = "Test123", Password = "WrongPassword1!" };
            var storedUser = CreateStoredUser();

            _loginValidator.ValidateAsync(Arg.Any<LoginDto>(), Arg.Any<CancellationToken>()).Returns(new ValidationResult());
            _userRepository.GetUserDetailsByUsernameAsync("Test123").Returns(storedUser);
            _passwordHasher.Verify("WrongPassword1!", storedUser.PasswordHash).Returns(false);

            // Act & Assert
            var exception = Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _authService.LoginAsync(dto));
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo(ErrorMessages.InvalidCredentials));
                _tokenService.DidNotReceive().CreateToken(Arg.Any<User>());
            });
        }
    }
}