using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PiStoreManagement
{
    public partial class BillForm : Form
    {
        private List<BillDTO> billList = new List<BillDTO>();
        private List<OrderItemDTO> orderItemList = new List<OrderItemDTO>();
        private BillBUS billBUS = new BillBUS();
        private OrderItemBUS orderItemBUS = new OrderItemBUS();
        private string orderID;
        private string billID;

        public BillForm()
        {
            InitializeComponent();
            formload();
            gridOrderItem.Enabled = false;
            gridBill.MultiSelect = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                string searchText = txtSearch.Text.Trim();

                List<BillDTO> searchResult = searchBill(searchText);

                gridBill.DataSource = searchResult;
            } 
            else
            {
                formload();
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            BillDTO selectedBill = billList.FirstOrDefault(bil => bil.id == billID);
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF files (*.pdf)|*.pdf";
                sfd.FileName = "bill.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Document pdfDoc = new Document(PageSize.A4, 25f, 25f, 30f, 30f);
                        PdfWriter.GetInstance(pdfDoc, new FileStream(sfd.FileName, FileMode.Create));
                        pdfDoc.Open();

                        Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
                        Font regularFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                        Font smallFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

                        // add the title
                        Paragraph storeTitle = new Paragraph("PI STORE", titleFont);
                        storeTitle.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(storeTitle);

                        Paragraph billTitle = new Paragraph("Bill Receipt", regularFont);
                        billTitle.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(billTitle);

                        // add space
                        pdfDoc.Add(new Paragraph(" "));

                        // add bill info
                        Paragraph billInfo = new Paragraph();
                        billInfo.Add(new Chunk($"Bill ID: {selectedBill.id}\n", smallFont));
                        billInfo.Add(new Chunk($"Order ID: {selectedBill.orderID}\n", smallFont));
                        billInfo.Add(new Chunk($"Client ID: {selectedBill.clientID}\n", smallFont));
                        billInfo.Add(new Chunk($"Employee ID: {selectedBill.employeeID}\n", smallFont));
                        billInfo.Add(new Chunk($"Bill Date: {selectedBill.billDate.ToString("MM/dd/yyyy hh:mm:ss")}\n", smallFont));
                        pdfDoc.Add(billInfo);

                        pdfDoc.Add(new Paragraph(" "));

                        // add order item header
                        Paragraph orderItemHeader = new Paragraph("---------- Order Items ----------", regularFont);
                        orderItemHeader.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(orderItemHeader);

                        pdfDoc.Add(new Paragraph(" "));

                        // create a table for order items
                        PdfPTable table = new PdfPTable(5);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 10, 20, 20, 20, 30 }); // Set column widths

                        // add table headers
                        table.AddCell(new Phrase("ID", smallFont));
                        table.AddCell(new Phrase("Order ID", smallFont));
                        table.AddCell(new Phrase("Product ID", smallFont));
                        table.AddCell(new Phrase("Quantity", smallFont));
                        table.AddCell(new Phrase("Price", smallFont));

                        // add order item rows
                        foreach (OrderItemDTO item in orderItemList)
                        {
                            ProductDTO product = orderItemBUS.getProductById(item.productID);
                            double price = product.price * item.quantity;

                            table.AddCell(new Phrase(item.id.ToString(), smallFont));
                            table.AddCell(new Phrase(item.orderID.ToString(), smallFont));
                            table.AddCell(new Phrase(item.productID.ToString(), smallFont));
                            table.AddCell(new Phrase(item.quantity.ToString(), smallFont));
                            table.AddCell(new Phrase(price.ToString("C2"), smallFont));
                        }
                        pdfDoc.Add(table);

                        // add total price
                        Paragraph totalPrice = new Paragraph($"Total Price: {selectedBill.totalPrice.ToString("C2")}\n", regularFont);
                        totalPrice.Alignment = Element.ALIGN_RIGHT;
                        pdfDoc.Add(totalPrice);

                        Paragraph footer = new Paragraph("-------------------------------\nThank you for visiting Pi Store", regularFont);
                        footer.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(footer);

                        pdfDoc.Close();

                        MessageBox.Show("Bill exported successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private List<BillDTO> searchBill(string searchText)
        {
            return billList.Where(bil =>
                bil.id.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                bil.orderID.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                bil.clientID.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0 ||
                bil.billDate.ToString("MM/dd/yyyy hh:mm:ss").Contains(searchText) ||
                bil.totalPrice.ToString().Contains(searchText)
            ).ToList();
        }

        private void gridBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                gridBill.Focus();
                gridBill.Rows[e.RowIndex].Selected = true;

                var selectedRow = gridBill.SelectedRows[0];

                billID = selectedRow.Cells[0].Value.ToString();
                orderID = selectedRow.Cells[1].Value.ToString();

                displayOrderItem(orderID);
                btnExportPDF.Enabled = true;
            }
        }
    }
}
