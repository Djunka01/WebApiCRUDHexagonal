using Microsoft.AspNetCore.Mvc;
using WepApiCRUDHexagonal.Application.Interfaces;
using WepApiCRUDHexagonal.Domain.Entities;

namespace WepApiCRUDHexagonal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAllProducts()
        { 
            var product =  await _productService.GetAllProducts();
            return Ok(product);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByID(int id)
        {
            var product = await _productService.GetProductByID(id);
            if (product == null)
            {
                return NotFound(new { message = $"No se encontraron registros con el ID: {id} "});
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        { 
           await _productService.CreateProduct(product);
           return CreatedAtAction(nameof(GetProductByID), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var existingProduct = await _productService.GetProductByID(id);
            if  (existingProduct == null)
            {
                return NotFound(new { message = $"No se encontraron registros con el ID: {id} " });
            }

            await _productService.UpdateProduct(id, updatedProduct);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByID(id);
            if (product == null) 
            {
                return NotFound(new { message = $"No se encontraron registros con el ID: {id} " });
            }
            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
