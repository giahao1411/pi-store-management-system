using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
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
                        billDate = reader["BillDate"].ToString(),
                        totalPrice = (double)reader["TotalPrice"]
                    };

                    billList.Add(billDTO);
                }  
                return billList;
            }
            catch (Exception ex)
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
