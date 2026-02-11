using System;
using System.Data;
using System.Data.SqlClient;

namespace ACOTIN_POS_APPLICATION
{
    internal class ProductDatabase
    {
        private string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Products;Integrated Security=True";
        private SqlConnection connection;

        // Constructor
        public ProductDatabase()
        {
            connection = new SqlConnection(connectionString);
        }

        // Open connection
        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // Close connection
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        // INSERT - Add new product
        public void InsertProduct(string productName, string productId, int quantity, decimal price,
                                  string unit, string description, string productPicPath, string barcodePicPath)
        {
            try
            {
                OpenConnection();
                string query = "INSERT INTO productTbl (product_name, productid, quantity, price, unit, description, product_pic_path, barcode_pic_path) " +
                               "VALUES (@productName, @productId, @quantity, @price, @unit, @description, @productPicPath, @barcodePicPath)";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@productName", productName);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@unit", unit);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@productPicPath", productPicPath);
                cmd.Parameters.AddWithValue("@barcodePicPath", barcodePicPath);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting product: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // UPDATE - Update product
        public void UpdateProduct(string productName, string productId, int quantity, decimal price,
                                  string unit, string description, string productPicPath, string barcodePicPath)
        {
            try
            {
                OpenConnection();
                string query = "UPDATE productTbl SET product_name = @productName, quantity = @quantity, price = @price, " +
                               "unit = @unit, description = @description, product_pic_path = @productPicPath, " +
                               "barcode_pic_path = @barcodePicPath WHERE productid = @productId";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@productName", productName);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@unit", unit);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@productPicPath", productPicPath);
                cmd.Parameters.AddWithValue("@barcodePicPath", barcodePicPath);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating product: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // DELETE - Delete product by barcode
        public void DeleteProduct(string productId)
        {
            try
            {
                OpenConnection();
                string query = "DELETE FROM productTbl WHERE productid = @productId";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@productId", productId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting product: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // SELECT - Get all products
        public DataTable GetAllProducts()
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                string query = "SELECT * FROM productTbl";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting products: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }

        // SELECT - Get product by barcode
        public DataTable GetProductByBarcode(string productId)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                string query = "SELECT * FROM productTbl WHERE productid = @productId";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@productId", productId);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting product: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }

        // SELECT - Search product by name
        public DataTable SearchProductByName(string productName)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                string query = "SELECT * FROM productTbl WHERE product_name LIKE @productName";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@productName", "%" + productName + "%");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error searching products: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }

        // UPDATE - Update only quantity (for inventory management)
        public void UpdateQuantity(string productId, int newQuantity)
        {
            try
            {
                OpenConnection();
                string query = "UPDATE productTbl SET quantity = @quantity WHERE productid = @productId";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@quantity", newQuantity);
                cmd.Parameters.AddWithValue("@productId", productId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating quantity: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // UPDATE - Update only price
        public void UpdatePrice(string productId, decimal newPrice)
        {
            try
            {
                OpenConnection();
                string query = "UPDATE productTbl SET price = @price WHERE productid = @productId";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@price", newPrice);
                cmd.Parameters.AddWithValue("@productId", productId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating price: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}