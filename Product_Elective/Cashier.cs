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
        private string pendingLoyaltyCustomer = "";

   
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

            SearchtextBox.TextChanged += SearchtextBox_TextChanged;

            SearchtextBox.Text = "READY TO SCAN...";
            SearchtextBox.ForeColor = Color.Gray;
            SearchtextBox.GotFocus += SearchtextBox_GotFocus;
            SearchtextBox.LostFocus += SearchtextBox_LostFocus;

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
            OrderGridView.ColumnHeadersHeight = 42;
            OrderGridView.RowTemplate.Height = 38;
            OrderGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            OrderGridView.BackgroundColor = Color.White;
            OrderGridView.GridColor = Color.FromArgb(200, 180, 190);
            OrderGridView.BorderStyle = BorderStyle.None;

            OrderGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(160, 50, 90);
            OrderGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            OrderGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold);
            OrderGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            OrderGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(160, 50, 90);

            OrderGridView.DefaultCellStyle.BackColor = Color.White;
            OrderGridView.DefaultCellStyle.ForeColor = Color.FromArgb(30, 10, 20);
            OrderGridView.DefaultCellStyle.Font = new Font("Segoe UI", 14F);
            OrderGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 70, 110);
            OrderGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            OrderGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            OrderGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 240, 242);
            OrderGridView.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(30, 10, 20);
            OrderGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 70, 110);
            OrderGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
            OrderGridView.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colBarcode", HeaderText = "Barcode", Width = 200 });
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Product Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrice", HeaderText = "Price (₱)", Width = 150 });
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQty", HeaderText = "Qty", Width = 90 });
            OrderGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotal", HeaderText = "Total (₱)", Width = 150 });

            OrderGridView.Columns["colPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            OrderGridView.Columns["colTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void SearchtextBox_GotFocus(object sender, EventArgs e)
        {
            if (SearchtextBox.ForeColor == Color.Gray)
            {
                SearchtextBox.TextChanged -= SearchtextBox_TextChanged;
                SearchtextBox.Text = "";
                SearchtextBox.ForeColor = Color.FromArgb(30, 10, 20);
                SearchtextBox.TextChanged += SearchtextBox_TextChanged;
            }
        }

        private void SearchtextBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchtextBox.Text))
            {
                SearchtextBox.TextChanged -= SearchtextBox_TextChanged;
                SearchtextBox.Text = "READY TO SCAN...";
                SearchtextBox.ForeColor = Color.Gray;
                SearchtextBox.TextChanged += SearchtextBox_TextChanged;
            }
        }

        private void ResetSearchBox()
        {
            SearchtextBox.TextChanged -= SearchtextBox_TextChanged;
            SearchtextBox.Text = "";
            SearchtextBox.ForeColor = Color.FromArgb(30, 10, 20);
            SearchtextBox.TextChanged += SearchtextBox_TextChanged;

            this.BeginInvoke(new Action(() =>
            {
                SearchtextBox.Focus();
            }));
        }


        private void SearchtextBox_TextChanged(object sender, EventArgs e)
        {
            string input = SearchtextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(input) || SearchtextBox.ForeColor == Color.Gray)
                return;

            try
            {
                productdb_connect.product_sql = "SELECT * FROM productTbl WHERE productid = '" + input + "'";
                productdb_connect.product_cmd();
                productdb_connect.product_sqladapterSelect();
                productdb_connect.product_sqldatasetSELECT();

                var table = productdb_connect.product_sql_dataset.Tables["productTbl"];

                if (table == null || table.Rows.Count == 0)
                    return;

                DataRow product = table.Rows[0];
                string barcode = product["productid"].ToString();
                string name = product["product_name"].ToString();
                decimal price = Convert.ToDecimal(product["price"]);
                int stockQty = Convert.ToInt32(product["quantity"]);

                if (stockQty < selectedQuantity)
                    return;

                bool alreadyInGrid = false;
                foreach (DataGridViewRow row in OrderGridView.Rows)
                {
                    if (row.Cells["colBarcode"].Value?.ToString() == barcode)
                    {
                        int newQty = Convert.ToInt32(row.Cells["colQty"].Value) + selectedQuantity;

                        if (newQty > stockQty)
                        {
                            alreadyInGrid = true;
                            break;
                        }

                        row.Cells["colQty"].Value = newQty;
                        row.Cells["colTotal"].Value = "₱" + (price * newQty).ToString("#,##0.00");
                        alreadyInGrid = true;
                        break;
                    }
                }

                if (!alreadyInGrid)
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
                ResetSearchBox();
            }
            catch
            {
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                string input = SearchtextBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(input) || SearchtextBox.ForeColor == Color.Gray)
                {
                    MessageBox.Show("Please enter a barcode to search!");
                    SearchtextBox.Focus();
                    return;
                }

                productdb_connect.product_sql = "SELECT * FROM productTbl WHERE productid = '" + input + "'";
                productdb_connect.product_cmd();
                productdb_connect.product_sqladapterSelect();
                productdb_connect.product_sqldatasetSELECT();

                var table = productdb_connect.product_sql_dataset.Tables["productTbl"];

                if (table == null || table.Rows.Count == 0)
                {
                    MessageBox.Show("Product not found! Please check the barcode and try again.", "Not Found");
                    ResetSearchBox();
                    return;
                }

                DataRow product = table.Rows[0];
                string barcode = product["productid"].ToString();
                string name = product["product_name"].ToString();
                decimal price = Convert.ToDecimal(product["price"]);
                int stockQty = Convert.ToInt32(product["quantity"]);

                if (stockQty == 0)
                {
                    MessageBox.Show($"'{name}' is out of stock!", "Out of Stock");
                    ResetSearchBox();
                    return;
                }

                if (stockQty < selectedQuantity)
                {
                    MessageBox.Show($"Not enough stock for '{name}'!\nAvailable: {stockQty}", "Insufficient Stock");
                    ResetSearchBox();
                    return;
                }

                bool alreadyInGrid = false;
                foreach (DataGridViewRow row in OrderGridView.Rows)
                {
                    if (row.Cells["colBarcode"].Value?.ToString() == barcode)
                    {
                        int newQty = Convert.ToInt32(row.Cells["colQty"].Value) + selectedQuantity;

                        if (newQty > stockQty)
                        {
                            MessageBox.Show($"Cannot add more '{name}'.\nMax available: {stockQty}", "Insufficient Stock");
                            alreadyInGrid = true;
                            break;
                        }

                        row.Cells["colQty"].Value = newQty;
                        row.Cells["colTotal"].Value = "₱" + (price * newQty).ToString("#,##0.00");
                        alreadyInGrid = true;
                        break;
                    }
                }

                if (!alreadyInGrid)
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
                ResetSearchBox();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }


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
                    cashier_comboBox.Items.Add(row["emp_id"].ToString());

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
            ResetSearchBox();
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


        private void OpenReceipt(string paymentType, decimal amountPaid, decimal change)
        {
            try
            {
                Product_Elective.Receipt print = new Product_Elective.Receipt();
                print.printDisplayListbox.Items.Clear();

                decimal originalTotal = 0;
                decimal discountedTotal = 0;

                foreach (DataGridViewRow row in OrderGridView.Rows)
                {
                    string priceStr = row.Cells["colPrice"].Value?.ToString().Replace("₱", "").Replace(",", "").Trim();
                    string qtyStr = row.Cells["colQty"].Value?.ToString();
                    string totalStr = row.Cells["colTotal"].Value?.ToString().Replace("₱", "").Replace(",", "").Trim();

                    if (decimal.TryParse(priceStr, out decimal p) && int.TryParse(qtyStr, out int q))
                        originalTotal += p * q;

                    if (decimal.TryParse(totalStr, out decimal t))
                        discountedTotal += t;
                }

                decimal discountAmount = originalTotal - discountedTotal;
                string discountLabel = lastDiscountType + (lastDiscountRate > 0 ? " (" + (lastDiscountRate * 100) + "%)" : "");

                int W = 38;

                print.printDisplayListbox.Items.Add("  ╔══════════════════════════════════════╗");
                print.printDisplayListbox.Items.Add("  ║" + Center("PRODUCT ELECTIVE", W) + "║");
                print.printDisplayListbox.Items.Add("  ║" + Center("SALES RECEIPT", W) + "║");
                print.printDisplayListbox.Items.Add("  ╚══════════════════════════════════════╝");
                print.printDisplayListbox.Items.Add("");

                print.printDisplayListbox.Items.Add("  Date    : " + DateTime.Now.ToString("MMMM dd, yyyy"));
                print.printDisplayListbox.Items.Add("  Time    : " + DateTime.Now.ToString("hh:mm tt"));
                print.printDisplayListbox.Items.Add("  Cashier : " + emp_fnameLabel.Text + " " + emp_surnameLabel.Text);
                print.printDisplayListbox.Items.Add("  ID      : " + cashier_comboBox.SelectedItem.ToString());
                print.printDisplayListbox.Items.Add("");

                print.printDisplayListbox.Items.Add("  " + new string('-', W + 2));
                print.printDisplayListbox.Items.Add("  " + PadRow("ITEM", "QTY", "TOTAL", W));
                print.printDisplayListbox.Items.Add("  " + new string('-', W + 2));

                foreach (DataGridViewRow row in OrderGridView.Rows)
                {
                    string itemName = row.Cells["colName"].Value?.ToString() ?? "";
                    string qty = "x" + (row.Cells["colQty"].Value?.ToString() ?? "");
                    string total = row.Cells["colTotal"].Value?.ToString() ?? "";

                    if (itemName.Length > 18)
                        itemName = itemName.Substring(0, 15) + "...";

                    print.printDisplayListbox.Items.Add("  " + PadRow(itemName, qty, total, W));
                }

                print.printDisplayListbox.Items.Add("  " + new string('-', W + 2));
                print.printDisplayListbox.Items.Add("");

                print.printDisplayListbox.Items.Add("  " + PadTwo("Subtotal:", "₱" + originalTotal.ToString("#,##0.00"), W));
                print.printDisplayListbox.Items.Add("  " + PadTwo("Discount (" + (lastDiscountRate * 100) + "%):", "-₱" + discountAmount.ToString("#,##0.00"), W));
                print.printDisplayListbox.Items.Add("  " + PadTwo("Discount Type:", discountLabel, W));
                print.printDisplayListbox.Items.Add("  " + new string('-', W + 2));
                print.printDisplayListbox.Items.Add("  " + PadTwo("TOTAL DUE:", "₱" + discountedTotal.ToString("#,##0.00"), W));
                print.printDisplayListbox.Items.Add("  " + new string('-', W + 2));
                print.printDisplayListbox.Items.Add("  " + PadTwo("Payment Type:", paymentType, W));
                print.printDisplayListbox.Items.Add("  " + PadTwo("Amount Paid:", "₱" + amountPaid.ToString("#,##0.00"), W));
                print.printDisplayListbox.Items.Add("  " + PadTwo("Change:", "₱" + change.ToString("#,##0.00"), W));
                print.printDisplayListbox.Items.Add("  " + new string('-', W + 2));

                print.printDisplayListbox.Items.Add("");
                print.printDisplayListbox.Items.Add("       ★  Thank you for shopping!  ★");
                print.printDisplayListbox.Items.Add("          Please come again soon.");
                print.printDisplayListbox.Items.Add("");

                print.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }

        private string Center(string text, int width)
        {
            if (text.Length >= width) return text;
            int totalPadding = width - text.Length;
            int leftPadding = totalPadding / 2;
            int rightPadding = totalPadding - leftPadding;
            return new string(' ', leftPadding) + text + new string(' ', rightPadding);
        }

        private string PadTwo(string label, string value, int width)
        {
            int spaces = width - label.Length - value.Length;
            if (spaces < 1) spaces = 1;
            return label + new string(' ', spaces) + value;
        }

        private string PadRow(string name, string qty, string total, int width)
        {
            string namePart = name.PadRight(18);
            string qtyPart = qty.PadLeft(5);
            string totalPart = total.PadLeft(width - 18 - 5);
            return namePart + qtyPart + totalPart;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Product_Elective.Quantity quantityForm = new Product_Elective.Quantity();
            if (quantityForm.ShowDialog() == DialogResult.OK)
            {
                selectedQuantity = quantityForm.QuantityValue;
                SearchtextBox.Focus();
                SearchtextBox.SelectAll();
            }
        }

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

                    textBox1.Text = "₱" + lastChange.ToString("#,##0.00");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }

        private void Printbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (OrderGridView.Rows.Count == 0)
                {
                    MessageBox.Show("No items to print!");
                    return;
                }

                if (string.IsNullOrEmpty(lastPaymentType))
                {
                    MessageBox.Show("Please process payment first before printing!");
                    return;
                }

                OpenReceipt(lastPaymentType, lastAmountPaid, lastChange);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator for this matter!!!");
            }
        }

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

                if (!string.IsNullOrWhiteSpace(pendingLoyaltyCustomer))
                {
                    int points = OrderGridView.Rows.Count; 
                    string cashierId = cashier_comboBox.SelectedItem.ToString();

              
                    productdb_connect.product_sql = "SELECT COUNT(*) FROM loyaltyTbl WHERE customer_name = '" + pendingLoyaltyCustomer + "'";
                    productdb_connect.product_cmd();
                    int exists = Convert.ToInt32(productdb_connect.product_sql_command.ExecuteScalar());

                    if (exists > 0)
                    {
                        productdb_connect.product_sql = "UPDATE loyaltyTbl SET points = points + " + points +
                                                        ", cashier_id = '" + cashierId + "'" +
                                                        ", time_date = '" + dateToSave + " " + timeToSave + "'" +
                                                        " WHERE customer_name = '" + pendingLoyaltyCustomer + "'";
                        productdb_connect.product_cmd();
                        productdb_connect.product_sqladapterUpdate();
                    }
                    else
                    {
                        productdb_connect.product_sql = "INSERT INTO loyaltyTbl (customer_name, points, cashier_id, time_date) " +
                                                        "VALUES ('" + pendingLoyaltyCustomer + "', " + points + ", '" + cashierId + "', '" + dateToSave + " " + timeToSave + "')";
                        productdb_connect.product_cmd();
                        productdb_connect.product_sqladapterInsert();
                    }

                    pendingLoyaltyCustomer = ""; 
                }


                DeductStockFromDatabase();

                MessageBox.Show("Sale saved successfully!");
                ClearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Actual error: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
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

            string customerName = loyaltyForm.CustomerName.Trim();

            productdb_connect.product_sql = "SELECT COUNT(*) FROM loyaltyTbl WHERE customer_name = '" + customerName + "'";
            productdb_connect.product_cmd();
            int exists = Convert.ToInt32(productdb_connect.product_sql_command.ExecuteScalar());

            if (exists > 0)
                MessageBox.Show("Welcome back, " + customerName + "! +1 point will be added.", "Existing Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(customerName + " is a new customer! +1 point will be added.", "New Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);

            pendingLoyaltyCustomer = customerName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Refund refundForm = new Refund();
            refundForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e) { ClearAll(); }
        private void button3_Click(object sender, EventArgs e) { ClearAll(); }

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

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Cash drawer is now open!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (OrderGridView.Rows.Count == 0)
            {
                MessageBox.Show("No active order to hold.");
                return;
            }
            MessageBox.Show("Transaction has been put on hold.");
        }

        private void button10_Click(object sender, EventArgs e) { this.Close(); }

        private void OrderGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void emp_surnameLabel_Click(object sender, EventArgs e)
        {

        }

        private void emp_fnameLabel_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}