using Microsoft.Data.SqlClient;
using ShoppingCart.DataModel;
using ShoppingCart.Repositories;

namespace ShoppingCart.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection _connection;
        public ProductRepository(SqlConnection connection)
        {
            _connection = connection; 
        }

        public async Task<int> AddProductAsync(Product product)
        {
            string sql = @"INSERT INTO Products (Name, Description, Price, PictureUrl, Type, Brand, QuantityInStock)
                           VALUES (@Name, @Description, @Price, @PictureUrl, @Type, @Brand, @QuantityInStock);
                           SELECT CAST(SCOPE_IDENTITY() as int);";

            await _connection.OpenAsync();
            using SqlCommand cmd = new SqlCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@PictureUrl", product.PictureUrl);
            cmd.Parameters.AddWithValue("@Type", product.Type);
            cmd.Parameters.AddWithValue("@Brand", product.Brand);
            cmd.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);

            var newId = (int)await cmd.ExecuteScalarAsync();
            await _connection.CloseAsync();
            return newId;
        }

      

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = null;
            string sql = "SELECT * FROM Products WHERE Id = @Id";

            await _connection.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    product = new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Price = reader.GetDecimal(3),
                        PictureUrl = reader.GetString(4),
                        Type = reader.GetInt32(5),
                        Brand = reader.GetInt32(6),
                        QuantityInStock = reader.GetInt32(7)
                    };
                }
            }
            await _connection.CloseAsync();
            return product;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = new List<Product>();
            string sql = "SELECT * FROM Products";
            await _connection.OpenAsync();
            using (SqlCommand cmd = new SqlCommand(sql, _connection))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //products.Add(new Product
                    //{
                    //    Id = reader.GetInt32(0),
                    //    Name = reader.GetString(1),
                    //    Description = reader.GetString(2),
                    //    Price = reader.GetDecimal(3),
                    //    PictureUrl = reader.GetString(4),
                    //    Type = reader.GetInt32(5),
                    //    Brand = reader.GetInt32(6),
                    //    QuantityInStock = reader.GetInt32(7)
                    //});
                    var product = new Product();

                    product.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    product.Name = reader.GetString(reader.GetOrdinal("Name"));
                    product.Description = reader.GetString(reader.GetOrdinal("Description"));
                    product.Price = reader.GetDecimal(reader.GetOrdinal("Price"));
                    product.PictureUrl = reader.GetString(reader.GetOrdinal("PictureUrl"));
                    product.Type = reader.GetInt32(reader.GetOrdinal("Type"));
                    product.Brand = reader.GetInt32(reader.GetOrdinal("Brand"));
                    product.QuantityInStock = reader.GetInt32(reader.GetOrdinal("QuantityInStock"));
                    products.Add(product);
                }
            }
            await _connection.CloseAsync();
            return products;

        }

        public async Task UpdateProductAsync(Product product)
        {
            string sql = @"UPDATE Products 
                           SET Name=@Name, Description=@Description, Price=@Price, PictureUrl=@PictureUrl, 
                               Type=@Type, Brand=@Brand, QuantityInStock=@QuantityInStock
                           WHERE Id=@Id";

            await _connection.OpenAsync();
            using SqlCommand cmd = new SqlCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@Id", product.Id);
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@Price", product.Price);
            cmd.Parameters.AddWithValue("@PictureUrl", product.PictureUrl);
            cmd.Parameters.AddWithValue("@Type", product.Type);
            cmd.Parameters.AddWithValue("@Brand", product.Brand);
            cmd.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);

            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            string sql = "DELETE FROM Products WHERE Id=@Id";

            await _connection.OpenAsync();
            using SqlCommand cmd = new SqlCommand(sql, _connection);
            cmd.Parameters.AddWithValue("@Id", id);
            await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }
    }
}
