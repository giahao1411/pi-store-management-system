using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiStoreManagement
{
    public partial class BillForm : Form
    {
        private List<BillDTO> billList = new List<BillDTO>();
        private List<OrderItemDTO> orderItemList = new List<OrderItemDTO>();
        private BillBUS billBUS = new BillBUS();
        private OrderItemBUS orderItemBUS = new OrderItemBUS();
        private string orderID;

        public BillForm()
        {
            InitializeComponent();
            formload();
            gridOrderItem.Enabled = false;
            gridBill.MultiSelect = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {

        }

        private void gridBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                gridBill.Focus();
                gridBill.Rows[e.RowIndex].Selected = true;

                var selectedRow = gridBill.SelectedRows[0];

                orderID = selectedRow.Cells[1].Value.ToString();

                displayOrderItem(orderID);
                btnExportPDF.Enabled = true;
            }
        }

        private void formload()
        {
            btnExportPDF.Enabled = false;

            gridOrderItem.DataSource = "";

            billList = billBUS.getList();
            gridBill.DataSource = billList;

            gridBill.Columns[0].HeaderText = "ID";
            gridBill.Columns[1].HeaderText = "Order ID";
            gridBill.Columns[2].HeaderText = "Client ID";
            gridBill.Columns[3].HeaderText = "Employee ID";
            gridBill.Columns[4].HeaderText = "Bill Date";
            gridBill.Columns[5].HeaderText = "Total Price";

            gridBill.Columns[4].Width = 125;
        }

        private void displayOrderItem(string orderID)
        {
            orderItemList = orderItemBUS.getList(orderID);
            gridOrderItem.DataSource = orderItemList;

            gridOrderItem.Columns[0].HeaderText = "ID";
            gridOrderItem.Columns[1].HeaderText = "Order ID";
            gridOrderItem.Columns[2].HeaderText = "Product ID";
            gridOrderItem.Columns[3].HeaderText = "Quantity";

            gridOrderItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            formload();
        }

        private void gridBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gridBill.Columns[e.ColumnIndex].Name == "billDate" && e.Value != null)
            {
                DateTime orderDate = (DateTime)e.Value;
                e.Value = orderDate.ToString("MM/dd/yyyy HH:mm:ss");
                e.FormattingApplied = true;
            }
        }
    }
}
