using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BillBUS
    {
        private BillDAL BillDAL = new BillDAL();

        public int getNumOfBill()
        {
            return BillDAL.countBill();
        }
    }
}
