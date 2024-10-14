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
    public partial class EmployeeForm : Form
    {
        private List<EmployeeDTO> currentEmployeeList = new List<EmployeeDTO>();
        private EmployeeBUS EmployeeBUS = new EmployeeBUS();

        public EmployeeForm()
        {
            InitializeComponent();
            formload();   
            displayEmployeeList();
        }

        private void gridEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                gridEmployee.Focus();
                gridEmployee.Rows[e.RowIndex].Selected = true;

                var selectedRow = gridEmployee.SelectedRows[0];

                txtID.Text = selectedRow.Cells[0].Value.ToString();
                txtName.Text = selectedRow.Cells[1].Value.ToString();
                txtEmail.Text = selectedRow.Cells[2].Value.ToString();
                txtPhone.Text = selectedRow.Cells[3].Value.ToString();
                txtAddress.Text = selectedRow.Cells[4].Value.ToString();
                txtSalary.Text = selectedRow.Cells[5].Value.ToString();
                dtpHireDate.Text = selectedRow.Cells[6].Value.ToString();


                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            formload();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void formload()
        {
            txtID.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtSalary.Clear();
            dtpHireDate.Text = "";

            txtID.Enabled = false;
            txtName.Enabled = false;
            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtAddress.Enabled = false;
            txtSalary.Enabled = false;
            dtpHireDate.Enabled = false;

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
        }

        private void displayEmployeeList()
        {
            currentEmployeeList = EmployeeBUS.getList();
            gridEmployee.DataSource = currentEmployeeList;

            gridEmployee.Columns[0].HeaderText = "ID";
            gridEmployee.Columns[1].HeaderText = "Name";
            gridEmployee.Columns[2].HeaderText = "Email";
            gridEmployee.Columns[3].HeaderText = "Phone";
            gridEmployee.Columns[4].HeaderText = "Address";
            gridEmployee.Columns[5].HeaderText = "Salary";
            gridEmployee.Columns[6].HeaderText = "Hire Date";
        }
    }
}
