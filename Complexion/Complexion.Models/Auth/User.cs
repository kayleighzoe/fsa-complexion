namespace Complexion.Models.Auth
{
    public record User
    {
        public Guid UserId { get; set; }
        public Guid SkinProfileId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
