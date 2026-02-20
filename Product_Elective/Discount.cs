using System;
using System.Windows.Forms;

namespace Product_Elective
{
    public partial class Discount : Form
    {
        // These values are read by Cashier.cs after the form closes
        public string DiscountType { get; private set; }
        public decimal DiscountRate { get; private set; }

        public Discount()
        {
            InitializeComponent();
        }

        private void Discount_Load(object sender, EventArgs e)
        {
            // Default to No Discount when the form opens
            radioButton1.Checked = true;
        }

        // ─── CONFIRM — just saves the selected discount and closes, no popup ───────
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)           // No Discount
            {
                DiscountType = "No Discount";
                DiscountRate = 0m;
            }
            else if (radioButton2.Checked)      // Senior Citizen
            {
                DiscountType = "Senior Citizen";
                DiscountRate = 0.30m;
            }
            else if (radioButton3.Checked)      // PWD
            {
                DiscountType = "PWD";
                DiscountRate = 0.25m;
            }
            else if (radioButton4.Checked)      // Employee
            {
                DiscountType = "Employee";
                DiscountRate = 0.20m;
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