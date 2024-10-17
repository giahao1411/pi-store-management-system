using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BillDAL
    {
        private Connection dbConn = new Connection();

        public List<BillDTO> getBillList()
        {
            SqlConnection conn = null;
            string query = "SELECT * FROM Bill";
            List<BillDTO> billList = new List<BillDTO>();

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    BillDTO billDTO = new BillDTO
                    {
                        id = reader["ID"].ToString(),
                        orderID = reader["OrderID"].ToString(),
                        clientID = reader["ClientID"].ToString(),
                        employeeID = reader["EmployeeID"].ToString(),
                        billDate = (DateTime)reader["BillDate"],
                        totalPrice = (double)reader["TotalPrice"]
                    };

                    billList.Add(billDTO);
                }  
                return billList;
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

        public bool insert(BillDTO billDTO)
        {
            string query = "INSERT INTO Bill VALUES (@Id, @OrderId, @ClientId, @EmployeeId, @OrderDate, @TotalPrice)";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", billDTO.id);
                cmd.Parameters.AddWithValue("@OrderId", billDTO.orderID);
                cmd.Parameters.AddWithValue("@ClientId", billDTO.clientID);
                cmd.Parameters.AddWithValue("@EmployeeId", billDTO.employeeID);
                cmd.Parameters.AddWithValue("@OrderDate", billDTO.billDate);
                cmd.Parameters.AddWithValue("@TotalPrice", billDTO.totalPrice);

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

        public bool update(OrderDTO orderDTO)
        {
            string query = "UPDATE Bill SET ClientID = @ClientID, EmployeeID = @EmployeeID, BillDate = @OrderDate, TotalPrice = @TotalPrice WHERE OrderID = @Id";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", orderDTO.id);
                cmd.Parameters.AddWithValue("@ClientID", orderDTO.clientID);
                cmd.Parameters.AddWithValue("@EmployeeID", orderDTO.employeeID);
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
            string query = "DELETE FROM Bill WHERE OrderID = @Id";
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

        public bool updateTotalPriceAndDate(double totalPrice, DateTime billDate, string orderID)
        {
            string query = "UPDATE Bill SET TotalPrice = @TotalPrice, BillDate = @BillDate WHERE OrderID = @Id";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                cmd.Parameters.AddWithValue("@BillDate", billDate);
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

        public int countBill()
        {
            string query = "SELECT COUNT(*) FROM Bill";
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

        public string getLastestBillID()
        {
            string query = "SELECT TOP 1 ID FROM Bill ORDER BY CAST(SUBSTRING(ID, 3, LEN(ID) - 2) AS INT) DESC";
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
