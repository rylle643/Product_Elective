using System;
using System.Drawing;
using System.Windows.Forms;

namespace Product_Elective
{
    public partial class Payment : Form
    {
        public string PaymentType;
        public decimal AmountPaid;
        public decimal Change;
        private decimal grandTotal;

        public Payment(decimal total)
        {
            grandTotal = total;
            InitializeComponent();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            label3.Text = "₱" + grandTotal.ToString("#,##0.00");

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "Cash", "E-Wallet", "Card", "Others" });
            comboBox1.SelectedIndex = 0;

            label7.Text = "₱0.00";

            textBox1.Enter += textBox1_Enter;

            SetCashMode();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Cash")
                SetCashMode();
            else
                SetNonCashMode();
        }

        private void SetCashMode()
        {
            textBox1.ReadOnly = false;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.FromArgb(30, 10, 20);
            textBox1.Clear();
            label7.Text = "₱0.00";
            textBox1.Focus();
        }

        private void SetNonCashMode()
        {
            textBox1.ReadOnly = true;
            textBox1.BackColor = Color.FromArgb(210, 210, 210);       
            textBox1.ForeColor = Color.FromArgb(90, 90, 90);           
            textBox1.Text = "₱" + grandTotal.ToString("#,##0.00"); 
            label7.Text = "₱0.00";
            comboBox1.Focus();                                          
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.ReadOnly)
                comboBox1.Focus(); 
        }



        private void button1_Click(object sender, EventArgs e)
        {
            PaymentType = comboBox1.Text;

            if (PaymentType == "Cash")
            {
                if (!decimal.TryParse(textBox1.Text, out decimal amount))
                {
                    label7.Text = "⚠ Enter a valid amount";
                    textBox1.Focus();
                    return;
                }

                decimal change = amount - grandTotal;

                if (change < 0)
                {
                    label7.Text = "Short by ₱" + Math.Abs(change).ToString("#,##0.00");
                    label7.ForeColor = Color.FromArgb(160, 50, 50);
                    textBox1.Focus();
                    return;
                }

                AmountPaid = amount;
                Change = change;
                label7.Text = "₱" + change.ToString("#,##0.00");
            }
            else
            {
                AmountPaid = grandTotal;
                Change = 0;
                label7.Text = "₱0.00";
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}