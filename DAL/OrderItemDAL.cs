using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class OrderItemDAL
    {
        private Connection dbConn = new Connection();

        public List<OrderItemDTO> getOrderItemList(string orderID)
        {
            SqlConnection conn = null;
            string query = "SELECT * FROM OrderItem WHERE OrderID = @Id";
            List<OrderItemDTO> orderItemList = new List<OrderItemDTO>();

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", orderID);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrderItemDTO orderItemDTO = new OrderItemDTO
                    {
                       id = reader["ID"].ToString(),
                       orderID = reader["OrderID"].ToString(),
                       productID = reader["ProductID"].ToString(),
                       quantity = (int)reader["Quantity"]
                    };

                    orderItemList.Add(orderItemDTO);
                }
                return orderItemList;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
        }

        public bool insert(OrderItemDTO orderItemDTO)
        {
            string query = "INSERT INTO OrderItem VALUES (@Id, @OrderID, @ProductID, @Quantity)";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", orderItemDTO.id);
                cmd.Parameters.AddWithValue("@OrderID", orderItemDTO.orderID);
                cmd.Parameters.AddWithValue("@ProductID", orderItemDTO.productID);
                cmd.Parameters.AddWithValue("@Quantity", orderItemDTO.quantity);

                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;   
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public bool update(OrderItemDTO orderItemDTO)
        {
            string query = "UPDATE OrderItem SET ProductID = @ProductID, Quantity = @Quantity WHERE ID = @Id";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", orderItemDTO.id);
                cmd.Parameters.AddWithValue("@ProductID", orderItemDTO.productID);
                cmd.Parameters.AddWithValue("@Quantity", orderItemDTO.quantity);

                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        
        public bool delete(OrderItemDTO orderItemDTO)
        {
            string query = "DELETE FROM OrderItem WHERE ID = @Id";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", orderItemDTO.id);

                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        
        public bool updateProductQuantityAdd(OrderItemDTO orderItemDTO)
        {
            string query = "UPDATE Product SET Quantity = Quantity - @Quantity WHERE ID = @ProductID";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Quantity", orderItemDTO.quantity);
                cmd.Parameters.AddWithValue("@ProductID", orderItemDTO.productID);

                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public bool updateProductQuantityUpdate(OrderItemDTO newOrderItemDTO, int oldQuantity)
        {
            int quantityDifference = oldQuantity - newOrderItemDTO.quantity;
            string query = "UPDATE Product SET Quantity = Quantity + @quantityDifference WHERE ID = @ProductID";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@quantityDifference", quantityDifference);
                cmd.Parameters.AddWithValue("@ProductID", newOrderItemDTO.productID);

                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public bool updateProductQuantityDelete(OrderItemDTO orderItemDTO)
        {
            string query = "UPDATE Product SET Quantity = Quantity + @Quantity WHERE ID = @ProductID";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Quantity", orderItemDTO.quantity);
                cmd.Parameters.AddWithValue("@ProductID", orderItemDTO.productID);

                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public ProductDTO getProductById(string productID)
        {
            string query = "SELECT * FROM Product WHERE ID = @ProductID";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ProductID", productID);

                SqlDataReader reader = cmd.ExecuteReader();

                ProductDTO product = new ProductDTO();

                while (reader.Read())
                {
                    product.id = reader["ID"].ToString();
                    product.name = reader["Name"].ToString();
                    product.description = reader["Description"].ToString();
                    product.price = (double)reader["Price"];
                    product.quantity = (int)reader["Quantity"];
                };
                return product;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public bool deleteOrderItemByOrderId(string orderID)
        {
            string query = "DELETE FROM OrderItem WHERE OrderID = @Id";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", orderID);

                int result = cmd.ExecuteNonQuery();

                return result > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public string getLastestOrderItemID()
        {
            string query = "SELECT TOP 1 ID FROM OrderItem ORDER BY CAST(SUBSTRING(ID, 3, LEN(ID) - 2) AS INT) DESC";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                string lastestID = "";

                while (reader.Read())
                {
                    lastestID = reader["ID"].ToString();
                };
                return lastestID;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
