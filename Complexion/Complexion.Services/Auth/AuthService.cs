
using Complexion.Exceptions;
using Complexion.Models.Auth;
using Complexion.Models.Skin;
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

            if (await _userRepository.GetByUsernameAsync(registerDto.Username) != null)
            {
                throw new ConflictException("Username is already taken.");
            }

            if (await _userRepository.GetByEmailAsync(registerDto.Email) != null)
            {
                throw new ConflictException("Email is already registered.");
            }

            var skinProfileId = await _skinProfileService.GetOrCreateSkinProfileAsync(
                new CreateSkinProfileDto
                {
                    ShadeId = registerDto.ShadeId,
                    UndertoneId = registerDto.UndertoneId,
                    HasTint = registerDto.HasTint
                });

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

            var user = loginDto.UsernameOrEmail.Contains('@')
                ? await _userRepository.GetByEmailAsync(loginDto.UsernameOrEmail)
                : await _userRepository.GetByUsernameAsync(loginDto.UsernameOrEmail);

            if (user == null || !_passwordHasher.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            return _tokenService.CreateToken(user);
        }

    }
}
