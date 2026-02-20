using System;
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

        private void Quantity_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(QuantitytextBox.Text))
                QuantitytextBox.Text = "1";
            QuantitytextBox.Focus();
            QuantitytextBox.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(QuantitytextBox.Text.Trim(), out int qty) && qty > 0)
            {
                QuantityValue = qty;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity!");
                QuantitytextBox.Focus();
                QuantitytextBox.SelectAll();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}