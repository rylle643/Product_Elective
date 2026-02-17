using System;
using System.Windows.Forms;

namespace Product_Elective
{
    public partial class Payment : Form
    {
        public string PaymentType { get; private set; }
        public decimal AmountPaid { get; private set; }
        public decimal Change { get; private set; }

        private readonly decimal grandTotal;

        public Payment(decimal total)
        {
            grandTotal = total;
            InitializeComponent();
        }

        // ─── LOAD ─────────────────────────────────────────────────────────────────
        private void Payment_Load(object sender, EventArgs e)
        {
            label3.Text = "₱" + grandTotal.ToString("#,##0.00");

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "Cash", "GCash", "Card", "Others" });
            comboBox1.SelectedIndex = 0;

            label7.Text = "₱0.00";

            ToggleCashFields(true);
        }

        // ─── PAYMENT TYPE CHANGED ─────────────────────────────────────────────────
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCash = comboBox1.Text == "Cash";
            ToggleCashFields(isCash);

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
                label7.Text = change >= 0
                    ? "₱" + change.ToString("#,##0.00")
                    : "₱0.00  (Not enough!)";
            }
            else
            {
                label7.Text = "₱0.00";
            }
        }

        // ─── CALCULATE BUTTON (button3) ───────────────────────────────────────────
        private void button3_Click_1(object sender, EventArgs e)
        {
            string type = comboBox1.Text;

            if (type == "Cash")
            {
                if (!decimal.TryParse(textBox1.Text, out decimal amount) || amount < grandTotal)
                {
                    MessageBox.Show("Amount given is less than the total!", "Invalid Payment",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }

                decimal change = amount - grandTotal;

                MessageBox.Show(
                    "Payment Type:     " + type + "\n" +
                    "Cash Given:       ₱" + amount.ToString("#,##0.00") + "\n" +
                    "Total Amount:     ₱" + grandTotal.ToString("#,##0.00") + "\n" +
                    "Change:           ₱" + change.ToString("#,##0.00"),
                    "Payment Summary",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            else
            {
                MessageBox.Show(
                    "Payment Type:     " + type + "\n" +
                    "Cash Given:       N/A" + "\n" +
                    "Total Amount:     ₱" + grandTotal.ToString("#,##0.00") + "\n" +
                    "Change:           ₱0.00",
                    "Payment Summary",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        // ─── CONFIRM (button1) — no auto print, just validate and close ───────────
        private void button1_Click(object sender, EventArgs e)
        {
            PaymentType = comboBox1.Text;

            if (PaymentType == "Cash")
            {
                if (!decimal.TryParse(textBox1.Text, out decimal amount) || amount < grandTotal)
                {
                    MessageBox.Show("Amount given is less than the total!", "Invalid Payment",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }

                AmountPaid = amount;
                Change = amount - grandTotal;
            }
            else
            {
                AmountPaid = grandTotal;
                Change = 0;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // ─── CANCEL (button2) ─────────────────────────────────────────────────────
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // ─── HELPER ───────────────────────────────────────────────────────────────
        private void ToggleCashFields(bool visible)
        {
            label5.Visible = visible;
            textBox1.Visible = visible;
            label6.Visible = visible;
            label7.Visible = visible;
        }
    }
}