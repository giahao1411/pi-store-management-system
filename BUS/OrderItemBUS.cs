using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BUS
{
    public class OrderItemBUS
    {
        private OrderItemDAL orderItemDAL = new OrderItemDAL();

        public List<OrderItemDTO> getList(string orderID)
        {
            return orderItemDAL.getOrderItemList(orderID);
        }

        public bool addOrderItem(OrderItemDTO orderItemDTO)
        {
            return orderItemDAL.insert(orderItemDTO);
        }

        public bool updateOrderItem(OrderItemDTO orderItemDTO)
        {
            return orderItemDAL.update(orderItemDTO);
        }

        public bool deleteOrderItem(OrderItemDTO orderItemDTO)
        {
            return orderItemDAL.delete(orderItemDTO);
        }

        public bool updateProductQuantity(OrderItemDTO orderItemDTO)
        {
            return orderItemDAL.updateProductQuantity(orderItemDTO);
        }

        public ProductDTO getProductById(OrderItemDTO orderItemDTO)
        {
            return orderItemDAL.getProductById(orderItemDTO);
        }

        public string getLastestOrderItemID()
        {
            return orderItemDAL.getLastestOrderItemID();
        }
    }
}
