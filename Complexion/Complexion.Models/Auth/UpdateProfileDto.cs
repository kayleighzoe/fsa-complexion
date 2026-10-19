namespace Complexion.Models.Auth
{
    public record UpdateProfileDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int ShadeId { get; set; }
        public int UndertoneId { get; set; }
        public bool HasTint { get; set; }
    }
}
