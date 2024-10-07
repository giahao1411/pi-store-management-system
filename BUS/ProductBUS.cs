using DAL;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ProductBUS
    {
        private ProductDAL productDAL = new ProductDAL();

        public int getNumOfProduct()
        {
            return productDAL.countProduct();
        }
    }
}
