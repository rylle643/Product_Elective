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
    public partial class Quantity : Form
    {
        public int QuantityValue { get; private set; }

        public Quantity()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Quantity_Load(object sender, EventArgs e)
        {
            QuantitytextBox.Focus();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuantitytextBox.Text = "1";
            QuantitytextBox.SelectAll();
            if (int.TryParse(QuantitytextBox.Text, out int qty) && qty > 0)
            {
                QuantityValue = qty;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
