using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Product_Elective
{
    public partial class LoyaltyForm : Form
    {
        public string CustomerName { get; private set; }
        public LoyaltyForm()
        {
            InitializeComponent();
        }

        private void LoyaltyForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NametextBox.Text))
            {
                MessageBox.Show("Please enter a customer name!");
                return;
            }

            CustomerName = NametextBox.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
