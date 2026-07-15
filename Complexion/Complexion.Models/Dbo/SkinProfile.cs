namespace Complexion.Models.Dbo
{
    public class SkinProfile
    {
        public Guid SkinProfileId { get; set; }
        public int ShadeId { get; set; }
        public int UndertoneId { get; set; }
        public bool HasTint { get; set; }
    }
}