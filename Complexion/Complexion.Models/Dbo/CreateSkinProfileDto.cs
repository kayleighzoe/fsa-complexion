namespace Complexion.Models.Dbo
{
    public class CreateSkinProfileDto
    {
        public int ShadeId { get; set; }
        public int UndertoneId { get; set; }
        public bool HasTint { get; set; }
    }
}