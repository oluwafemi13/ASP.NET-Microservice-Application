namespace Basket.API.Models
{
    public class ShoppingCartItems
    {
        public string ProductName { get; set; }

        public string ProductId { get; set; }

        public decimal Price { get; set; }
        //public string color { get; set; }
        public int Quantity { get; set; }

    }
}
