using System.Data.SqlClient;

namespace DAL
{
    public class Connection
    {
        private string connectionString = "Data Source=DESKTOP-44OH4C4\\SQLEXPRESS;Initial Catalog=PiStoreManagementDataBase;Integrated Security=True;";

        public SqlConnection getConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;    
        }

        public void closeConnection(SqlConnection conn) 
        {
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
