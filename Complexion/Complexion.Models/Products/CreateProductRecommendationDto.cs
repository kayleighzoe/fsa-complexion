namespace Complexion.Models.Products
{
    public record CreateProductRecommendationDto
    {
        public Guid ProductId { get; set; }
        public Guid SkinProfileId { get; set; }
        public string? Comment { get; set; }
    }
}