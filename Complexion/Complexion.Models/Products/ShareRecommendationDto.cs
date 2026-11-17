namespace Complexion.Models.Products
{
    public record ShareRecommendationDto
    {
        public string CategoryName { get; set; } = null!;
        public string PriceTierName { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ShadeName { get; set; } = null!;
        public string MatchedShadeName { get; set; } = null!;
        public string MatchedUndertoneName { get; set; } = null!;
        public bool HasTint { get; set; }
        public string? Comment { get; set; }
    }
}