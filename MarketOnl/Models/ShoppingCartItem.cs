namespace MarketOnl.Models
{
    public class ShoppingCartItem
    {
        public int productId { get; set; }
        public string ProductName { get; set; }
        
        public string ProductImg { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
