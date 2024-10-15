using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace PiStoreManagement
{
    public partial class ClientForm : Form
    {
        private List<ClientDTO> currentClientList = new List<ClientDTO>();
        private ClientBUS ClientBUS = new ClientBUS();

        public ClientForm()
        {
            InitializeComponent();
            formload();
            displayClientList();
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

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {

        }

        private void gridClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void formload()
        {
            txtID.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();

            txtID.Enabled = false;
            txtName.Enabled = false;
            txtEmail.Enabled = false;
            txtPhone.Enabled = false;
            txtAddress.Enabled = false;

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
        }

        private void displayClientList()
        {
            currentClientList = ClientBUS.getList();
            gridClient.DataSource = currentClientList;

            gridClient.Columns[0].HeaderText = "ID";
            gridClient.Columns[1].HeaderText = "Name";
            gridClient.Columns[2].HeaderText = "Email";
            gridClient.Columns[3].HeaderText = "Phone";
            gridClient.Columns[4].HeaderText = "Address";

            gridClient.Columns[1].Width = 133;
            gridClient.Columns[2].Width = 160;
            gridClient.Columns[4].Width = 200;

        }
    }
}
