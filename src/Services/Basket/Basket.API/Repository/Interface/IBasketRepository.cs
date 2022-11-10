using Basket.API.Models;

namespace Basket.API.Repository.Interface
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string UserName);

        Task<ShoppingCart> UpdateBasket(ShoppingCart Basket);

        Task DeleteBasket(string UserName);
    }
}
