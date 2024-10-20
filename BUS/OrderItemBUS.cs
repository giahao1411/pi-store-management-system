using System.Collections.Generic;
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

        public ProductDTO getProductById(string productID)
        {
            return orderItemDAL.getProductById(productID);
        }

        public bool deleteOrderItemByOrderId(string orderID)
        {
            return orderItemDAL.deleteOrderItemByOrderId(orderID);
        }

        public string getLastestOrderItemID()
        {
            return orderItemDAL.getLastestOrderItemID();
        }

        public bool updateProductForAdd(OrderItemDTO orderItem)
        {
            return orderItemDAL.updateProductQuantityAdd(orderItem);
        }

        public bool updateProductForUpdate(OrderItemDTO newOrderItem, int oldQuantity)
        {
            return orderItemDAL.updateProductQuantityUpdate(newOrderItem, oldQuantity);
        }

        public bool updateProductForDelete(OrderItemDTO orderItem)
        {
            return orderItemDAL.updateProductQuantityDelete(orderItem);
        }
    }
}
