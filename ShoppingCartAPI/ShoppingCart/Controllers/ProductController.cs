using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DataModel;
using ShoppingCart.Repositories;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var result = await _repository.AddProductAsync(product);
            if (result > 0)
                return Ok("Product added succesfully.");
            return BadRequest("Failed to add product.");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest("Product ID mismatch");

           
            var existing = await _repository.GetProductByIdAsync(id);
            if (existing == null)
                return NotFound("Product not found");

            await _repository.UpdateProductAsync(product);
            return NoContent(); // 204 No Content
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existing = await _repository.GetProductByIdAsync(id);
            if (existing == null)
                return NotFound("Product not found");

            await _repository.DeleteProductAsync(id);
            return NoContent(); 
        }
    }
}
