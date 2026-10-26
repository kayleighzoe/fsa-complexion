namespace Complexion.Models.Auth
{
    public record MyRecommendationDto
    {
        public Guid RecommendationId { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string ShadeName { get; set; } = string.Empty;
        public string PriceTierName { get; set; } = string.Empty;
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
