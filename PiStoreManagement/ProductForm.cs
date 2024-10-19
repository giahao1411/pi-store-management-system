using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BUS;
using DTO;

namespace PiStoreManagement
{
    public partial class ProductForm : Form
    {
        List<ProductDTO> currentProductList = new List<ProductDTO>();
        ProductBUS ProductBUS = new ProductBUS();

        public ProductForm()
        {
            InitializeComponent();
            formload();
            gridProduct.MultiSelect = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtID.Text = idGenerator();

            enable();
            btnCancel.Enabled = true;

            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtPrice.Text) && !string.IsNullOrEmpty(txtQuantity.Text) && !string.IsNullOrEmpty(rtxtDescription.Text))
            {
                if (isNumber(txtPrice.Text) && isNumber(txtQuantity.Text))
                {
                    ProductDTO newProduct = new ProductDTO
                    {
                        id = txtID.Text,
                        name = txtName.Text,
                        description = rtxtDescription.Text,
                        price = double.Parse(txtPrice.Text),
                        quantity = int.Parse(txtQuantity.Text)
                    };

                    bool isSuccess = ProductBUS.addProduct(newProduct);

                    if (isSuccess)
                    {
                        MessageBox.Show("Product added successfully");
                        formload();
                    } else
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
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            gridProduct.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ProductDTO deleteProduct = currentProductList.FirstOrDefault(pro => pro.id == txtID.Text);
            if (deleteProduct != null)
            {
                DialogResult result = MessageBox.Show("Are you sure want to delete this product?", "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    bool isSuccess = ProductBUS.deleteProduct(deleteProduct);
                    if (isSuccess)
                    {
                        MessageBox.Show("Product deleted successfully");
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
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtPrice.Text) && !string.IsNullOrEmpty(txtQuantity.Text) && !string.IsNullOrEmpty(rtxtDescription.Text))
            {
                if (isNumber(txtPrice.Text) && isNumber(txtQuantity.Text))
                {
                    ProductDTO updateProduct = currentProductList.FirstOrDefault(pro => pro.id == txtID.Text);
                    if(updateProduct != null)
                    {
                        updateProduct.name = txtName.Text;
                        updateProduct.description = rtxtDescription.Text;
                        updateProduct.price = double.Parse(txtPrice.Text);
                        updateProduct.quantity = int.Parse(txtQuantity.Text);
                    }

                    bool isSuccess = ProductBUS.updateProduct(updateProduct);

                    if (isSuccess)
                    {
                        MessageBox.Show("Product added successfully");
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                string searchText = txtSearch.Text.Trim();

                List<ProductDTO> searchResult = searchProduct(searchText);

                gridProduct.DataSource = searchResult;
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
                sfd.FileName = "product.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                        {
                            // write column headers
                            for (int i = 0; i < gridProduct.Columns.Count; i++)
                            {
                                sw.Write(gridProduct.Columns[i].HeaderText);
                                if (i < gridProduct.Columns.Count - 1)
                                {
                                    sw.Write(",");
                                }
                            }
                            sw.WriteLine();

                            // write row data
                            foreach (DataGridViewRow row in gridProduct.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    for (int i = 0; i < gridProduct.Columns.Count; i++)
                                    {
                                        string cellValue = row.Cells[i].Value?.ToString();

                                        // handle if has horizontal and vertical space in column cell
                                        if (cellValue != null && (cellValue.Contains(",") || cellValue.Contains("\n") || cellValue.Contains("\r")))
                                        {
                                            cellValue = "\"" + cellValue.Replace("\"", "\"\"") + "\"";
                                        }
                                        sw.Write(cellValue);

                                        if (i < gridProduct.Columns.Count - 1)
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

        private void gridProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                gridProduct.Focus();
                gridProduct.Rows[e.RowIndex].Selected = true;

                var selectedRow = gridProduct.SelectedRows[0];

                txtID.Text = selectedRow.Cells[0].Value.ToString();
                txtName.Text = selectedRow.Cells[1].Value.ToString();
                rtxtDescription.Text = selectedRow.Cells[2].Value.ToString();
                txtPrice.Text = selectedRow.Cells[3].Value.ToString();
                txtQuantity.Text = selectedRow.Cells[4].Value.ToString();

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
            txtPrice.Clear();
            txtQuantity.Clear();
            rtxtDescription.Clear();

            txtID.Enabled = false;
            txtName.Enabled = false;
            txtPrice.Enabled = false;
            txtQuantity.Enabled = false;
            rtxtDescription.Enabled = false;
            gridProduct.Enabled = true;

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;

            displayProductList();
        }

        private void displayProductList()
        {
            currentProductList = ProductBUS.getList();
            gridProduct.DataSource = currentProductList;

            gridProduct.Columns[0].HeaderText = "ID";
            gridProduct.Columns[1].HeaderText = "Name";
            gridProduct.Columns[2].HeaderText = "Description";
            gridProduct.Columns[3].HeaderText = "Price";
            gridProduct.Columns[4].HeaderText = "Quantity";

            gridProduct.Columns[1].Width = 180;
            gridProduct.Columns[2].Width = 213;
        }

        private void enable()
        {
            txtName.Enabled = true;
            txtPrice.Enabled = true;
            txtQuantity.Enabled = true;
            rtxtDescription.Enabled = true;
        }

        private string idGenerator()
        {
            int maxIdNumber = currentProductList
                .Select(pro => int.Parse(pro.id.Substring(2))) 
                .DefaultIfEmpty(0)
                .Max();

            int newIdNumber = maxIdNumber + 1; 
            string newId;

            // Generate the new ID based on the new numeric value
            if (newIdNumber >= 100)
            {
                newId = "PD" + newIdNumber.ToString();
            }
            else if (newIdNumber >= 10)
            {
                newId = "PD0" + newIdNumber.ToString();
            }
            else
            {
                newId = "PD00" + newIdNumber.ToString();
            }

            return newId; 
        }

        private bool isNumber(string input)
        {
            double number;
            return double.TryParse(input, out number);
        }

        private List<ProductDTO> searchProduct(string searchText)
        {
            return currentProductList.Where(pro =>
                (pro.id.Contains(searchText)) ||
                (pro.name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (pro.description.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (pro.price.ToString().Contains(searchText)) ||
                (pro.quantity.ToString().Contains(searchText))
            ).ToList();
        }
    }
}
