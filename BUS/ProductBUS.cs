using DAL;
using DTO;
using System.Collections.Generic;


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
