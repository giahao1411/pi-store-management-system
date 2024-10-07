using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
