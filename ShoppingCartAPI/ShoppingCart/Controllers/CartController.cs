using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DataModel;
using ShoppingCart.Repositories;

namespace ShoppingCart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController:ControllerBase
    {
        private readonly ICartRepository _repository;
        public CartController(ICartRepository repository)
        {
            _repository = repository;
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(Cart cart)
        {
            var result = await _repository.AddCartAsync(cart);
            if (result > 0)
                return Ok("Cart items added succesfully.");
            return BadRequest("Failed to add product.");
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    var existing = await _repository.GetProductByIdAsync(id);
        //    if (existing == null)
        //        return NotFound("Product not found");

        //    await _repository.DeleteProductAsync(id);
        //    return NoContent(); 
        //}
    }
}
