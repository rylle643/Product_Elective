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
    public partial class Discount : Form
    {
        public string DiscountType { get; private set; }
        public decimal DiscountRate { get; private set; }

        public Discount()
        {
            InitializeComponent();
        }

        private void Discount_Load(object sender, EventArgs e)
        {
            // Default to No Discount
            radioButton1.Checked = true;
        }

        // ─── CONFIRM ──────────────────────────────────────────────────────────────
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)       // No Discount
            {
                DiscountType = "No Discount";
                DiscountRate = 0;
            }
            else if (radioButton2.Checked)  // Senior Citizen
            {
                DiscountType = "Senior Citizen";
                DiscountRate = 0.20m;
            }
            else if (radioButton3.Checked)  // PWD
            {
                DiscountType = "PWD";
                DiscountRate = 0.20m;
            }
            else if (radioButton4.Checked)  // Employee
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
