namespace Complexion.Models.Products
{
    public record RecommendationCommentDto
    {
        public required string Username { get; set; }
        public required string Text { get; set; }
    }
}
