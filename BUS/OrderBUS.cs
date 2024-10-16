using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
    }
}
