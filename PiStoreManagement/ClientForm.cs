using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
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
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtID.Text = idGenerator();

            enable();
            btnCancel.Enabled = true;

            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPhone.Text) && !string.IsNullOrEmpty(txtAddress.Text))
            {
                if (regexEmail(txtEmail.Text) && regexPhoneNumber(txtPhone.Text))
                {
                    ClientDTO newClient = new ClientDTO
                    {
                        id = txtID.Text,
                        name = txtName.Text,
                        email = txtEmail.Text,
                        phone = txtPhone.Text,
                        address = txtAddress.Text
                    };

                    bool isSuccess = ClientBUS.addClient(newClient);

                    if (isSuccess)
                    {
                        MessageBox.Show("Client addedd successfully");
                        formload();
                    }
                    else
                    {
                        MessageBox.Show("An error occurr. Please try again");
                    }
                } 
                else
                {
                    MessageBox.Show("Invalid input. PLease try again");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            enable();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ClientDTO deleteClient = currentClientList.FirstOrDefault(cli => cli.id == txtID.Text);
            if(deleteClient != null)
            {
                DialogResult result = MessageBox.Show("Are you sure want to delete this client?", "Delete Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    bool isSuccess = ClientBUS.deleteClient(deleteClient);
                    if (isSuccess)
                    {
                        MessageBox.Show("Client deleted successfully");
                        formload();
                    } 
                    else
                    {
                        MessageBox.Show("An error occurr. Please try again");
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            formload();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPhone.Text) && !string.IsNullOrEmpty(txtAddress.Text))
            {
                if (regexEmail(txtEmail.Text) && regexPhoneNumber(txtPhone.Text))
                {
                    ClientDTO updateClient = currentClientList.FirstOrDefault(cli => cli.id == txtID.Text);
                    if(updateClient != null)
                    {
                        updateClient.name = txtName.Text;
                        updateClient.email = txtEmail.Text;
                        updateClient.phone = txtPhone.Text;
                        updateClient.address = txtAddress.Text;
                    }

                    bool isSuccess = ClientBUS.updateClient(updateClient);

                    if (isSuccess)
                    {
                        MessageBox.Show("Client update successfully");
                        formload();
                    }
                    else
                    {
                        MessageBox.Show("An rrror occurr. Please try again");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input. PLease try again");
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {

        }

        private void gridClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                gridClient.Focus();
                gridClient.Rows[e.RowIndex].Selected = true;

                var selectedRow = gridClient.SelectedRows[0];

                txtID.Text = selectedRow.Cells[0].Value.ToString();
                txtName.Text = selectedRow.Cells[1].Value.ToString();
                txtEmail.Text = selectedRow.Cells[2].Value.ToString();
                txtPhone.Text = selectedRow.Cells[3].Value.ToString();
                txtAddress.Text = selectedRow.Cells[4].Value.ToString();

                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnCancel.Enabled = true;
            }
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

            displayClientList();
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

        private void enable()
        {
            txtName.Enabled = true;
            txtEmail.Enabled = true;
            txtPhone.Enabled = true;
            txtAddress.Enabled = true;
        }

        private string idGenerator()
        {
            int idNumber = currentClientList.Count() + 1;

            if (idNumber > 100)
            {
                return "CL" + idNumber.ToString();
            } 
            if (idNumber > 10)
            {
                return "CL0" + idNumber.ToString();
            }
            return "CL00" + idNumber.ToString();
        }

        private bool regexEmail(string email)
        {
            string regexEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, regexEmail);
        }

        private bool regexPhoneNumber(string phone)
        {
            string regexPhoneNumber = @"^[\+]?[0-9]{0,3}\W?[0]?[0-9]{9}$";

            return Regex.IsMatch(phone, regexPhoneNumber, RegexOptions.IgnoreCase) && phone.Length == 10;
        }
    }
}
