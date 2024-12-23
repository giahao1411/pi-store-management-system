﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BUS;
using DTO;

namespace PiStoreManagement
{
    public partial class OrderForm : Form
    {
        private List<OrderDTO> orderList = new List<OrderDTO>();    
        private List<OrderItemDTO> orderItemList = new List<OrderItemDTO>();
        private OrderBUS orderBUS = new OrderBUS();
        private OrderItemBUS orderItemBUS = new OrderItemBUS();
        private BillBUS billBUS = new BillBUS();    

        public OrderForm()
        {
            InitializeComponent();
            dtpOrderDate.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            dtpOrderDate.ShowUpDown = true;

            gridOrder.MultiSelect = false;
            gridOrderItem.MultiSelect = false;

            formload();
            updateClientID();
            updateEmployeeID();
            updateProductID();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbProductID.Text) && !string.IsNullOrEmpty(txtQuantity.Text))
            {
                if (int.TryParse(txtQuantity.Text, out int number))
                {
                    if (isEnoughQuantity(int.Parse(txtQuantity.Text), cbProductID.Text))
                    {
                        OrderItemDTO newOrderItem = new OrderItemDTO
                        {
                            id = orderItemIDGenerator(orderItemBUS.getLastestOrderItemID()),
                            orderID = txtOrderID.Text,
                            productID = cbProductID.Text,
                            quantity = int.Parse(txtQuantity.Text)
                        };
                        orderItemList.Add(newOrderItem);

                        bool isSuccess = orderItemBUS.addOrderItem(newOrderItem) && orderItemBUS.updateProductForAdd(newOrderItem);

                        if (isSuccess)
                        {
                            displayOrderItemList(txtOrderID.Text);
                            double currentTotalPrice = calculateTotalPrice(orderItemList);
                            bool isUpdated = orderBUS.updateTotalPrice(currentTotalPrice, txtOrderID.Text) && billBUS.updateTotalPriceAndBillDate(currentTotalPrice, dtpOrderDate.Value, txtOrderID.Text);

                            if (isUpdated)
                            {
                                txtTotalPrice.Text = currentTotalPrice.ToString();
                                cbProductID.Text = "";
                                txtQuantity.Clear();
                                displayOrderItemList(txtOrderID.Text);
                                displayOrderList();
                            }
                            else
                            {
                                MessageBox.Show("An error occurr. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("An error occurr. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Exceed the available of product's quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            btnUpdateItem.Enabled = false;
            btnAddItem.Enabled = false;
            btnDeleteItem.Enabled = false;
            btnSaveItem.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            txtQuantity.Enabled = true;
            gridOrderItem.Enabled = false;
            gridOrder.Enabled = false;
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            OrderItemDTO deleteOrderItem = orderItemList.FirstOrDefault(odi => odi.productID == cbProductID.Text && odi.orderID == txtOrderID.Text);
            if (deleteOrderItem != null)
            {
                DialogResult result = MessageBox.Show("Are you sure want to delete this order item?", "Delete Order Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    orderItemList.Remove(deleteOrderItem);
                    bool isSuccess = orderItemBUS.deleteOrderItem(deleteOrderItem) && orderItemBUS.updateProductForDelete(deleteOrderItem);
                    if (isSuccess)
                    {
                        double currentTotalPrice = calculateTotalPrice(orderItemList);
                        bool isUpdated = orderBUS.updateTotalPrice(currentTotalPrice, txtOrderID.Text) && billBUS.updateTotalPriceAndBillDate(currentTotalPrice, dtpOrderDate.Value, txtOrderID.Text);

                        if (isUpdated)
                        {
                            txtTotalPrice.Text = currentTotalPrice.ToString();
                            cbProductID.Text = "";
                            txtQuantity.Clear();
                            MessageBox.Show("OrderItem deleted successfully");
                            displayOrderItemList(txtOrderID.Text);
                            displayOrderList();
                        }
                        else
                        {
                            MessageBox.Show("An error occurr when update total price. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("An error occurr. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQuantity.Text))
            {
                if (int.TryParse(txtQuantity.Text, out int number))
                {
                    if (isEnoughQuantity(int.Parse(txtQuantity.Text), cbProductID.Text))
                    {
                        OrderItemDTO updateOrderItem = orderItemList.FirstOrDefault(odi => odi.productID == cbProductID.Text && odi.orderID == txtOrderID.Text);
                        if (updateOrderItem != null)
                        {
                            int oldQuantity = updateOrderItem.quantity;
                            updateOrderItem.quantity = int.Parse(txtQuantity.Text);

                            bool isSuccess = orderItemBUS.updateOrderItem(updateOrderItem) && orderItemBUS.updateProductForUpdate(updateOrderItem, oldQuantity);
                            if (isSuccess)
                            {
                                double currentTotalPrice = calculateTotalPrice(orderItemList);
                                bool isUpdated = orderBUS.updateTotalPrice(currentTotalPrice, txtOrderID.Text) && billBUS.updateTotalPriceAndBillDate(currentTotalPrice, dtpOrderDate.Value, txtOrderID.Text);

                                if (isUpdated)
                                {
                                    txtTotalPrice.Text = currentTotalPrice.ToString();
                                    cbProductID.Text = "";
                                    txtQuantity.Clear();
                                    MessageBox.Show("OrderItem updated successfully");
                                    displayOrderItemList(txtOrderID.Text);
                                    displayOrderList();

                                    gridOrder.Enabled = true;
                                    gridOrderItem.Enabled = true;
                                    btnSaveItem.Enabled = false;
                                    btnAddItem.Enabled = true;
                                    cbProductID.Enabled = true;
                                    btnUpdate.Enabled = true;
                                    btnDelete.Enabled = true;
                                }
                                else
                                {
                                    MessageBox.Show("An error occurr when update total price. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                            else
                            {
                                MessageBox.Show("An error occurr. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Exceed the available product's quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtOrderID.Text = orderIDGenerator();
            cbClientID.Enabled = true;
            cbEmployeeID.Enabled = true;

            btnCancel.Enabled = true;
            btnAddItem.Enabled = true;

            if (!string.IsNullOrEmpty(cbClientID.Text) && !string.IsNullOrEmpty(cbEmployeeID.Text) && !string.IsNullOrEmpty(dtpOrderDate.Text))
            {
                OrderDTO newOrder = new OrderDTO
                {
                    id = txtOrderID.Text,
                    clientID = cbClientID.Text,
                    employeeID = cbEmployeeID.Text,
                    orderDate = dtpOrderDate.Value,
                    totalPrice = 0
                };

                BillDTO newBill = new BillDTO
                {
                    id = billIDGenerator(billBUS.getLastestBillID()),
                    orderID = txtOrderID.Text,
                    clientID = cbClientID.Text,
                    employeeID = cbEmployeeID.Text,
                    billDate = dtpOrderDate.Value,
                    totalPrice = 0
                };

                bool isSuccess = orderBUS.addOrder(newOrder) && billBUS.addBill(newBill);

                if (isSuccess)
                {
                    MessageBox.Show("Order added successfully");
                    formload();
                } 
                else
                {
                    MessageBox.Show("An error occurr. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cbClientID.Enabled = true;
            cbEmployeeID.Enabled = true;
            cbProductID.Enabled = false;
            txtQuantity.Enabled = false;
            btnUpdate.Enabled = false;

            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            gridOrder.Enabled = false;
            gridOrderItem.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OrderDTO deleteOrder = orderList.FirstOrDefault(ord => ord.id == txtOrderID.Text);
            if (deleteOrder != null)
            {
                DialogResult result = MessageBox.Show("Are you sure want to delete this order?", "Delete Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    deleteAndUpdate(orderItemList);
                    bool isSuccess = billBUS.deleteBill(deleteOrder) && orderBUS.deleteOrder(deleteOrder);
                    if (isSuccess)
                    {
                        MessageBox.Show("Order deleted successfully");
                        formload();
                    }
                    else
                    {
                        MessageBox.Show("An error occurr. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbClientID.Text) && !string.IsNullOrEmpty(cbEmployeeID.Text))
            {
                OrderDTO updateOrder = orderList.FirstOrDefault(ord => ord.id == txtOrderID.Text);
                if (updateOrder != null)
                {
                    updateOrder.clientID = cbClientID.Text;
                    updateOrder.employeeID = cbEmployeeID.Text;
                    updateOrder.orderDate = dtpOrderDate.Value;
                    updateOrder.totalPrice = double.Parse(txtTotalPrice.Text);

                    bool isSuccess = orderBUS.updateOrder(updateOrder) && billBUS.updateBill(updateOrder);

                    if (isSuccess)
                    {
                        MessageBox.Show("Order updated successfully");
                        formload();
                    }
                    else
                    {
                        MessageBox.Show("An error occurr. Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = "order.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                        {
                            // write column headers
                            for (int i = 0; i < gridOrder.Columns.Count; i++)
                            {
                                sw.Write(gridOrder.Columns[i].HeaderText);
                                if (i < gridOrder.Columns.Count - 1)
                                {
                                    sw.Write(",");
                                }
                            }
                            sw.WriteLine();

                            // write row data
                            foreach (DataGridViewRow row in gridOrder.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    for (int i = 0; i < gridOrder.Columns.Count; i++)
                                    {
                                        string cellValue = row.Cells[i].Value?.ToString();

                                        // handle the DateTime datatype
                                        if (row.Cells[i].Value is DateTime dateValue)
                                        {
                                            cellValue = dateValue.ToString("MM/dd/yyyy hh:mm:ss");
                                        }

                                        // handle if has horizontal and vertical space in Column cell
                                        if (cellValue != null && (cellValue.Contains(",") || cellValue.Contains("\n") || cellValue.Contains("\r")))
                                        {
                                            cellValue = "\"" + cellValue.Replace("\"", "\"\"") + "\"";
                                        }
                                        sw.Write(cellValue);

                                        if (i < gridOrder.Columns.Count - 1)
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            formload();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                string searchText = txtSearch.Text.Trim();

                List<OrderDTO> searchResult = searchOrder(searchText);

                gridOrder.DataSource = searchResult;
            }
            else
            {
                formload();
            }
        }

        private void gridOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gridOrder.Columns[e.ColumnIndex].Name == "orderDate" && e.Value != null)
            {
                DateTime orderDate = (DateTime)e.Value;
                e.Value = orderDate.ToString("MM/dd/yyyy HH:mm:ss");
                e.FormattingApplied = true;
            }
        }

        private void gridOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                gridOrder.Focus();
                gridOrder.Rows[e.RowIndex].Selected = true;

                var selectedRow = gridOrder.SelectedRows[0];

                txtOrderID.Text = selectedRow.Cells[0].Value.ToString();
                cbClientID.Text = selectedRow.Cells[1].Value.ToString();
                cbEmployeeID.Text = selectedRow.Cells[2].Value.ToString();
                dtpOrderDate.Text = selectedRow.Cells[3].Value.ToString();
                txtTotalPrice.Text = selectedRow.Cells[4].Value.ToString();

                displayOrderItemList(txtOrderID.Text);

                cbProductID.Enabled = true;
                txtQuantity.Enabled = true;

                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnCancel.Enabled = true;
                btnAddItem.Enabled = true;

                cbClientID.Enabled = false;
                cbEmployeeID.Enabled = false;
            }
        }

        private void gridOrderItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                gridOrderItem.Focus();
                gridOrderItem.Rows[e.RowIndex].Selected = true;

                var selecetedRow = gridOrderItem.SelectedRows[0];

                cbProductID.Text = selecetedRow.Cells[2].Value.ToString();
                txtQuantity.Text = selecetedRow.Cells[3].Value.ToString();

                cbProductID.Enabled = false;
                txtQuantity.Enabled = false;

                btnUpdateItem.Enabled = true;
                btnDeleteItem.Enabled = true;
                btnAddItem.Enabled = false;
            }
        }

        private void formload()
        {
            txtOrderID.Clear();
            txtQuantity.Clear();
            txtTotalPrice.Clear();
            dtpOrderDate.Text = "";
            cbClientID.Text = "";
            cbEmployeeID.Text = "";
            cbProductID.Text = "";


            txtOrderID.Enabled = false;
            cbClientID.Enabled = false;
            cbEmployeeID.Enabled = false;
            dtpOrderDate.Enabled = false;
            cbProductID.Enabled = false;
            txtQuantity.Enabled = false;
            txtTotalPrice.Enabled = false;

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;

            btnAddItem.Enabled = false;
            btnUpdateItem.Enabled = false;
            btnDeleteItem.Enabled = false;
            btnSaveItem.Enabled = false;

            gridOrderItem.DataSource = "";

            gridOrder.Enabled = true;
            gridOrderItem.Enabled = true;

            displayOrderList();
        }

        private void updateClientID()
        {
            cbClientID.Items.Clear();
            ClientBUS clientBUS = new ClientBUS();
            List<ClientDTO> clientList = clientBUS.getList();

            foreach (ClientDTO client in clientList)
            {
                cbClientID.Items.Add(client.id);
            }
        }

        private void updateEmployeeID()
        {
            cbEmployeeID.Items.Clear();
            EmployeeBUS employeeBUS = new EmployeeBUS();
            List<EmployeeDTO> employeeList = employeeBUS.getList();

            foreach (EmployeeDTO employee in employeeList)
            {
                cbEmployeeID.Items.Add(employee.id);
            }
        }

        private void updateProductID()
        {
            cbProductID.DataSource = null;
            ProductBUS productBUS = new ProductBUS();
            List<ProductDTO> productList = productBUS.getList();

            var displayProductId = productList.Select(p => new
            {
                Display = p.quantity > 0 ? p.id : $"{p.id} (Unvailable)",
                Value = p.id,
                isAvailable = p.quantity > 0
            }).ToList();

            cbProductID.DataSource = displayProductId;
            cbProductID.DisplayMember = "Display";
            cbProductID.ValueMember = "Value";
            cbProductID.SelectedIndex = -1;
        }

        private void displayOrderList()
        {
            orderList = orderBUS.getList();
            gridOrder.DataSource = orderList;

            gridOrder.Columns[0].HeaderText = "ID";
            gridOrder.Columns[1].HeaderText = "Client ID";
            gridOrder.Columns[2].HeaderText = "Employee ID";
            gridOrder.Columns[3].HeaderText = "Order Date";
            gridOrder.Columns[4].HeaderText = "Total Price";

            gridOrder.Columns[1].Width = 150;
            gridOrder.Columns[2].Width = 150;
            gridOrder.Columns[3].Width = 200;
            gridOrder.Columns[4].Width = 146;
        }

        private void displayOrderItemList(string orderID)
        {
            orderItemList = orderItemBUS.getList(orderID);
            gridOrderItem.DataSource = orderItemList;

            gridOrderItem.Columns[0].HeaderText = "ID";
            gridOrderItem.Columns[1].HeaderText = "Order ID";
            gridOrderItem.Columns[2].HeaderText = "Product ID";
            gridOrderItem.Columns[3].HeaderText = "Quantity";

            gridOrderItem.Columns[3].Width = 59;
        }

        private string orderIDGenerator()
        {
            int maxIdNumber = orderList
                .Select(ord => int.Parse(ord.id.Substring(2)))
                .DefaultIfEmpty(0)
                .Max();

            int newIdNumber = maxIdNumber + 1;
            string newId;

            // Generate the new ID based on the new numeric value
            if (newIdNumber >= 100)
            {
                newId = "OD" + newIdNumber.ToString();
            }
            else if (newIdNumber >= 10)
            {
                newId = "OD0" + newIdNumber.ToString();
            }
            else
            {
                newId = "OD00" + newIdNumber.ToString();
            }

            return newId;
        }

        private string orderItemIDGenerator(string lastestID)
        {
            if (string.IsNullOrEmpty(lastestID))
            {
                return "OT001";
            }

            int maxIdNumber = int.Parse(lastestID.Substring(2));

            int newIdNumber = maxIdNumber + 1;
            string newId;

            // Generate the new ID based on the new numeric value
            if (newIdNumber >= 100)
            {
                newId = "OT" + newIdNumber.ToString();
            }
            else if (newIdNumber >= 10)
            {
                newId = "OT0" + newIdNumber.ToString();
            }
            else
            {
                newId = "OT00" + newIdNumber.ToString();
            }

            return newId;
        }

        private string billIDGenerator(string lastestID)
        {
            if (string.IsNullOrEmpty(lastestID)) 
            {
                return "BI001";
            }

            int maxIdNumber = int.Parse(lastestID.Substring(2));

            int newIdNumber = maxIdNumber + 1;
            string newId;

            // Generate the new ID based on the new numeric value
            if (newIdNumber >= 100)
            {
                newId = "BI" + newIdNumber.ToString();
            }
            else if (newIdNumber >= 10)
            {
                newId = "BI0" + newIdNumber.ToString();
            }
            else
            {
                newId = "BI00" + newIdNumber.ToString();
            }

            return newId;
        }

        private void cbProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProductID.SelectedItem != null)
            {
                var selectedProduct = cbProductID.SelectedItem;

                var displayProperty = selectedProduct.GetType().GetProperty("Display");
                var isAvailableProperty = selectedProduct.GetType().GetProperty("isAvailable");

                if (displayProperty != null && isAvailableProperty != null)
                {
                    string displayValue = displayProperty.GetValue(selectedProduct)?.ToString();
                    bool isAvailable = (bool)isAvailableProperty.GetValue(selectedProduct);

                    if (!isAvailable)
                    {
                        MessageBox.Show("This product is unavailable. Please select another one.", "Unavailable Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cbProductID.SelectedIndex = -1;
                    }
                }
            }
        }

        private double calculateTotalPrice(List<OrderItemDTO> currentOrderItem)
        {
            double totalPrice = 0;

            foreach (OrderItemDTO item in currentOrderItem)
            {
                ProductDTO product = orderItemBUS.getProductById(item.productID);
                if (product != null) 
                {
                    totalPrice += product.price * item.quantity;
                }
            }

            return totalPrice;
        }

        private bool isEnoughQuantity(int userQuantity, string productID)
        {
            ProductDTO selectedProduct = orderItemBUS.getProductById(productID);
            if (userQuantity <= selectedProduct.quantity) return true;
            return false;
        }

        private void deleteAndUpdate(List<OrderItemDTO> orderItemList)
        {
            foreach (OrderItemDTO item in orderItemList)
            {
                orderItemBUS.deleteOrderItem(item);
                orderItemBUS.updateProductForDelete(item);
            }
        }

        private List<OrderDTO> searchOrder(string searchText)
        {
            return orderList.Where(ord =>
                (ord.id.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) || 
                (ord.clientID.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (ord.employeeID.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                (ord.orderDate.ToString("MM/dd/yyyy hh:mm:ss").Contains(searchText)) ||
                (ord.totalPrice.ToString().Contains(searchText))
            ).ToList();
        }
    }
}
