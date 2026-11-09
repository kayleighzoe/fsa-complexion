namespace Complexion.Models.Products
{
    public record RecommendationRowDto
    {
        public Guid RecommendationId { get; set; }
        public Guid UserId { get; set; }
        public required string Username { get; set; }
        public Guid ProductId { get; set; }
        public required string Brand { get; set; }
        public required string ProductName { get; set; }
        public required string ProductShadeName { get; set; }
        public required string CategoryName { get; set; }
        public required string PriceTierName { get; set; }
        public required string ProfileShadeName { get; set; }
        public required string ProfileUndertoneName { get; set; }
        public bool HasTint { get; set; }
        public string? Comment { get; set; }
    }
}
