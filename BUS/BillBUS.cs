using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class BillBUS
    {
        private BillDAL BillDAL = new BillDAL();

        public List<BillDTO> getList()
        {
            return BillDAL.getBillList();
        }

        public bool addBill(BillDTO billDTO)
        {
            return BillDAL.insert(billDTO);
        }

        public bool updateBill(OrderDTO orderDTO)
        {
            return BillDAL.update(orderDTO);
        }

        public bool deleteBill(OrderDTO orderDTO)
        {
            return BillDAL.delete(orderDTO);
        }

        public bool updateTotalPriceAndBillDate(double totalPrice, DateTime billDate, string orderID)
        {
            return BillDAL.updateTotalPriceAndDate(totalPrice, billDate, orderID);  
        }

        public int getNumOfBill()
        {
            return BillDAL.countBill();
        }

        public string getLastestBillID()
        {
            return BillDAL.getLastestBillID();
        }

        public List<string> getBillListByTimePeriod(string query)
        {
            return BillDAL.getBillListByTimePeriod(query);
        }
    }
}
