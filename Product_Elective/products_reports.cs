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
    public partial class products_reports : Form
    {
        ProductDatabase productdb_connect = new ProductDatabase();

        public products_reports()
        {
            productdb_connect.product_connString();
            InitializeComponent();
        }

        private void StyleDataGridView()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.GridColor = Color.FromArgb(200, 180, 190);
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = 42;
            dataGridView1.RowTemplate.Height = 38;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.EnableHeadersVisualStyles = false;

            // ── Column Header ────────────────────────────────────────────────────
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(160, 50, 90);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11.5F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(160, 50, 90);
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // ── Even Rows ────────────────────────────────────────────────────────
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(30, 10, 20);
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;   // DEFAULT = LEFT
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 70, 110);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.DefaultCellStyle.Padding = new Padding(8, 0, 5, 0);

            // ── Odd Rows ─────────────────────────────────────────────────────────
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 240, 242);
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(30, 10, 20);
            dataGridView1.AlternatingRowsDefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dataGridView1.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;   // DEFAULT = LEFT
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 70, 110);
            dataGridView1.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void StyleColumns()
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                // Set all columns to MiddleLeft by default
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                switch (col.Name)
                {
                    case "product_name": col.HeaderText = "Product Name"; col.FillWeight = 180; break;
                    case "productid": col.HeaderText = "Barcode / ID"; col.FillWeight = 100; break;
                    case "quantity": col.HeaderText = "Qty"; col.FillWeight = 60; break;
                    case "price": col.HeaderText = "Price (₱)"; col.FillWeight = 90; break;
                    case "unit": col.HeaderText = "Unit"; col.FillWeight = 70; break;
                    case "description": col.HeaderText = "Description"; col.FillWeight = 200; break;
                    case "product_pic_path": col.HeaderText = "Product Image"; col.FillWeight = 110; break;
                    case "barcode_pic_path": col.HeaderText = "Barcode Image"; col.FillWeight = 110; break;
                    case "brand": col.HeaderText = "Brand"; col.FillWeight = 110; break;
                    case "category": col.HeaderText = "Category"; col.FillWeight = 110; break;
                    case "date_added": col.HeaderText = "Date Added"; col.FillWeight = 100; break;
                    case "date_expiration": col.HeaderText = "Expiration Date"; col.FillWeight = 110; break;
                }
            }

            // All text columns — explicitly MiddleLeft (overrides anything inherited)
            string[] leftAlignCols = { "product_name", "description", "brand", "category", "unit", "productid", "date_added", "date_expiration" };
            foreach (string colName in leftAlignCols)
                if (dataGridView1.Columns.Contains(colName))
                    dataGridView1.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Numeric columns — MiddleLeft as requested
            if (dataGridView1.Columns.Contains("price"))
                dataGridView1.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            if (dataGridView1.Columns.Contains("quantity"))
                dataGridView1.Columns["quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Format price column with ₱ sign
            if (dataGridView1.Columns.Contains("price"))
                dataGridView1.Columns["price"].DefaultCellStyle.Format = "₱#,##0.00";

            // Hide raw image path columns
            if (dataGridView1.Columns.Contains("product_pic_path"))
                dataGridView1.Columns["product_pic_path"].Visible = false;
            if (dataGridView1.Columns.Contains("barcode_pic_path"))
                dataGridView1.Columns["barcode_pic_path"].Visible = false;
        }

        private void product_select()
        {
            productdb_connect.product_cmd();
            productdb_connect.product_sqladapterSelect();
            productdb_connect.product_sqldatasetSELECT();
            dataGridView1.DataSource = productdb_connect.product_sql_dataset.Tables[0];
            StyleColumns();
        }

        private void cleartextboxes()
        {
            optionCombo.SelectedIndex = 0;
            optionInputTxtbox.Clear();
            optionCombo.Focus();
        }

        private void cleartextboxes1()
        {
            optionInputTxtbox.Clear();
            optionInputTxtbox.Focus();
        }

        // ── Hover ────────────────────────────────────────────────────────────────
        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(220, 170, 190);
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(30, 10, 20);
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(180, 70, 110);
            }
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex % 2 == 0)
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                else
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(245, 240, 242);

                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(30, 10, 20);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void employee_reports_Load(object sender, EventArgs e)
        {
            StyleDataGridView();

            optionCombo.Items.Add("Select search option");
            optionCombo.Items.Add("product_name");
            optionCombo.Items.Add("productid");
            optionCombo.Items.Add("brand");
            optionCombo.Items.Add("category");
            optionCombo.Items.Add("unit");
            optionCombo.Items.Add("quantity");
            optionCombo.Items.Add("price");
            optionCombo.Items.Add("date_added");
            optionCombo.Items.Add("date_expiration");

            optionCombo.SelectedIndex = 0;

            productdb_connect.product_sql = "SELECT * FROM productTbl";
            product_select();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(optionInputTxtbox.Text))
                {
                    MessageBox.Show("Please enter a search value!");
                    return;
                }

                if (optionCombo.Text == "product_name")
                {
                    productdb_connect.product_sql = "SELECT * FROM productTbl WHERE product_name LIKE '%" + optionInputTxtbox.Text + "%'";
                    product_select();
                    cleartextboxes1();
                }
                else if (optionCombo.Text == "productid")
                {
                    productdb_connect.product_sql = "SELECT * FROM productTbl WHERE productid = '" + optionInputTxtbox.Text + "'";
                    product_select();
                    cleartextboxes1();
                }
                else if (optionCombo.Text == "brand")
                {
                    productdb_connect.product_sql = "SELECT * FROM productTbl WHERE brand LIKE '%" + optionInputTxtbox.Text + "%'";
                    product_select();
                    cleartextboxes1();
                }
                else if (optionCombo.Text == "category")
                {
                    productdb_connect.product_sql = "SELECT * FROM productTbl WHERE category = '" + optionInputTxtbox.Text + "'";
                    product_select();
                    cleartextboxes1();
                }
                else if (optionCombo.Text == "unit")
                {
                    productdb_connect.product_sql = "SELECT * FROM productTbl WHERE unit = '" + optionInputTxtbox.Text + "'";
                    product_select();
                    cleartextboxes1();
                }
                else if (optionCombo.Text == "quantity")
                {
                    productdb_connect.product_sql = "SELECT * FROM productTbl WHERE quantity = " + optionInputTxtbox.Text;
                    product_select();
                    cleartextboxes1();
                }
                else if (optionCombo.Text == "price")
                {
                    productdb_connect.product_sql = "SELECT * FROM productTbl WHERE price = " + optionInputTxtbox.Text;
                    product_select();
                    cleartextboxes1();
                }
                else if (optionCombo.Text == "date_added")
                {
                    productdb_connect.product_sql = "SELECT * FROM productTbl WHERE date_added = '" + optionInputTxtbox.Text + "'";
                    product_select();
                    cleartextboxes1();
                }
                else if (optionCombo.Text == "date_expiration")
                {
                    productdb_connect.product_sql = "SELECT * FROM productTbl WHERE date_expiration = '" + optionInputTxtbox.Text + "'";
                    product_select();
                    cleartextboxes1();
                }
                else
                {
                    MessageBox.Show("Please select a search option!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }

        private void optionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                productdb_connect.product_sql = "SELECT * FROM productTbl";
                product_select();
                cleartextboxes();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }
    }
}