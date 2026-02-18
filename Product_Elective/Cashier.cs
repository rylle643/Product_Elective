using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Product_Elective
{
    public partial class Cashier : Form
    {
        ProductDatabase productdb_connect = new ProductDatabase();
        employee_dbconnection empdb_connect = new employee_dbconnection();

        private int selectedQuantity = 1;
        private string lastPaymentType = "";
        private decimal lastAmountPaid = 0;
        private decimal lastChange = 0;
        private string lastDiscountType = "";
        private decimal lastDiscountRate = 0;

        public Cashier()
        {
            productdb_connect.product_connString();
            empdb_connect.employee_connString();
            InitializeComponent();
        }

        private void Cashier_Load(object sender, EventArgs e)
        {
            time_dateLabel.Text = DateTime.Now.ToString("MMMM dd, yyyy");
            SetupOrderGrid();
            LoadEmployees();
            SearchtextBox.Focus();
        }

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

            OrderGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(219, 112, 147);
            OrderGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            OrderGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            OrderGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 112, 147);

            OrderGridView.DefaultCellStyle.BackColor = Color.FromArgb(255, 240, 245);
            OrderGridView.DefaultCellStyle.ForeColor = Color.FromArgb(100, 40, 70);
            OrderGridView.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            OrderGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(228, 144, 173);
            OrderGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            OrderGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            OrderGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 228, 237);
            OrderGridView.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(100, 40, 70);
            OrderGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(228, 144, 173);
            OrderGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBarcode", HeaderText = "Barcode", Width = 110 });
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Product Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrice", HeaderText = "Price (₱)", Width = 100 });
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQty", HeaderText = "Qty", Width = 60 });
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotal", HeaderText = "Total (₱)", Width = 110 });

            OrderGridView.Columns["colPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            OrderGridView.Columns["colTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        // put the emp id in the combo box and display the name of the employee
        private void LoadEmployees()
        {
            try
            {
                cashier_comboBox.Items.Clear();
                cashier_comboBox.Items.Add("-- Select Cashier --");

                empdb_connect.employee_sql = "SELECT emp_id FROM employeeTbl";
                empdb_connect.employee_cmd();
                empdb_connect.employee_sqladapterSelect();
                empdb_connect.employee_sqldatasetSELECT();

                DataTable dt = empdb_connect.employee_sql_dataset.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No employees found in the database!");
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {
                    cashier_comboBox.Items.Add(row["emp_id"].ToString());
                }

                cashier_comboBox.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }

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

                DataTable dt = empdb_connect.employee_sql_dataset.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    emp_fnameLabel.Text = dt.Rows[0]["emp_fname"].ToString();
                    emp_surnameLabel.Text = dt.Rows[0]["emp_mname"].ToString() + " " + dt.Rows[0]["emp_lname"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }
        //Search barcode of product
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
                productdb_connect.product_cmd();
                productdb_connect.product_sqladapterSelect();
                productdb_connect.product_sqldatasetSELECT();

                if (productdb_connect.product_sql_dataset.Tables["productTbl"].Rows.Count > 0)
                {
                    DataRow product = productdb_connect.product_sql_dataset.Tables["productTbl"].Rows[0];
                    string barcode = product["productid"].ToString();
                    string name = product["product_name"].ToString();
                    decimal price = Convert.ToDecimal(product["price"]);
                    int stockQty = Convert.ToInt32(product["quantity"]);

                    if (stockQty < selectedQuantity)
                    {
                        MessageBox.Show("Not enough stock! Available: " + stockQty);
                        SearchtextBox.Clear();
                        SearchtextBox.Focus();
                        return;
                    }

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
                    selectedQuantity = 1;
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
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }

        
        private void UpdateTotalPrice()
        {
            decimal grandTotal = 0;

            foreach (DataGridViewRow row in OrderGridView.Rows)
            {
                if (row.Cells["colTotal"].Value != null)
                {
                    string totalStr = row.Cells["colTotal"].Value.ToString().Replace("₱", "").Replace(",", "").Trim();
                    if (decimal.TryParse(totalStr, out decimal rowTotal))
                        grandTotal += rowTotal;
                }
            }

            priceTxtbox1.Text = "₱" + grandTotal.ToString("#,##0.00");
        }

       
        private void ClearAll()
        {
            OrderGridView.Rows.Clear();
            priceTxtbox1.Text = "₱0.00";
            textBox1.Text = "₱0.00";
            selectedQuantity = 1;
            lastPaymentType = "";
            lastAmountPaid = 0;
            lastChange = 0;
            lastDiscountType = "";
            lastDiscountRate = 0;
            cashier_comboBox.SelectedIndex = 0;
            emp_fnameLabel.Text = "";
            emp_surnameLabel.Text = "";
            SearchtextBox.Focus();
        }

        
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
                        productdb_connect.product_sql = "UPDATE productTbl SET quantity = quantity - " + qty + " WHERE productid = '" + barcode + "'";
                        productdb_connect.product_cmd();
                        productdb_connect.product_sqladapterUpdate();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }

        // inside the receipt
        private void OpenReceipt(string paymentType, decimal amountPaid, decimal change)
        {
            try
            {
                Product_Elective.Receipt print = new Product_Elective.Receipt();

                print.printDisplayListbox.Items.Clear();
                print.printDisplayListbox.Items.Add("-----------------------------------------------------");
                print.printDisplayListbox.Items.Add("                  SALES RECEIPT                     ");
                print.printDisplayListbox.Items.Add("-----------------------------------------------------");
                print.printDisplayListbox.Items.Add("Date:       " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                print.printDisplayListbox.Items.Add("Cashier ID: " + cashier_comboBox.SelectedItem.ToString());
                print.printDisplayListbox.Items.Add("Cashier:    " + emp_fnameLabel.Text + " " + emp_surnameLabel.Text);
                print.printDisplayListbox.Items.Add("Discount:   " + lastDiscountType + (lastDiscountRate > 0 ? " (" + (lastDiscountRate * 100) + "%)" : ""));
                print.printDisplayListbox.Items.Add("-----------------------------------------------------");
                print.printDisplayListbox.Items.Add(string.Format("{0,-24} {1,5} {2,12}", "Item", "Qty", "Total"));
                print.printDisplayListbox.Items.Add("-----------------------------------------------------");

                foreach (DataGridViewRow row in OrderGridView.Rows)
                {
                    string itemName = row.Cells["colName"].Value?.ToString();
                    string qty = row.Cells["colQty"].Value?.ToString();
                    string total = row.Cells["colTotal"].Value?.ToString();
                    print.printDisplayListbox.Items.Add(string.Format("{0,-24} {1,5} {2,12}", itemName, qty, total));
                }

                print.printDisplayListbox.Items.Add("-----------------------------------------------------");
                print.printDisplayListbox.Items.Add(string.Format("{0,-30} {1,12}", "Total:", priceTxtbox1.Text));
                print.printDisplayListbox.Items.Add(string.Format("{0,-30} {1,12}", "Payment Type:", paymentType));
                print.printDisplayListbox.Items.Add(string.Format("{0,-30} {1,12}", "Amount Paid:", "₱" + amountPaid.ToString("#,##0.00")));
                print.printDisplayListbox.Items.Add(string.Format("{0,-30} {1,12}", "Change:", "₱" + change.ToString("#,##0.00")));
                print.printDisplayListbox.Items.Add("-----------------------------------------------------");
                print.printDisplayListbox.Items.Add("           Thank you! Come again!                   ");
                print.printDisplayListbox.Items.Add("-----------------------------------------------------");

                print.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }

        // Select the quantity
        private void button1_Click(object sender, EventArgs e)
        {
            Product_Elective.Quantity quantityForm = new Product_Elective.Quantity();
            if (quantityForm.ShowDialog() == DialogResult.OK)
            {
                selectedQuantity = quantityForm.QuantityValue;
                MessageBox.Show("Quantity set to: " + selectedQuantity + "\nNow scan the product.");
                SearchtextBox.Focus();
                SearchtextBox.SelectAll();
            }
        }

        // select discount
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (OrderGridView.Rows.Count == 0)
                {
                    MessageBox.Show("No items in the order!");
                    return;
                }

                Discount discountForm = new Discount();
                if (discountForm.ShowDialog() != DialogResult.OK)
                    return;

                lastDiscountType = discountForm.DiscountType;
                lastDiscountRate = discountForm.DiscountRate;

                MessageBox.Show("Type: " + lastDiscountType + " | Rate: " + lastDiscountRate);

                foreach (DataGridViewRow row in OrderGridView.Rows)
                {
                    string priceStr = row.Cells["colPrice"].Value?.ToString().Replace("₱", "").Replace(",", "").Trim();
                    string qtyStr = row.Cells["colQty"].Value?.ToString();

                    if (decimal.TryParse(priceStr, out decimal price) && int.TryParse(qtyStr, out int qty))
                    {
                        decimal original = price * qty;
                        decimal discounted = original - (original * lastDiscountRate);
                        row.Cells["colTotal"].Value = "₱" + discounted.ToString("#,##0.00");
                    }
                }

                UpdateTotalPrice();

                if (lastAmountPaid > 0)
                {
                    string totalStr = priceTxtbox1.Text.Replace("₱", "").Replace(",", "").Trim();
                    decimal newTotal = Convert.ToDecimal(totalStr);
                    lastChange = lastAmountPaid - newTotal;
                    textBox1.Text = "₱" + lastChange.ToString("#,##0.00");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }

        // payment type
        private void Paymentbutton_Click_1(object sender, EventArgs e)
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

                if (string.IsNullOrEmpty(lastDiscountType))
                {
                    MessageBox.Show("Please select a discount type first!");
                    return;
                }

                string totalStr = priceTxtbox1.Text.Replace("₱", "").Replace(",", "").Trim();
                decimal grandTotal = Convert.ToDecimal(totalStr);

                Payment paymentForm = new Payment(grandTotal);
                if (paymentForm.ShowDialog() == DialogResult.OK)
                {
                    lastPaymentType = paymentForm.PaymentType;
                    lastAmountPaid = paymentForm.AmountPaid;
                    lastChange = paymentForm.Change;

                    DeductStockFromDatabase();

                    textBox1.Text = "₱" + lastChange.ToString("#,##0.00");

                    MessageBox.Show("Payment confirmed! Click Print to print the receipt.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }

        //print receipt
        private void Printbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (OrderGridView.Rows.Count == 0)
                {
                    MessageBox.Show("No items to print!");
                    return;
                }

                OpenReceipt(lastPaymentType, lastAmountPaid, lastChange);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }

        // save transaction to database
        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (OrderGridView.Rows.Count == 0)
                {
                    MessageBox.Show("No items to save!");
                    return;
                }

                if (string.IsNullOrEmpty(lastPaymentType))
                {
                    MessageBox.Show("Please process payment first!");
                    return;
                }

                string dateToSave = DateTime.Now.ToString("M/d/yyyy");
                string timeToSave = DateTime.Now.ToString("hh:mm tt");

                foreach (DataGridViewRow row in OrderGridView.Rows)
                {
                    string productId = row.Cells["colBarcode"].Value?.ToString();
                    string productName = row.Cells["colName"].Value?.ToString();
                    string total = row.Cells["colTotal"].Value?.ToString().Replace("₱", "").Replace(",", "").Trim();

                    productdb_connect.product_sql = "INSERT INTO salesTbl (product_id, product_name, total, discount_type, payment_type, amount_paid, change_amount, cashier_id, time_date) VALUES ('" + productId + "', '" + productName + "', '" + total + "', '" + lastDiscountType + "', '" + lastPaymentType + "', '" + lastAmountPaid + "', '" + lastChange + "', '" + cashier_comboBox.SelectedItem.ToString() + "', '" + dateToSave + " " + timeToSave + "')";
                    productdb_connect.product_cmd();
                    productdb_connect.product_sqladapterInsert();
                }

                MessageBox.Show("Sale saved successfully!");
                ClearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Actual error: " + ex.Message);
            }
        }

        // select loyalty points
        private void button7_Click(object sender, EventArgs e)
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

                LoyaltyForm loyaltyForm = new LoyaltyForm();
                if (loyaltyForm.ShowDialog() != DialogResult.OK)
                    return;

                string customerName = loyaltyForm.CustomerName;
                int points = OrderGridView.Rows.Count;
                string dateToSave = DateTime.Now.ToString("M/d/yyyy");
                string timeToSave = DateTime.Now.ToString("hh:mm tt");

                productdb_connect.product_sql = "INSERT INTO loyaltyTbl (customer_name, points, cashier_id, time_date) VALUES ('" + customerName + "', '" + points + "', '" + cashier_comboBox.SelectedItem.ToString() + "', '" + dateToSave + " " + timeToSave + "')";
                productdb_connect.product_cmd();
                productdb_connect.product_sqladapterInsert();

                MessageBox.Show("Added " + points + " point(s) for " + customerName + "!");
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }

        // ─── OTHER BUTTONS ────────────────────────────────────────────────────────
        private void button5_Click(object sender, EventArgs e) { ClearAll(); }
        private void button3_Click(object sender, EventArgs e) { ClearAll(); }

        // Remove selected item from order
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

        // Open cash drawer
        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cash drawer is now open!", "Drawer Open", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Hold transaction
        private void button4_Click(object sender, EventArgs e)
        {
            if (OrderGridView.Rows.Count == 0)
            {
                MessageBox.Show("No active order to hold.", "Hold Transaction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Transaction has been put on hold.", "Transaction Held", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Cancel transaction
        private void button10_Click(object sender, EventArgs e) { this.Close(); }
        private void OrderGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}