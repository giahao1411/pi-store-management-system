﻿using BUS;
using System.Windows.Forms;

namespace PiStoreManagement
{
    public partial class DisplayTags : Form
    {
        private EmployeeBUS employeeBUS = new EmployeeBUS();
        private ClientBUS clientBUS = new ClientBUS();
        private ProductBUS productBUS = new ProductBUS();
        private OrderBUS orderBUS = new OrderBUS();
        private BillBUS billBUS = new BillBUS();

        public DisplayTags()
        {
            InitializeComponent();
            labelNumOfEmployee.Text = employeeBUS.getNumOfEmployee().ToString();
            labelNumOfClient.Text = clientBUS.getNumOfClient().ToString();
            labelNumOfProduct.Text = productBUS.getNumOfProduct().ToString();
            labelNumOfOrder.Text = orderBUS.getNumOfOrder().ToString();
            labelNumOfBill.Text = billBUS.getNumOfBill().ToString();
        }
    }
}
