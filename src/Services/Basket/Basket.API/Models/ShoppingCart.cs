namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; }

        public List<ShoppingCartItems> items { get; set; } = new List<ShoppingCartItems>();

        public ShoppingCart()
        {

        }

        public ShoppingCart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {

            get
            {
                decimal totalPrice = 0;
                foreach (var item in items)
                {
                    totalPrice = item.Price * item.Quantity;
                    
                }
                return totalPrice;
            }

        }

    }
}
