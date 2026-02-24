using System;
using System.Windows.Forms;

namespace Product_Elective
{
    public partial class Discount : Form
    {
        public string DiscountType;
        public decimal DiscountRate;

        public Discount()
        {
            InitializeComponent();
        }

        private void Discount_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)  
            {
                DiscountType = "No Discount";
                DiscountRate = 0m;
            }
            else if (radioButton2.Checked)     
            {
                DiscountType = "Senior Citizen";
                DiscountRate = 0.30m;
            }
            else if (radioButton3.Checked)    
            {
                DiscountType = "PWD";
                DiscountRate = 0.25m;
            }
            else if (radioButton4.Checked)    
            {
                DiscountType = "Employee";
                DiscountRate = 0.20m;
            }

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