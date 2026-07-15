namespace Complexion.Models.Dbo
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public int CategoryId { get; set; }
        public int PriceTierId { get; set; }
        public string Name { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string ShadeName { get; set; } = null!;
    }
}