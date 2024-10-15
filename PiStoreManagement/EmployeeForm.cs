using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

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
            gridEmployee.MultiSelect = false;

            // set min date for date time picker
            dtpHireDate.MinDate = new DateTime(2020, 1, 1);
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
            txtID.Text = idGenerator();

            enable();
            btnCancel.Enabled = true;

            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPhone.Text) && !string.IsNullOrEmpty(txtAddress.Text) && !string.IsNullOrEmpty(txtSalary.Text) && !string.IsNullOrEmpty(dtpHireDate.Text))
            {
                if (regexEmail(txtEmail.Text) && regexPhoneNumber(txtPhone.Text) && isNumber(txtSalary.Text) && checkDate(dtpHireDate.Value))
                {
                    EmployeeDTO newEmployee = new EmployeeDTO
                    {
                        id = txtID.Text,
                        name = txtName.Text,
                        email = txtEmail.Text,
                        phone = txtPhone.Text,
                        address = txtAddress.Text,
                        salary = double.Parse(txtSalary.Text),
                        hireDate = dtpHireDate.Value,
                    };

                    bool isSuccess = EmployeeBUS.addEmployee(newEmployee);

                    if (isSuccess)
                    {
                        MessageBox.Show("Employee added successfully");
                        formload();
                    }
                    else
                    {
                        MessageBox.Show("An error occurr. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            enable();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            gridEmployee.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            EmployeeDTO deleteEmployee = currentEmployeeList.FirstOrDefault(emp => emp.id == txtID.Text);
            if (deleteEmployee != null)
            {
                DialogResult result = MessageBox.Show("Are you sure want to delete this employee?", "Delete Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    bool isSuccess = EmployeeBUS.deleteEmployee(deleteEmployee);
                    if(isSuccess)
                    {
                        MessageBox.Show("Employee deleted successfully");
                        formload();
                    }
                    else
                    {
                        MessageBox.Show("An error occurr. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPhone.Text) && !string.IsNullOrEmpty(txtAddress.Text) && !string.IsNullOrEmpty(txtSalary.Text) && !string.IsNullOrEmpty(dtpHireDate.Text))
            {
                if (regexEmail(txtEmail.Text) && regexPhoneNumber(txtPhone.Text) && isNumber(txtSalary.Text) && checkDate(dtpHireDate.Value))
                {
                    EmployeeDTO updateEmployee = currentEmployeeList.FirstOrDefault(emp => emp.id == txtID.Text);
                    if (updateEmployee != null)
                    {
                        updateEmployee.name = txtName.Text;
                        updateEmployee.email = txtEmail.Text;
                        updateEmployee.phone = txtPhone.Text;
                        updateEmployee.address = txtAddress.Text;
                        updateEmployee.salary = double.Parse(txtSalary.Text);
                        updateEmployee.hireDate = dtpHireDate.Value;

                        bool isSuccess = EmployeeBUS.updateEmployee(updateEmployee);

                        if (isSuccess)
                        {
                            MessageBox.Show("Employee updated successfully");
                            formload();
                        }
                        else
                        {
                            MessageBox.Show("An error occurr. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                string searchText = txtSearch.Text.Trim();

                List<EmployeeDTO> searchResult = searchEmployee(searchText);

                gridEmployee.DataSource = searchResult;
            } 
            else
            {
                formload();
            }
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = "employee.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                        {
                            // write column headers
                            for (int i = 0; i < gridEmployee.Columns.Count; i++)
                            {
                                sw.Write(gridEmployee.Columns[i].HeaderText);
                                if (i < gridEmployee.Columns.Count - 1)
                                {
                                    sw.Write(",");
                                }
                            }
                            sw.WriteLine();

                            // write row data
                            foreach (DataGridViewRow row in gridEmployee.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    for (int i = 0; i < gridEmployee.Columns.Count; i++)
                                    {
                                        string cellValue = row.Cells[i].Value?.ToString();

                                        // handle the DateTime datatype
                                        if (row.Cells[i].Value is DateTime dateValue)
                                        {
                                            cellValue = dateValue.ToString("MM/dd/yyyy");
                                        }

                                        // handle if has horizontal and vertical space in Column cell
                                        if (cellValue != null && (cellValue.Contains(",") || cellValue.Contains("\n") || cellValue.Contains("\r"))) 
                                        {
                                            cellValue = "\"" + cellValue.Replace("\"", "\"\"") + "\"";
                                        }
                                        sw.Write(cellValue);

                                        if (i < gridEmployee.Columns.Count - 1)
                                        {
                                            sw.Write(",");
                                        }
                                    }
                                    sw.WriteLine();
                                }
                            }
                        }
                        MessageBox.Show("Data exported successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } 
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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
            gridEmployee.Enabled = true;

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;

            displayEmployeeList();
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

            gridEmployee.Columns[5].Width = 93;
        }

        private void enable()
        {
            txtName.Enabled = true;
            txtEmail.Enabled = true;
            txtPhone.Enabled = true;
            txtAddress.Enabled = true;
            txtSalary.Enabled = true;
            dtpHireDate.Enabled = true;
        }

        private string idGenerator()
        {
            int maxIdNumber = currentEmployeeList
                .Select(emp => int.Parse(emp.id.Substring(2)))
                .DefaultIfEmpty(0)
                .Max();

            int newIdNumber = maxIdNumber + 1;
            string newId;

            // Generate the new ID based on the new numeric value
            if (newIdNumber >= 100)
            {
                newId = "EM" + newIdNumber.ToString();
            }
            else if (newIdNumber >= 10)
            {
                newId = "EM0" + newIdNumber.ToString();
            }
            else
            {
                newId = "EM00" + newIdNumber.ToString();
            }

            return newId;
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

        private bool isNumber(string salary)
        {
            double number;
            return double.TryParse(salary, out number);
        }

        private bool checkDate(DateTime date)
        {
            DateTime tomorrow = DateTime.Today.AddDays(1);

            return date.Date < tomorrow;
        }

        private List<EmployeeDTO> searchEmployee(string searchText)
        {
            return currentEmployeeList.Where(emp =>
                (emp.id.Contains(searchText)) ||
                (emp.name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (emp.email.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (emp.phone.Contains(searchText)) ||
                (emp.address.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (emp.salary.ToString().Contains(searchText)) ||
                (emp.hireDate.ToString("MM/dd/yyyy").Contains(searchText))
            ).ToList();
        }
    }
}
