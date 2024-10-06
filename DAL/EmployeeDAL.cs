using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class EmployeeDAL
    {
        private Connection dbConn = new Connection();

        public List<EmployeeDTO> getEmployyeeList()
        {
            SqlConnection conn = null;
            string query = "SELECT * FROM Employee";
            List<EmployeeDTO> employeeList = new List<EmployeeDTO>();

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeDTO employeeDTO = new EmployeeDTO
                    {
                        id = reader["ID"].ToString(),
                        name = reader["Name"].ToString(),
                        email = reader["Email"].ToString(),
                        phone = reader["Phone"].ToString(),
                        address = reader["Address"].ToString(),
                        salary = (double)reader["Salary"],
                        hireDate = reader["HireDate"].ToString()
                    };

                    employeeList.Add(employeeDTO);
                }
                return employeeList;
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

        public int countEmployee()
        {
            string query = "SELECT COUNT(*) FROM Employee";
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
