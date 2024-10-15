using DAL;
using DTO;
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

        public List<ProductDTO> getList()
        {
            return productDAL.getProductList();
        }

        public bool addProduct(ProductDTO productDTO)
        {
            return productDAL.insert(productDTO);
        }

        public bool updateProduct(ProductDTO productDTO)
        {
            return productDAL.update(productDTO);
        }

        public bool deleteProduct(ProductDTO productDTO)
        {
            return productDAL.delete(productDTO);
        }
    }
}
