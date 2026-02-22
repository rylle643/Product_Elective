using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Product_Elective
{
    public partial class Refund : Form
    {
        ProductDatabase productdb_connect = new ProductDatabase();

        public Refund()
        {
            productdb_connect.product_connString();
            InitializeComponent();
        }

        private void Refund_Load(object sender, EventArgs e)
        {
            SetupGrid();
            ClearSummaryLabels();
            salesIdTextBox.Focus();
        }

        // ─── SETUP GRID ───────────────────────────────────────────────────────────
        private void SetupGrid()
        {
            refundGrid.Columns.Clear();
            refundGrid.AutoGenerateColumns = false;
            refundGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            refundGrid.MultiSelect = false;
            refundGrid.AllowUserToAddRows = false;
            refundGrid.ReadOnly = true;
            refundGrid.RowHeadersVisible = false;
            refundGrid.EnableHeadersVisualStyles = false;
            refundGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            refundGrid.ColumnHeadersHeight = 42;
            refundGrid.RowTemplate.Height = 38;
            refundGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            refundGrid.BackgroundColor = Color.White;
            refundGrid.GridColor = Color.FromArgb(200, 180, 190);
            refundGrid.BorderStyle = BorderStyle.None;

            // Header style
            refundGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(160, 50, 90);
            refundGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            refundGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            refundGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            refundGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(160, 50, 90);

            // Cell style
            refundGrid.DefaultCellStyle.BackColor = Color.White;
            refundGrid.DefaultCellStyle.ForeColor = Color.FromArgb(30, 10, 20);
            refundGrid.DefaultCellStyle.Font = new Font("Segoe UI", 10.5F);
            refundGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 70, 110);
            refundGrid.DefaultCellStyle.SelectionForeColor = Color.White;
            refundGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Alternating rows
            refundGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 240, 242);
            refundGrid.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(30, 10, 20);
            refundGrid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 70, 110);
            refundGrid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            // Columns
            refundGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSaleId", HeaderText = "Sale ID", Width = 80 });
            refundGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "colProductId", HeaderText = "Barcode", Width = 130 });
            refundGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Product Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            refundGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotal", HeaderText = "Total (P)", Width = 120 });
            refundGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDiscount", HeaderText = "Discount", Width = 120 });
            refundGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPayment", HeaderText = "Payment", Width = 100 });
            refundGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDate", HeaderText = "Date & Time", Width = 150 });

            refundGrid.Columns["colTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        // ─── SEARCH BUTTON ────────────────────────────────────────────────────────
        private void button1_Click(object sender, EventArgs e)
        {
            string input = salesIdTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Please enter a Sales ID to search.", "No Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                salesIdTextBox.Focus();
                return;
            }

            try
            {
                productdb_connect.product_sql = "SELECT * FROM salesTbl WHERE sale_id = '" + input + "'";
                productdb_connect.product_cmd();
                productdb_connect.product_sqladapterSelect();
                productdb_connect.product_sqldatasetSELECT();

                DataTable dt = productdb_connect.product_sql_dataset.Tables[0];

                refundGrid.Rows.Clear();
                statusLabel.Text = "";
                ClearSummaryLabels();

                if (dt.Rows.Count == 0)
                {
                    statusLabel.Text = "No sale found with that ID.";
                    statusLabel.ForeColor = Color.FromArgb(160, 50, 50);
                    confirmButton.Enabled = false;
                    return;
                }

                // Fill the grid and collect summary info at the same time
                decimal grandTotal = 0;
                string cashierId = "";
                string paymentType = "";
                string discountType = "";

                foreach (DataRow row in dt.Rows)
                {
                    decimal rowTotal = Convert.ToDecimal(row["total"]);
                    grandTotal += rowTotal;

                    // Only grab summary info once from the first row
                    if (cashierId == "")
                    {
                        cashierId = row["cashier_id"].ToString();
                        paymentType = row["payment_type"].ToString();
                        discountType = row["discount_type"].ToString();
                    }

                    refundGrid.Rows.Add(
                        row["sale_id"].ToString(),
                        row["product_id"].ToString(),
                        row["product_name"].ToString(),
                        "P" + rowTotal.ToString("#,##0.00"),
                        row["discount_type"].ToString(),
                        row["payment_type"].ToString(),
                        row["time_date"].ToString()
                    );
                }

                // Populate the summary labels so cashier can verify before confirming
                CashierLabel.Text = cashierId;
                PaymentTypeLabel.Text = paymentType;
                DiscountLabel.Text = discountType;
                TotalRefundLabel.Text = "P" + grandTotal.ToString("#,##0.00");

                // Check if already refunded
                if (discountType.StartsWith("REFUNDED"))
                {
                    statusLabel.Text = "This sale has already been refunded!";
                    statusLabel.ForeColor = Color.FromArgb(160, 50, 50);
                    confirmButton.Enabled = false; // ← block the confirm button
                    return;
                }

                statusLabel.Text = "Sale found - " + dt.Rows.Count + " item(s). Click CONFIRM REFUND to proceed.";
                statusLabel.ForeColor = Color.FromArgb(30, 120, 50);
                confirmButton.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error searching for sale. Please contact your administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── CONFIRM REFUND ───────────────────────────────────────────────────────
        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (refundGrid.Rows.Count == 0)
            {
                MessageBox.Show("No sale loaded. Please search for a Sale ID first.", "No Sale", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to refund this sale?\nThis will add the quantities back to stock.",
                "Confirm Refund",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                string saleId = salesIdTextBox.Text.Trim();

                productdb_connect.product_sql = "SELECT product_id FROM salesTbl WHERE sale_id = '" + saleId + "'";
                productdb_connect.product_cmd();
                productdb_connect.product_sqladapterSelect();
                productdb_connect.product_sqldatasetSELECT();

                DataTable dt = productdb_connect.product_sql_dataset.Tables[0];

                // Add back 1 unit per sale row to stock
                foreach (DataRow row in dt.Rows)
                {
                    string productId = row["product_id"].ToString();

                    productdb_connect.product_sql = "UPDATE productTbl SET quantity = quantity + 1 WHERE productid = '" + productId + "'";
                    productdb_connect.product_cmd();
                    productdb_connect.product_sqladapterUpdate();
                }

                // Mark the sale as refunded in the database
                productdb_connect.product_sql = "UPDATE salesTbl SET discount_type = 'REFUNDED - ' + discount_type WHERE sale_id = '" + saleId + "'";
                productdb_connect.product_cmd();
                productdb_connect.product_sqladapterUpdate();

                MessageBox.Show("Refund successful! Stock has been restored.", "Refund Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear everything after a successful refund
                refundGrid.Rows.Clear();
                salesIdTextBox.Clear();
                statusLabel.Text = "";
                confirmButton.Enabled = false;
                ClearSummaryLabels();
                salesIdTextBox.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Error processing refund. Please contact your administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── CANCEL BUTTON ────────────────────────────────────────────────────────
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ─── ENTER KEY IN SEARCH BOX ──────────────────────────────────────────────
        // IMPORTANT: In the designer, hook this to the KeyDown event of salesIdTextBox
        //            NOT TextChanged — KeyDown gives you KeyEventArgs with KeyCode
        private void salesIdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }

        // ─── HELPER: resets summary labels to empty/default state ─────────────────
        private void ClearSummaryLabels()
        {
            CashierLabel.Text = "—";
            PaymentTypeLabel.Text = "—";
            DiscountLabel.Text = "—";
            TotalRefundLabel.Text = "P0.00";
        }
    }
}