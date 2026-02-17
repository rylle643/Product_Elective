using ACOTIN_POS_APPLICATION;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_Elective
{
    public partial class Cashier : Form
    {
        ProductDatabase productdb_connect = new ProductDatabase();
        employee_dbconnection empdb_connect = new employee_dbconnection();

        private int selectedQuantity = 1; // default quantity

        public Cashier()
        {
            productdb_connect.product_connString();
            empdb_connect.employee_connString();
            InitializeComponent();
        }

        // ─── LOAD ─────────────────────────────────────────────────────────────────
        private void Cashier_Load(object sender, EventArgs e)
        {
            // Show current date
            time_dateLabel.Text = DateTime.Now.ToString("MMMM dd, yyyy");

            // Setup OrderGridView columns
            SetupOrderGrid();

            // Load employees into combobox
            LoadEmployees();

            // Focus search box on load
            SearchtextBox.Focus();
        }

        // ─── GRIDVIEW SETUP ───────────────────────────────────────────────────────
        private void SetupOrderGrid()
        {
            OrderGridView.Columns.Clear();
            OrderGridView.Rows.Clear();
            OrderGridView.AutoGenerateColumns = false;
            OrderGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            OrderGridView.MultiSelect = false;
            OrderGridView.AllowUserToAddRows = false;
            OrderGridView.ReadOnly = true;
            OrderGridView.RowHeadersVisible = false;
            OrderGridView.EnableHeadersVisualStyles = false;
            OrderGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            OrderGridView.ColumnHeadersHeight = 38;
            OrderGridView.RowTemplate.Height = 34;
            OrderGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            OrderGridView.BackgroundColor = Color.FromArgb(255, 240, 245);
            OrderGridView.GridColor = Color.FromArgb(244, 210, 224);
            OrderGridView.BorderStyle = BorderStyle.None;

            // Header style — PaleVioletRed
            OrderGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(219, 112, 147);
            OrderGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            OrderGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            OrderGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 112, 147);

            // Row style — LavenderBlush
            OrderGridView.DefaultCellStyle.BackColor = Color.FromArgb(255, 240, 245);
            OrderGridView.DefaultCellStyle.ForeColor = Color.FromArgb(100, 40, 70);
            OrderGridView.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            OrderGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(228, 144, 173);
            OrderGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            OrderGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Alternating rows
            OrderGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 228, 237);
            OrderGridView.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(100, 40, 70);
            OrderGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(228, 144, 173);
            OrderGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            // Columns
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBarcode", HeaderText = "Barcode", Width = 110 });
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Product Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrice", HeaderText = "Price (₱)", Width = 100 });
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQty", HeaderText = "Qty", Width = 60 });
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotal", HeaderText = "Total (₱)", Width = 110 });

            // Right-align price columns
            OrderGridView.Columns["colPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            OrderGridView.Columns["colTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        // ─── LOAD EMPLOYEES INTO COMBOBOX ─────────────────────────────────────────
        private void LoadEmployees()
        {
            try
            {
                empdb_connect.employee_sql = "SELECT emp_id, emp_fname, emp_mname, emp_lname FROM employeeTbl";
                empdb_connect.employee_cmd();
                empdb_connect.employee_sqladapterSelect();
                empdb_connect.employee_sqldatasetSELECT();

                cashier_comboBox.Items.Clear();
                cashier_comboBox.Items.Add("-- Select Cashier --");

                foreach (DataRow row in empdb_connect.employee_sql_dataset.Tables[0].Rows)
                {
                    // Show only emp_id in combobox — name shown in labels after selection
                    cashier_comboBox.Items.Add(row["emp_id"].ToString());
                }

                cashier_comboBox.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading employees. Please contact your administrator!");
            }
        }

        // ─── EMPLOYEE COMBOBOX — show fname and surname labels ────────────────────
        private void cashier_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cashier_comboBox.SelectedIndex <= 0)
                {
                    emp_fnameLabel.Text = "";
                    emp_surnameLabel.Text = "";
                    return;
                }

                string empId = cashier_comboBox.SelectedItem.ToString();

                empdb_connect.employee_sql = "SELECT emp_fname, emp_mname, emp_lname FROM employeeTbl WHERE emp_id = '" + empId + "'";
                empdb_connect.employee_cmd();
                empdb_connect.employee_sqladapterSelect();
                empdb_connect.employee_sqldatasetSELECT();

                if (empdb_connect.employee_sql_dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow emp = empdb_connect.employee_sql_dataset.Tables[0].Rows[0];
                    emp_fnameLabel.Text = emp["emp_fname"].ToString();
                    emp_surnameLabel.Text = emp["emp_mname"].ToString() + " " + emp["emp_lname"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading cashier info!");
            }
        }

        // ─── SEARCH BY BARCODE ────────────────────────────────────────────────────
        private void product_select()
        {
            productdb_connect.product_cmd();
            productdb_connect.product_sqladapterSelect();
            productdb_connect.product_sqldatasetSELECT();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchtextBox.Text))
                {
                    MessageBox.Show("Please enter a barcode to search!");
                    SearchtextBox.Focus();
                    return;
                }

                productdb_connect.product_sql = "SELECT * FROM productTbl WHERE productid = '" + SearchtextBox.Text.Trim() + "'";
                product_select();

                if (productdb_connect.product_sql_dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow product = productdb_connect.product_sql_dataset.Tables[0].Rows[0];
                    string barcode = product["productid"].ToString();
                    string name = product["product_name"].ToString();
                    decimal price = Convert.ToDecimal(product["price"]);
                    int stockQty = Convert.ToInt32(product["quantity"]);

                    // Check if enough stock
                    if (stockQty < selectedQuantity)
                    {
                        MessageBox.Show("Not enough stock! Available: " + stockQty);
                        SearchtextBox.Clear();
                        SearchtextBox.Focus();
                        return;
                    }

                    // Check if product already in grid — update quantity instead of adding duplicate
                    bool found = false;
                    foreach (DataGridViewRow row in OrderGridView.Rows)
                    {
                        if (row.Cells["colBarcode"].Value?.ToString() == barcode)
                        {
                            int newQty = Convert.ToInt32(row.Cells["colQty"].Value) + selectedQuantity;

                            if (newQty > stockQty)
                            {
                                MessageBox.Show("Not enough stock! Available: " + stockQty);
                                found = true;
                                break;
                            }

                            row.Cells["colQty"].Value = newQty;
                            row.Cells["colTotal"].Value = "₱" + (price * newQty).ToString("#,##0.00");
                            found = true;
                            break;
                        }
                    }

                    // Add new row if not already in grid
                    if (!found)
                    {
                        OrderGridView.Rows.Add(
                            barcode,
                            name,
                            "₱" + price.ToString("#,##0.00"),
                            selectedQuantity,
                            "₱" + (price * selectedQuantity).ToString("#,##0.00")
                        );
                    }

                    UpdateTotalPrice();
                    selectedQuantity = 1;       // reset after each scan
                    SearchtextBox.Clear();
                    SearchtextBox.Focus();
                }
                else
                {
                    MessageBox.Show("Product not found!");
                    SearchtextBox.Clear();
                    SearchtextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching product: " + ex.Message);
            }
        }

        // ─── QUANTITY BUTTON ──────────────────────────────────────────────────────
        private void button1_Click(object sender, EventArgs e)
        {
            Product_Elective.Quantity quantityForm = new Product_Elective.Quantity();
            if (quantityForm.ShowDialog() == DialogResult.OK)
            {
                selectedQuantity = quantityForm.QuantityValue;
                MessageBox.Show("Quantity set to: " + selectedQuantity + "\nNow scan the product.");
                SearchtextBox.Focus();
            }
        }

        // ─── UPDATE TOTAL PRICE TEXTBOX ───────────────────────────────────────────
        private void UpdateTotalPrice()
        {
            decimal grandTotal = 0;

            foreach (DataGridViewRow row in OrderGridView.Rows)
            {
                if (row.Cells["colTotal"].Value != null)
                {
                    string totalStr = row.Cells["colTotal"].Value.ToString()
                                         .Replace("₱", "").Replace(",", "").Trim();
                    if (decimal.TryParse(totalStr, out decimal rowTotal))
                        grandTotal += rowTotal;
                }
            }

            priceTxtbox1.Text = "₱" + grandTotal.ToString("#,##0.00");
        }

        // ─── CLEAR ALL ────────────────────────────────────────────────────────────
        private void button5_Click(object sender, EventArgs e)
        {
            OrderGridView.Rows.Clear();
            priceTxtbox1.Text = "₱0.00";
            selectedQuantity = 1;
            SearchtextBox.Focus();
        }

        // ─── DELETE SELECTED ROW ──────────────────────────────────────────────────
        private void button6_Click(object sender, EventArgs e)
        {
            if (OrderGridView.SelectedRows.Count > 0)
            {
                OrderGridView.Rows.RemoveAt(OrderGridView.SelectedRows[0].Index);
                UpdateTotalPrice();
            }
            else
            {
                MessageBox.Show("Please select a row to remove.");
            }
        }

        // ─── PAYMENT BUTTON ───────────────────────────────────────────────────────
        private void Paymentbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (OrderGridView.Rows.Count == 0)
                {
                    MessageBox.Show("No items in the order!");
                    return;
                }

                if (cashier_comboBox.SelectedIndex <= 0)
                {
                    MessageBox.Show("Please select a cashier first!");
                    return;
                }

                string totalStr = priceTxtbox1.Text.Replace("₱", "").Replace(",", "").Trim();
                decimal grandTotal = Convert.ToDecimal(totalStr);

                Payment paymentForm = new Payment(grandTotal);
                if (paymentForm.ShowDialog() == DialogResult.OK)
                {
                    DeductStockFromDatabase();
                    OpenReceipt(paymentForm.PaymentType, paymentForm.AmountPaid, paymentForm.Change);

                    // Clear after sale
                    OrderGridView.Rows.Clear();
                    priceTxtbox1.Text = "₱0.00";
                    selectedQuantity = 1;
                    SearchtextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Payment error: " + ex.Message);
            }
        }

        // ─── DEDUCT STOCK FROM DATABASE ───────────────────────────────────────────
        private void DeductStockFromDatabase()
        {
            try
            {
                foreach (DataGridViewRow row in OrderGridView.Rows)
                {
                    string barcode = row.Cells["colBarcode"].Value?.ToString();
                    int qty = Convert.ToInt32(row.Cells["colQty"].Value);

                    if (!string.IsNullOrEmpty(barcode))
                    {
                        productdb_connect.product_sql = "UPDATE productTbl SET quantity = quantity - " + qty +
                                                        " WHERE productid = '" + barcode + "'";
                        productdb_connect.product_cmd();
                        productdb_connect.product_sqladapterUpdate();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating stock: " + ex.Message);
            }
        }

        // ─── OPEN RECEIPT ─────────────────────────────────────────────────────────
        private void OpenReceipt(string paymentType, decimal amountPaid, decimal change)
        {
            try
            {
                Product_Elective.Receipt print = new Product_Elective.Receipt();

                print.printDisplayListbox.Items.Clear();
                print.printDisplayListbox.Items.Add("===========================");
                print.printDisplayListbox.Items.Add("        ACOTIN POS         ");
                print.printDisplayListbox.Items.Add("===========================");
                print.printDisplayListbox.Items.Add("Date: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                print.printDisplayListbox.Items.Add("Cashier ID: " + cashier_comboBox.SelectedItem.ToString());
                print.printDisplayListbox.Items.Add("Cashier: " + emp_fnameLabel.Text + " " + emp_surnameLabel.Text);
                print.printDisplayListbox.Items.Add("---------------------------");
                print.printDisplayListbox.Items.Add(string.Format("{0,-18} {1,5} {2,8}", "Item", "Qty", "Total"));
                print.printDisplayListbox.Items.Add("---------------------------");

                foreach (DataGridViewRow row in OrderGridView.Rows)
                {
                    string itemName = row.Cells["colName"].Value?.ToString();
                    string qty = row.Cells["colQty"].Value?.ToString();
                    string total = row.Cells["colTotal"].Value?.ToString();
                    print.printDisplayListbox.Items.Add(
                        string.Format("{0,-18} {1,5} {2,8}", itemName, qty, total)
                    );
                }

                print.printDisplayListbox.Items.Add("---------------------------");
                print.printDisplayListbox.Items.Add("Total:       " + priceTxtbox1.Text);
                print.printDisplayListbox.Items.Add("Payment:     " + paymentType);
                print.printDisplayListbox.Items.Add("Amount Paid: ₱" + amountPaid.ToString("#,##0.00"));
                print.printDisplayListbox.Items.Add("Change:      ₱" + change.ToString("#,##0.00"));
                print.printDisplayListbox.Items.Add("===========================");
                print.printDisplayListbox.Items.Add("   Thank you! Come again!  ");
                print.printDisplayListbox.Items.Add("===========================");

                print.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening receipt: " + ex.Message);
            }
        }

        // ─── PRINT BUTTON ─────────────────────────────────────────────────────────
        private void Printbutton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenReceipt("Reprint", 0, 0);
            }
            catch (Exception)
            {
                MessageBox.Show("Error opening print form.");
            }
        }

        // ─── GRIDVIEW CLICK (required by Designer) ────────────────────────────────
        private void OrderGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // ─── EMPTY HANDLERS ───────────────────────────────────────────────────────
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void priceTxtbox1_TextChanged(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
    }
}