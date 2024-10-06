using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace PiStoreManagement
{
    public partial class Dashboard : Form
    {
        private EmployeeBUS employeeBUS = new EmployeeBUS();
        private ClientBUS clientBUS = new ClientBUS();

        public Dashboard()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            labelNumOfEmployee.Text = employeeBUS.getNumOfEmployee().ToString();
            labelNumOfClient.Text = clientBUS.getNumOfClient().ToString();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
