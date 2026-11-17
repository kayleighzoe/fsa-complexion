namespace Complexion.Services.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public bool Verify(string password, string passwordHasher)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHasher);
        }
    }
}
