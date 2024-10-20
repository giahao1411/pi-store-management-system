using DAL;
using DTO;
using System.Collections.Generic;

namespace BUS
{
    public class OrderBUS
    {
        private OrderDAL orderDAL = new OrderDAL();

        public int getNumOfOrder()
        {
            return orderDAL.countOrder();
        }

        public List<OrderDTO> getList()
        {
            return orderDAL.getOrderList();
        }

        public bool addOrder(OrderDTO orderDTO)
        {
            return orderDAL.insert(orderDTO);
        }

        public bool updateOrder(OrderDTO orderDTO)
        {
            return orderDAL.update(orderDTO);
        }

        public bool deleteOrder(OrderDTO orderDTO)
        {
            return orderDAL.delete(orderDTO);
        }

        public bool updateTotalPrice(double totalPrice, string orderID)
        {
            return orderDAL.updateTotalPrice(totalPrice, orderID);
        }
    }
}
