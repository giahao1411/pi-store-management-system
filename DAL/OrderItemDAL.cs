using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.XPath;

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

        public bool updateProductQuantity(OrderItemDTO orderItemDTO)
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
    }
}
