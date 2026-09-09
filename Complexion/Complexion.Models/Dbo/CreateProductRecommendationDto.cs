namespace Complexion.Models.Dbo
{
    public class CreateProductRecommendationDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid SkinProfileId { get; set; }
        public string? Comment { get; set; }
    }
}