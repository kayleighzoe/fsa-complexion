using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Complexion.Models.Auth;
using Complexion.Services.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Complexion.Tests.Services.Auth
{
    public class TokenServiceTests
    {
        private JwtSettings _settings;
        private TokenService _tokenService;

        [SetUp]
        public void Setup()
        {
            _settings = CreateSettings();
            _tokenService = new TokenService(Options.Create(_settings));
        }

        private static JwtSettings CreateSettings()
        {
            return new JwtSettings
            {
                Key = "this-is-a-test-signing-key-with-enough-length-123456",
                Issuer = "test-issuer",
                Audience = "test-audience",
                ExpirationInMinutes = 60
            };
        }

        private static User CreateUser()
        {
            return new User
            {
                UserId = Guid.NewGuid(),
                Username = "Test123"
            };
        }

        private static JwtSecurityToken ReadToken(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token);
        }

        [Test]
        public void GIVEN_User_WHEN_CreatingToken_THEN_TokenContainsUserIdAndUsernameClaims()
        {
            // Arrange
            var user = CreateUser();

            // Act
            var token = ReadToken(_tokenService.CreateToken(user));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(token.Subject, Is.EqualTo(user.UserId.ToString()));
                Assert.That(token.Claims.Single(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value, Is.EqualTo(user.Username));
            });
        }

        [Test]
        public void GIVEN_JwtSettings_WHEN_CreatingToken_THEN_TokenHasConfiguredIssuerAndAudience()
        {
            // Arrange
            var user = CreateUser();

            // Act
            var token = ReadToken(_tokenService.CreateToken(user));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(token.Issuer, Is.EqualTo(_settings.Issuer));
                Assert.That(token.Audiences, Does.Contain(_settings.Audience));
            });
        }

        [Test]
        public void GIVEN_ExpirationInMinutes_WHEN_CreatingToken_THEN_TokenExpiresAfterThatManyMinutes()
        {
            // Arrange
            var user = CreateUser();
            var expectedExpiry = DateTime.UtcNow.AddMinutes(_settings.ExpirationInMinutes);

            // Act
            var token = ReadToken(_tokenService.CreateToken(user));

            // Assert
            Assert.That(token.ValidTo, Is.EqualTo(expectedExpiry).Within(TimeSpan.FromMinutes(1)));
        }

        [Test]
        public void GIVEN_User_WHEN_CreatingToken_THEN_TokenIsSignedWithConfiguredKey()
        {
            // Arrange
            var user = CreateUser();
            var token = _tokenService.CreateToken(user);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)),
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience
            };

            // Act & Assert
            Assert.DoesNotThrow(() => new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _));
        }

        [Test]
        public void GIVEN_User_WHEN_ValidatingTokenWithADifferentKey_THEN_ValidationFails()
        {
            // Arrange
            var user = CreateUser();
            var token = _tokenService.CreateToken(user);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a-completely-different-signing-key-1234567890")),
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience
            };

            // Act & Assert
            Assert.Catch<SecurityTokenException>(() => new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _));
        }

        [Test]
        public void GIVEN_SameUser_WHEN_CreatingTwoTokens_THEN_EachTokenHasADifferentId()
        {
            // Arrange
            var user = CreateUser();

            // Act
            var firstToken = ReadToken(_tokenService.CreateToken(user));
            var secondToken = ReadToken(_tokenService.CreateToken(user));

            // Assert
            Assert.That(firstToken.Id, Is.Not.EqualTo(secondToken.Id));
        }
    }
}