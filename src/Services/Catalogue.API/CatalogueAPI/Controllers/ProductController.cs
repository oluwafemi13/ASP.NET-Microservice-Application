using CatalogueAPI.Models;
using CatalogueAPI.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CatalogueAPI.Controllers
{
    [ApiController]
    [Route("Api/v1/controller")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _ProductRepository;

        public ProductController(ILogger<ProductController> logger,
                                  IProductRepository productRepository)
        {
            _logger = logger;
            _ProductRepository = productRepository;
        }


        [HttpGet("GetAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _ProductRepository.GetProduct();
            return Ok(products);
        }

        //[HttpGet("getProductById/{id}")]
         [HttpGet("[action]/{id}")]
        //[Route("[action]/{id:length(24)}"]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> getProductById(string id)
        {
            var products = await _ProductRepository.GetProductById(id);
            
            if(products == null)
            {
                _logger.LogError($"Product with id = {id} not  found");
                return NotFound();
            }
            return Ok(products);
            
        }

        [HttpGet]
        [Route("[action]/{Category}", Name = "GetProductByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var product = await _ProductRepository.GetProductByCategory(category);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /*[HttpPost]
        [Route("[action]", Name = "CreateProduct")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> createProduct([FromBody]Product product)
        {
            var creatProduct = _ProductRepository.CreateProduct(product);
            if (creatProduct == null)
            {
                _logger.LogError("Content is Empty");
                return NoContent();
            }
            return Created("CreateProduct", product);

        }*/

        [HttpPost]
        [Route("[action]", Name = "CreateProduct")]
        //[ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> createProduct([FromBody] Product product)
        {
            await _ProductRepository.CreateProduct(product);
            
            return CreatedAtRoute("GetProduct", new {id = product.ProductId }, product);

        }

        [HttpPut]
        [Route("[action]", Name = "UpdateProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> updateProduct([FromBody]Product product)
        {
            
            return Ok(await _ProductRepository.UpdateProduct(product));

        }

        [HttpDelete("{id:length(24)}", Name ="DeleteProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            
            return Ok(await _ProductRepository.DeleteProduct(id));
        }


    }
}
