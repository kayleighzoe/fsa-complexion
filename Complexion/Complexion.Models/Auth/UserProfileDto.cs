namespace Complexion.Models.Auth
{
    public record UserProfileDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ShadeName { get; set; } = string.Empty;
        public string UndertoneName { get; set; } = string.Empty;
        public bool HasTint { get; set; }
    }
}
