using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_Elective
{
    public partial class Full : Form
    {
        public Full()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Full_Load(object sender, EventArgs e)
        {
            menuStrip1.BackColor = Color.FromArgb(219, 112, 147);
            menuStrip1.ForeColor = Color.White;
            label1.ForeColor = Color.FromArgb(160, 32, 64);
            label2.ForeColor = Color.FromArgb(219, 112, 147);
            timer1.Start();
            timer1_Tick(null, null);
            LoadDashboardStats();
            this.IsMdiContainer = true;
        }

 

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy").ToUpper();
        }

        private void LoadDashboardStats()
        {
            ProductDatabase db = new ProductDatabase();
            db.product_connString();

            // Total Products
            db.product_sql = "SELECT COUNT(*) FROM productTbl";
            db.product_cmd();
            TotalProductLabel.Text = db.product_sql_command.ExecuteScalar().ToString();

            // Total Stocks
            db.product_sql = "SELECT SUM(quantity) FROM productTbl";
            db.product_cmd();
            TotalStocksLabel.Text = db.product_sql_command.ExecuteScalar().ToString();

            // Total Sales ← COUNT not SUM!
            db.product_sql = "SELECT COUNT(*) FROM salesTbl";
            db.product_cmd();
            TotalSalesLabel.Text = db.product_sql_command.ExecuteScalar().ToString();

            // Total Customers
            db.product_sql = "SELECT COUNT(*) FROM loyaltyTbl";
            db.product_cmd();
            TotalCustomerLabel.Text = db.product_sql_command.ExecuteScalar().ToString();

            db.product_sql_connection.Close();
        }

        private void lblClock_Click(object sender, EventArgs e)
        {

        }

        private void inventoryManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {

            pnlWelcome.Visible = false;

            Save_Product inventory = new Save_Product();
            inventory.MdiParent = this;
            inventory.StartPosition = FormStartPosition.CenterParent;
            inventory.WindowState = FormWindowState.Maximized;
            inventory.Show();
        }

        private void inventoryManagementSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainFrm_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
                pnlWelcome.Visible = true;
            else
                pnlWelcome.Visible = false;
        }

        private void cashierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlWelcome.Visible = false;

            Cashier cashier = new Cashier();
            cashier.MdiParent = this;
            cashier.StartPosition = FormStartPosition.CenterParent;
            cashier.WindowState = FormWindowState.Maximized;
            cashier.Show();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlWelcome.Visible = false;

            products_reports reports = new products_reports();
            reports.MdiParent = this;
            reports.StartPosition = FormStartPosition.CenterParent;
            reports.WindowState = FormWindowState.Maximized;
            reports.Show();
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form child in this.MdiChildren)
            {
                child.Close();
            }
        }
    }
}
