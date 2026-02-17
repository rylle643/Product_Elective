using System;
using System.Drawing;
using System.Windows.Forms;

namespace Product_Elective
{
    public partial class Payment : Form
    {
        // ─── PUBLIC RESULTS (read by Cashier after OK) ────────────────────────────
        public string PaymentType { get; private set; }
        public decimal AmountPaid { get; private set; }
        public decimal Change { get; private set; }

        private decimal grandTotal;

        public Payment(decimal total)
        {
            grandTotal = total;
            InitializeComponent();
        }

        // ─── LOAD — set total label and populate combobox ─────────────────────────
        private void Payment_Load(object sender, EventArgs e)
        {
            // Show the grand total in label3
            label3.Text = "₱" + grandTotal.ToString("#,##0.00");
            label3.ForeColor = Color.FromArgb(219, 112, 147);

            // Populate payment types
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "Cash", "GCash", "Card", "Others" });
            comboBox1.SelectedIndex = 0;

            // Set change label default
            label7.Text = "₱0.00";
            label7.ForeColor = Color.FromArgb(219, 112, 147);

            // Hide amount given + change — only show for Cash
            label5.Visible = true;
            textBox1.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
        }

        // ─── PAYMENT TYPE CHANGED ─────────────────────────────────────────────────
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCash = comboBox1.Text == "Cash";

            // Show amount given + change only for Cash
            label5.Visible = isCash;
            textBox1.Visible = isCash;
            label6.Visible = isCash;
            label7.Visible = isCash;

            if (!isCash)
            {
                textBox1.Clear();
                label7.Text = "₱0.00";
            }
        }

        // ─── AMOUNT TEXTBOX — live change calculation ─────────────────────────────
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox1.Text, out decimal amount))
            {
                decimal change = amount - grandTotal;

                if (change >= 0)
                {
                    label7.Text = "₱" + change.ToString("#,##0.00");
                    label7.ForeColor = Color.FromArgb(219, 112, 147);  // enough — rose color
                }
                else
                {
                    label7.Text = "₱0.00  (Not enough!)";
                    label7.ForeColor = Color.FromArgb(200, 60, 60);    // red — not enough
                }
            }
            else
            {
                label7.Text = "₱0.00";
                label7.ForeColor = Color.FromArgb(219, 112, 147);
            }
        }

        // ─── CONFIRM BUTTON (button1) ─────────────────────────────────────────────
        private void button1_Click(object sender, EventArgs e)
        {
            PaymentType = comboBox1.Text;

            if (PaymentType == "Cash")
            {
                if (!decimal.TryParse(textBox1.Text, out decimal amount) || amount < grandTotal)
                {
                    MessageBox.Show("Amount given is less than the total!");
                    textBox1.Focus();
                    return;
                }
                AmountPaid = amount;
                Change = amount - grandTotal;
            }
            else
            {
                // Non-cash — exact payment assumed
                AmountPaid = grandTotal;
                Change = 0;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // ─── CANCEL BUTTON (button2) ──────────────────────────────────────────────
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ─── EMPTY HANDLERS (kept so Designer does not break) ────────────────────
        private void label3_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
    }
}