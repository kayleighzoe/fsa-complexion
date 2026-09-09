namespace Complexion.Models.Dbo
{
    public class CreateProductDto
    {
        public int CategoryId { get; set; }
        public int PriceTierId { get; set; }
        public string Name { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string ShadeName { get; set; } = null!;
    }
}