namespace Complexion.Models.Auth
{
    public record RegistrationConflictsDto
    {
        public bool UsernameTaken { get; set; }
        public bool EmailTaken { get; set; }
    }
}
