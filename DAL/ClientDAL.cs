using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ClientDAL
    {
        private Connection dbConn = new Connection();

        public List<ClientDTO> getClientList()
        {
            SqlConnection conn = null;
            string query = "SELECT * FROM Client";
            List<ClientDTO> clientList = new List<ClientDTO>();

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClientDTO clientDTO = new ClientDTO
                    {
                        id = reader["ID"].ToString(),
                        name = reader["Name"].ToString(),
                        email = reader["Email"].ToString(),
                        phone = reader["Phone"].ToString(),
                        address = reader["Address"].ToString()
                    };

                    clientList.Add(clientDTO);
                }
                return clientList;
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

        public int countClient()
        {
            string query = "SELECT COUNT(*) FROM Client";
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
