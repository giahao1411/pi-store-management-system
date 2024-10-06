using System;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class LoginDAL
    {
        private Connection dbConn = new Connection();

        public bool verifyLogin(LoginDTO loginDTO)
        {
            string query = "SELECT COUNT(1) FROM Account WHERE Username = @username AND Password = @password AND Lock = 0";

            SqlConnection conn = null;

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", loginDTO.username);
                cmd.Parameters.AddWithValue("@password", loginDTO.password);

                // execute the query
                int result = (int)cmd.ExecuteScalar();

                return result == 1;
            }
            catch (Exception ex)
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
