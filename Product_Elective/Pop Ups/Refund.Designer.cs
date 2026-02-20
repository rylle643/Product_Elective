namespace Product_Elective
{
    partial class Refund
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.salesIdTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.refundGrid = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CashierLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PaymentTypeLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.DiscountLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.TotalRefundLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refundGrid)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // panel1
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.salesIdTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(28, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(970, 100);
            this.panel1.TabIndex = 0;
            // button1
            this.button1.BackColor = System.Drawing.Color.PaleVioletRed;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(800, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 33);
            this.button1.TabIndex = 24;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // salesIdTextBox
            this.salesIdTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.salesIdTextBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salesIdTextBox.Location = new System.Drawing.Point(155, 33);
            this.salesIdTextBox.Name = "salesIdTextBox";
            this.salesIdTextBox.Size = new System.Drawing.Size(614, 33);
            this.salesIdTextBox.TabIndex = 25;
            // FIX 1: Changed from TextChanged (EventArgs) to KeyDown (KeyEventArgs)
            this.salesIdTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.salesIdTextBox_KeyDown);
            // label3
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.label3.Location = new System.Drawing.Point(24, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 25);
            this.label3.TabIndex = 24;
            this.label3.Text = "Sale ID:";
            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 47);
            this.label1.TabIndex = 22;
            this.label1.Text = "REFUND";
            // label2
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(23, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(319, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "Search by Sale ID to view and refund a transaction";
            // refundGrid
            this.refundGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.refundGrid.Location = new System.Drawing.Point(28, 279);
            this.refundGrid.Name = "refundGrid";
            this.refundGrid.Size = new System.Drawing.Size(970, 239);
            this.refundGrid.TabIndex = 24;
            // panel2
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.CashierLabel);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(28, 539);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(221, 73);
            this.panel2.TabIndex = 25;
            // CashierLabel
            this.CashierLabel.AutoSize = true;
            this.CashierLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CashierLabel.ForeColor = System.Drawing.Color.Black;
            this.CashierLabel.Location = new System.Drawing.Point(3, 33);
            this.CashierLabel.Name = "CashierLabel";
            this.CashierLabel.Size = new System.Drawing.Size(96, 30);
            this.CashierLabel.TabIndex = 28;
            // FIX 2: Default text was "REFUND", now shows dash
            this.CashierLabel.Text = "—";
            // label4
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 26;
            this.label4.Text = "Cashier";
            // label5
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.label5.Location = new System.Drawing.Point(3, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 20);
            this.label5.TabIndex = 26;
            this.label5.Text = "Payment Type";
            // panel3
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.PaymentTypeLabel);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(277, 539);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(221, 73);
            this.panel3.TabIndex = 27;
            // PaymentTypeLabel
            this.PaymentTypeLabel.AutoSize = true;
            this.PaymentTypeLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PaymentTypeLabel.ForeColor = System.Drawing.Color.Black;
            this.PaymentTypeLabel.Location = new System.Drawing.Point(2, 33);
            this.PaymentTypeLabel.Name = "PaymentTypeLabel";
            this.PaymentTypeLabel.Size = new System.Drawing.Size(96, 30);
            this.PaymentTypeLabel.TabIndex = 29;
            // FIX 2: Default text was "REFUND"
            this.PaymentTypeLabel.Text = "—";
            // label6
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.label6.Location = new System.Drawing.Point(3, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 20);
            this.label6.TabIndex = 26;
            this.label6.Text = "Discount Applied";
            // panel4
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.DiscountLabel);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Location = new System.Drawing.Point(527, 539);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(221, 73);
            this.panel4.TabIndex = 27;
            // DiscountLabel
            this.DiscountLabel.AutoSize = true;
            this.DiscountLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiscountLabel.ForeColor = System.Drawing.Color.Black;
            this.DiscountLabel.Location = new System.Drawing.Point(2, 33);
            this.DiscountLabel.Name = "DiscountLabel";
            this.DiscountLabel.Size = new System.Drawing.Size(96, 30);
            this.DiscountLabel.TabIndex = 30;
            // FIX 2: Default text was "REFUND"
            this.DiscountLabel.Text = "—";
            // label7
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.label7.Location = new System.Drawing.Point(3, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 20);
            this.label7.TabIndex = 26;
            this.label7.Text = "Total Refund Amount";
            // panel5
            this.panel5.BackColor = System.Drawing.Color.LavenderBlush;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.TotalRefundLabel);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Location = new System.Drawing.Point(777, 539);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(221, 73);
            this.panel5.TabIndex = 27;
            // TotalRefundLabel
            this.TotalRefundLabel.AutoSize = true;
            this.TotalRefundLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalRefundLabel.ForeColor = System.Drawing.Color.Black;
            this.TotalRefundLabel.Location = new System.Drawing.Point(2, 32);
            this.TotalRefundLabel.Name = "TotalRefundLabel";
            this.TotalRefundLabel.Size = new System.Drawing.Size(96, 30);
            this.TotalRefundLabel.TabIndex = 31;
            // FIX 2: Default text was "REFUND"
            this.TotalRefundLabel.Text = "₱0.00";
            // button2 — CANCEL
            this.button2.BackColor = System.Drawing.Color.Brown;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(821, 638);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 44);
            this.button2.TabIndex = 29;
            this.button2.Text = "CANCEL";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // confirmButton
            this.confirmButton.BackColor = System.Drawing.Color.Green;
            this.confirmButton.Cursor = System.Windows.Forms.Cursors.Hand;
            // FIX 3: Was missing Enabled = false — button must be disabled until a valid sale is found
            this.confirmButton.Enabled = false;
            this.confirmButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.confirmButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmButton.ForeColor = System.Drawing.Color.White;
            this.confirmButton.Location = new System.Drawing.Point(638, 638);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(177, 44);
            this.confirmButton.TabIndex = 28;
            this.confirmButton.Text = "Confirm";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // panel6 — status bar
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.statusLabel);
            this.panel6.Location = new System.Drawing.Point(28, 218);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(970, 55);
            this.panel6.TabIndex = 30;
            // statusLabel
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.Black;
            this.statusLabel.Location = new System.Drawing.Point(4, 20);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(70, 21);
            this.statusLabel.TabIndex = 29;
            // FIX 2: Default text was "REFUND"
            this.statusLabel.Text = "";
            // Refund form
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.ClientSize = new System.Drawing.Size(1030, 704);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.refundGrid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "Refund";
            this.Text = "Refund";
            this.Load += new System.EventHandler(this.Refund_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refundGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox salesIdTextBox;
        private System.Windows.Forms.DataGridView refundGrid;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label CashierLabel;
        private System.Windows.Forms.Label PaymentTypeLabel;
        private System.Windows.Forms.Label DiscountLabel;
        private System.Windows.Forms.Label TotalRefundLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label statusLabel;
    }
}