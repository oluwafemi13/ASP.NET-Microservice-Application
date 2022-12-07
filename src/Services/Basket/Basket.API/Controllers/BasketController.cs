using Basket.API.DiscountService;
using Basket.API.Models;
using Basket.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class BasketController:ControllerBase
    {

        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository repository,
                                DiscountGrpcService discountGrpcService)
        {
            _repository = repository;
            _discountGrpcService = discountGrpcService;
        }


        [HttpGet("{UserName}", Name ="GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
                var basket = await _repository.GetBasket(userName);
            return Ok(basket);
        }

        [HttpPost]
        [EndpointName("UpdateBasket")]
        [ProducesResponseType(typeof (ShoppingCart), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart cart)
        {
            if (cart == null)
                return NotFound();
            //consume the discount gRPC Service
            foreach (var item in cart.items)
            {
                //get the discount for each product
                var coupon = await _discountGrpcService.getDiscount(item.ProductName);
                //update the price of each product by deducting
                item.Price -= coupon.Amount;

            }
            
            var update = await _repository.UpdateBasket(cart);
            return Ok(update);
        }

        [HttpDelete]
        [EndpointName("DeleteBasket")]
        public async Task<ActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }
    }
}
