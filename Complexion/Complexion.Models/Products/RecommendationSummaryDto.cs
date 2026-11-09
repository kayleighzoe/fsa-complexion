using Complexion.Models.Products;

namespace Complexion.Models.Products
{
    public record RecommendationSummaryDto
    {
        public required string Brand { get; set; }
        public required string ProductName { get; set; }
        public required string ProductShadeName { get; set; }
        public required string CategoryName { get; set; }
        public required string PriceTierName { get; set; }
        public required string ShadeName { get; set; }
        public required string UndertoneName { get; set; }
        public bool HasTint { get; set; }
        public int RecommendedByCount { get; set; }
        public required List<RecommendationCommentDto> Comments { get; set; }
    }
}
