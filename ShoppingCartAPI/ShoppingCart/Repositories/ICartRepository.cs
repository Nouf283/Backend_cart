using ShoppingCart.DataModel;

namespace ShoppingCart.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddCartAsync(Cart cart);
    }
}
