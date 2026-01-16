namespace ShoppingCart.DataModel
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int Type { get; set; }
        public int Brand { get; set; }
        public int QuantityInStock { get; set; }
    }
}
