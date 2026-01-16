namespace ShoppingCart.DataModel
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set;}
        public int TotalPrice { get; set; }
        public int TotalDiscount { get; set; }
        public int TotalAmount { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
