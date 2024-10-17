using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace PiStoreManagement
{
    public partial class Dashboard : Form
    {
        private Form activateForm;

        public Dashboard()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            DisplayTags tags = new DisplayTags();
            openChildForm(tags);
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            EmployeeForm employee = new EmployeeForm();
            openChildForm(employee);
            pathLevel.Text = "Home / Dashboard / Employee";
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            ClientForm client = new ClientForm();
            openChildForm(client);
            pathLevel.Text = "Home / Dashboard / Client";
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ProductForm product = new ProductForm();
            openChildForm(product);
            pathLevel.Text = "Home / Dashboard / Product";
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            OrderForm order = new OrderForm();
            openChildForm(order);
            pathLevel.Text = "Home / Dashboard / Order";
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            BillForm bill = new BillForm(); 
            openChildForm(bill);
            pathLevel.Text = "Home / Dashboard / Bill";
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.panelMain.Controls.Clear();
            pathLevel.Text = "Home / Dashboard";
            DisplayTags tags = new DisplayTags();
            openChildForm(tags);
        }

        private void openChildForm(Form childForm)
        {
            if (activateForm != null)
            {
                activateForm.Close();
            }

            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelMain.Controls.Clear();
            this.panelMain.Controls.Add(childForm);
            this.panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
