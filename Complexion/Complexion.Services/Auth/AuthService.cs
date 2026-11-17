
using Complexion.Exceptions;
using Complexion.Exceptions.Constants;
using Complexion.Models.Auth;
using Complexion.Repository.Auth;
using Complexion.Services.Skin;
using FluentValidation;

namespace Complexion.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISkinProfileService _skinProfileService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LoginDto> _loginValidator;

        public AuthService(
            IUserRepository userRepository,
            ISkinProfileService skinProfileService,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IValidator<RegisterDto> registerValidator,
            IValidator<LoginDto> loginValidator)
        {
            _userRepository = userRepository;
            _skinProfileService = skinProfileService;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var validationResult = await _registerValidator.ValidateAsync(registerDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var conflicts = await _userRepository.GetRegistrationConflictsAsync(registerDto.Username, registerDto.Email);

            if (conflicts.UsernameTaken)
            {
                throw new ConflictException(ErrorMessages.UsernameTaken);
            }

            if (conflicts.EmailTaken)
            {
                throw new ConflictException(ErrorMessages.EmailTaken);
            }

            var skinProfileId = await _skinProfileService.GetSkinProfileIdAsync(registerDto.ShadeId, registerDto.UndertoneId, registerDto.HasTint);

            var user = new User
            {
                SkinProfileId = skinProfileId,
                FirstName = registerDto.FirstName,
                Surname = registerDto.Surname,
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = _passwordHasher.Hash(registerDto.Password)
            };

            user.UserId = await _userRepository.CreateUserAsync(user);

            return _tokenService.CreateToken(user);
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var usernameOrEmail = loginDto.UsernameOrEmail;
            var isEmail = usernameOrEmail.Contains('@');

            var user = isEmail
                ? await _userRepository.GetUserDetailsByEmailAsync(usernameOrEmail)
                : await _userRepository.GetUserDetailsByUsernameAsync(usernameOrEmail);

            if (user is null)
            {
                throw new UnauthorizedAccessException(ErrorMessages.InvalidCredentials);
            }

            var isPasswordValid = _passwordHasher.Verify(loginDto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException(ErrorMessages.InvalidCredentials);
            }

            return _tokenService.CreateToken(user);
        }
    }
}
