using Complexion.Services.Auth;

namespace Complexion.Tests.Services.Auth
{
    public class PasswordHasherTests
    {
        private PasswordHasher _passwordHasher;

        [SetUp]
        public void Setup()
        {
            _passwordHasher = new PasswordHasher();
        }

        [Test]
        public void GIVEN_Password_WHEN_Hashing_THEN_ReturnHashDifferentFromPassword()
        {
            // Arrange
            var password = "Password123!";

            // Act
            var hash = _passwordHasher.Hash(password);

            // Assert
            Assert.That(hash, Is.Not.EqualTo(password));
        }

        [Test]
        public void GIVEN_HashOfPassword_WHEN_VerifyingTheSamePassword_THEN_ReturnTrue()
        {
            // Arrange
            var password = "Password123!";
            var hash = _passwordHasher.Hash(password);

            // Act
            var result = _passwordHasher.Verify(password, hash);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void GIVEN_HashOfPassword_WHEN_VerifyingADifferentPassword_THEN_ReturnFalse()
        {
            // Arrange
            var hash = _passwordHasher.Hash("Password123!");

            // Act
            var result = _passwordHasher.Verify("WrongPassword1!", hash);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}