namespace Complexion.Models.Products
{
    public class ProductRecommendation
    {
        public Guid RecommendationId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid SkinProfileId { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}