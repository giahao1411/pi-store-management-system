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
                        hireDate = (DateTime)reader["HireDate"]
                    };

                    employeeList.Add(employeeDTO);
                }
                return employeeList;
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

        public bool insert(EmployeeDTO employeeDTO)
        {
            string query = "INSERT INTO Employee VALUES (@Id, @Name, @Email, @Phone, @Address, @Salary, @HireDate)";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", employeeDTO.id);
                cmd.Parameters.AddWithValue("@Name", employeeDTO.name);
                cmd.Parameters.AddWithValue("@Email", employeeDTO.email);
                cmd.Parameters.AddWithValue("@Phone", employeeDTO.phone);
                cmd.Parameters.AddWithValue("@Address", employeeDTO.address);
                cmd.Parameters.AddWithValue("@Salary", employeeDTO.salary);
                cmd.Parameters.AddWithValue("@HireDate", employeeDTO.hireDate);

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

        public bool update(EmployeeDTO employeeDTO) 
        {
            string query = "UPDATE Employee SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address, Salary = @Salary, HireDate = @HireDate WHERE ID = @Id";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", employeeDTO.id);
                cmd.Parameters.AddWithValue("@Name", employeeDTO.name);
                cmd.Parameters.AddWithValue("@Email", employeeDTO.email);
                cmd.Parameters.AddWithValue("@Phone", employeeDTO.phone);
                cmd.Parameters.AddWithValue("@Address", employeeDTO.address);
                cmd.Parameters.AddWithValue("@Salary", employeeDTO.salary);
                cmd.Parameters.AddWithValue("@HireDate", employeeDTO.hireDate);

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

        public bool delete(EmployeeDTO employeeDTO)
        {
            string query = "DELETE FROM Employee WHERE ID = @Id";
            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", employeeDTO.id);

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
