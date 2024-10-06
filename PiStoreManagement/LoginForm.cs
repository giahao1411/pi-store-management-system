using System;
using System.Windows.Forms;
using BUS;
using DTO;

namespace PiStoreManagement
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtPassword.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // create a LoginDTO Object to store the current login form
            LoginDTO loginDTO = new LoginDTO
            {
                username = txtUsername.Text,
                password = txtPassword.Text,
            };

            LoginBUS loginBUS = new LoginBUS();

            if (loginBUS.validateLogin(loginDTO))
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();

                this.Hide();
            } else
            {
                MessageBox.Show("Invalid username or password, or account is locked");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // close the entire application
            Application.Exit();
        }
    }
}
