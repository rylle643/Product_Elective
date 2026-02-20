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
            QuantitytextBox.Value = 1;  // NumericUpDown uses .Value not .Text
            QuantitytextBox.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuantityValue = (int)QuantitytextBox.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (QuantitytextBox.Value > 1)
                QuantitytextBox.Value--;
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            QuantitytextBox.Value++;
        }
    }
}