using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class ProductDAL
    {
        private Connection dbConn = new Connection();

        public List<ProductDTO> getProductList() 
        {
            SqlConnection conn = null;
            string query = "SELECT * FROM Product";
            List<ProductDTO> productList = new List<ProductDTO>();

            try
            {
                conn = dbConn.getConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    ProductDTO productDTO = new ProductDTO
                    {
                        id = reader["ID"].ToString(),
                        name = reader["Name"].ToString(),
                        description = reader["Description"].ToString(),
                        price = (double)reader["Price"],
                        quantity = (int)reader["Quantity"]
                    };

                    productList.Add(productDTO);   
                }
                return productList;
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

        public int countProduct()
        {
            string query = "SELECT COUNT(*) FROM Prodcut";
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
