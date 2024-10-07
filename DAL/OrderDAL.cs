using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        orderDate = reader["OrderDate"].ToString(),
                        totalPrice = (double)reader["TotalPrice"]
                    };

                    orderList.Add(orderDTO);
                }
                return orderList;
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
