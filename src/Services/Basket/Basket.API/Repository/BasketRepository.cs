using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repository.Interface
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task DeleteBasket(string UserName)
        {
            await _redisCache.RemoveAsync(UserName);

        }

        public async Task<ShoppingCart> GetBasket(string UserName)
        {
            var cart = await _redisCache.GetStringAsync(UserName);
            if (cart is null)
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(cart);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart Basket)
        {
            if(Basket is null)
                return null;
             await _redisCache.SetStringAsync(Basket.UserName, JsonConvert.SerializeObject(Basket));
            
            return await GetBasket(Basket.UserName);

        }
    }
}
