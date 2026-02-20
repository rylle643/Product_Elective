using System;
using System.Drawing;
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
            comboBox1.Items.AddRange(new string[] { "Cash", "E-Wallet", "Card", "Others" });
            comboBox1.SelectedIndex = 0;

            label7.Text = "₱0.00";

            // Wire up the Enter event to block focus on non-cash mode
            textBox1.Enter += textBox1_Enter;

            SetCashMode();
        }

        // ─── PAYMENT TYPE CHANGED ─────────────────────────────────────────────────
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Cash")
                SetCashMode();
            else
                SetNonCashMode();
        }

        // ─── Cash mode: white, editable, cashier types the amount ────────────────
        private void SetCashMode()
        {
            textBox1.ReadOnly = false;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.FromArgb(30, 10, 20);
            textBox1.Clear();
            label7.Text = "₱0.00";
            textBox1.Focus();
        }

        // ─── Non-cash mode: gray, locked, shows the exact total ──────────────────
        private void SetNonCashMode()
        {
            textBox1.ReadOnly = true;
            textBox1.BackColor = Color.FromArgb(210, 210, 210);        // gray background
            textBox1.ForeColor = Color.FromArgb(90, 90, 90);           // gray text
            textBox1.Text = "₱" + grandTotal.ToString("#,##0.00"); // auto-fill exact amount
            label7.Text = "₱0.00";
            comboBox1.Focus();                                          // kick focus away from textbox
        }

        // ─── Bounce focus away if cashier clicks the locked textbox ──────────────
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.ReadOnly)
                comboBox1.Focus();   // redirect focus back to combobox immediately
        }

        // ─── AMOUNT TEXTBOX — live change calculation (cash only) ─────────────────
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "Cash")
                return;

            if (decimal.TryParse(textBox1.Text, out decimal amount))
            {
                decimal change = amount - grandTotal;
                label7.Text = change >= 0
                    ? "₱" + change.ToString("#,##0.00")
                    : "⚠ Short by ₱" + Math.Abs(change).ToString("#,##0.00");
            }
            else
            {
                label7.Text = "₱0.00";
            }
        }

        // ─── CALCULATE BUTTON — updates change label, no popup ───────────────────
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Cash")
            {
                if (!decimal.TryParse(textBox1.Text, out decimal amount))
                {
                    label7.Text = "⚠ Enter a valid amount";
                    textBox1.Focus();
                    return;
                }

                decimal change = amount - grandTotal;
                label7.Text = change >= 0
                    ? "₱" + change.ToString("#,##0.00")
                    : "⚠ Short by ₱" + Math.Abs(change).ToString("#,##0.00");
            }
            else
            {
                label7.Text = "₱0.00";
            }
        }

        // ─── CONFIRM — validates and closes, no popup ─────────────────────────────
        private void button1_Click(object sender, EventArgs e)
        {
            PaymentType = comboBox1.Text;

            if (PaymentType == "Cash")
            {
                if (!decimal.TryParse(textBox1.Text, out decimal amount) || amount < grandTotal)
                {
                    label7.Text = "⚠ Amount is not enough!";
                    textBox1.Focus();
                    return;
                }

                AmountPaid = amount;
                Change = amount - grandTotal;
            }
            else
            {
                // E-Wallet / Card / Others — exact amount, zero change
                AmountPaid = grandTotal;
                Change = 0;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // ─── CANCEL ───────────────────────────────────────────────────────────────
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}