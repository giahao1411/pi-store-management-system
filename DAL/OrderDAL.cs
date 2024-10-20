using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class OrderDAL
    {
        private Connection dbConn = new Connection();

        public List<OrderDTO> getOrderList()
        {
            SqlConnection conn = null;
            string query = "SELECT * FROM Orders";
            List<OrderDTO> orderList = new List<OrderDTO>();

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrderDTO orderDTO = new OrderDTO
                    {
                        id = reader["ID"].ToString(),
                        clientID = reader["ClientID"].ToString(),
                        employeeID = reader["EmployeeID"].ToString(),
                        orderDate = (DateTime)reader["OrderDate"],
                        totalPrice = (double)reader["TotalPrice"]
                    };

                    orderList.Add(orderDTO);
                }
                return orderList;
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

        public bool insert(OrderDTO orderDTO)
        {
            string query = "INSERT INTO Orders VALUES (@Id, @ClientId, @EmployeeId, @OrderDate, @TotalPrice)";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", orderDTO.id);
                cmd.Parameters.AddWithValue("@ClientId", orderDTO.clientID);
                cmd.Parameters.AddWithValue("@EmployeeId", orderDTO.employeeID);
                cmd.Parameters.AddWithValue("@OrderDate", orderDTO.orderDate);
                cmd.Parameters.AddWithValue("@TotalPrice", orderDTO.totalPrice);

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
                if(conn != null)
                {
                    conn.Close();
                }
            }
        }

        public bool update(OrderDTO orderDTO)
        {
            string query = "UPDATE Orders SET ClientID = @ClientID, EmployeeID = @EmployeeID, OrderDate = @OrderDate, TotalPrice = @TotalPrice WHERE ID = @Id";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", orderDTO.id);
                cmd.Parameters.AddWithValue("@ClientId", orderDTO.clientID);
                cmd.Parameters.AddWithValue("@EmployeeId", orderDTO.employeeID);
                cmd.Parameters.AddWithValue("@OrderDate", orderDTO.orderDate);
                cmd.Parameters.AddWithValue("@TotalPrice", orderDTO.totalPrice);

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

        public bool delete(OrderDTO orderDTO)
        {
            string query = "DELETE FROM Orders WHERE ID = @Id";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", orderDTO.id);

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

        public bool updateTotalPrice(double totalPrice, string orderID)
        {
            string query = "UPDATE Orders SET TotalPrice = @TotalPrice WHERE ID = @Id";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
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

        public int countOrder()
        {
            string query = "SELECT COUNT(*) FROM Orders";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                return (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
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
