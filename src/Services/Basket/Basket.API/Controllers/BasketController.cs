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

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
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
            if(cart == null)
                return NotFound();
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
