namespace ACOTIN_POS_APPLICATION
{
    partial class Save_Product
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameTxtbox1 = new System.Windows.Forms.TextBox();
            this.Barcode_Combobox = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.picpathTxtbox1 = new System.Windows.Forms.TextBox();
            this.barcodeTxtbox1 = new System.Windows.Forms.TextBox();
            this.quantityTxtbox1 = new System.Windows.Forms.TextBox();
            this.priceTxtbox1 = new System.Windows.Forms.TextBox();
            this.unitTxtbox1 = new System.Windows.Forms.TextBox();
            this.descriptionTxtbox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.brandtextBox1 = new System.Windows.Forms.TextBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.dateAddedPicker = new System.Windows.Forms.DateTimePicker();
            this.dateExpirationPicker = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // nameTxtbox1
            // 
            this.nameTxtbox1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTxtbox1.Location = new System.Drawing.Point(906, 218);
            this.nameTxtbox1.Name = "nameTxtbox1";
            this.nameTxtbox1.Size = new System.Drawing.Size(812, 35);
            this.nameTxtbox1.TabIndex = 30;
            // 
            // Barcode_Combobox
            // 
            this.Barcode_Combobox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Barcode_Combobox.FormattingEnabled = true;
            this.Barcode_Combobox.Location = new System.Drawing.Point(245, 739);
            this.Barcode_Combobox.Name = "Barcode_Combobox";
            this.Barcode_Combobox.Size = new System.Drawing.Size(381, 33);
            this.Barcode_Combobox.TabIndex = 185;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label51.Location = new System.Drawing.Point(186, 476);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(222, 21);
            this.label51.TabIndex = 186;
            this.label51.Text = "Product Barcode (Code 128)";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PaleVioletRed;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(882, 745);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(258, 98);
            this.button1.TabIndex = 187;
            this.button1.Text = "SEARCH";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PaleVioletRed;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(1473, 745);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(258, 98);
            this.button2.TabIndex = 194;
            this.button2.Text = "SAVE";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(1181, 861);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(258, 100);
            this.button3.TabIndex = 196;
            this.button3.Text = "DELETE";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.PaleVioletRed;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(1181, 745);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(258, 98);
            this.button4.TabIndex = 195;
            this.button4.Text = "UPDATE";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Gainsboro;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(1473, 863);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(258, 98);
            this.button5.TabIndex = 198;
            this.button5.Text = "EXIT";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(882, 862);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(258, 98);
            this.button6.TabIndex = 197;
            this.button6.Text = "NEW / CANCEL";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // picpathTxtbox1
            // 
            this.picpathTxtbox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.picpathTxtbox1.Location = new System.Drawing.Point(217, 294);
            this.picpathTxtbox1.Name = "picpathTxtbox1";
            this.picpathTxtbox1.Size = new System.Drawing.Size(437, 23);
            this.picpathTxtbox1.TabIndex = 199;
            // 
            // barcodeTxtbox1
            // 
            this.barcodeTxtbox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barcodeTxtbox1.Location = new System.Drawing.Point(225, 647);
            this.barcodeTxtbox1.Name = "barcodeTxtbox1";
            this.barcodeTxtbox1.Size = new System.Drawing.Size(437, 23);
            this.barcodeTxtbox1.TabIndex = 201;
            // 
            // quantityTxtbox1
            // 
            this.quantityTxtbox1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantityTxtbox1.Location = new System.Drawing.Point(906, 302);
            this.quantityTxtbox1.Name = "quantityTxtbox1";
            this.quantityTxtbox1.Size = new System.Drawing.Size(403, 35);
            this.quantityTxtbox1.TabIndex = 203;
            // 
            // priceTxtbox1
            // 
            this.priceTxtbox1.BackColor = System.Drawing.Color.LavenderBlush;
            this.priceTxtbox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.priceTxtbox1.Font = new System.Drawing.Font("Segoe UI Black", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceTxtbox1.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.priceTxtbox1.Location = new System.Drawing.Point(906, 93);
            this.priceTxtbox1.Name = "priceTxtbox1";
            this.priceTxtbox1.Size = new System.Drawing.Size(437, 93);
            this.priceTxtbox1.TabIndex = 205;
            // 
            // unitTxtbox1
            // 
            this.unitTxtbox1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unitTxtbox1.Location = new System.Drawing.Point(1315, 302);
            this.unitTxtbox1.Name = "unitTxtbox1";
            this.unitTxtbox1.Size = new System.Drawing.Size(403, 35);
            this.unitTxtbox1.TabIndex = 207;
            // 
            // descriptionTxtbox1
            // 
            this.descriptionTxtbox1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionTxtbox1.Location = new System.Drawing.Point(906, 474);
            this.descriptionTxtbox1.Multiline = true;
            this.descriptionTxtbox1.Name = "descriptionTxtbox1";
            this.descriptionTxtbox1.Size = new System.Drawing.Size(812, 213);
            this.descriptionTxtbox1.TabIndex = 209;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label6.Location = new System.Drawing.Point(186, 785);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 21);
            this.label6.TabIndex = 210;
            this.label6.Text = "Category:";
            // 
            // brandtextBox1
            // 
            this.brandtextBox1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brandtextBox1.Location = new System.Drawing.Point(190, 926);
            this.brandtextBox1.Name = "brandtextBox1";
            this.brandtextBox1.Size = new System.Drawing.Size(489, 35);
            this.brandtextBox1.TabIndex = 213;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(190, 820);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(489, 38);
            this.comboBoxCategory.TabIndex = 217;
            // 
            // dateAddedPicker
            // 
            this.dateAddedPicker.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateAddedPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateAddedPicker.Location = new System.Drawing.Point(906, 381);
            this.dateAddedPicker.Name = "dateAddedPicker";
            this.dateAddedPicker.Size = new System.Drawing.Size(403, 35);
            this.dateAddedPicker.TabIndex = 218;
            // 
            // dateExpirationPicker
            // 
            this.dateExpirationPicker.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateExpirationPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateExpirationPicker.Location = new System.Drawing.Point(1315, 381);
            this.dateExpirationPicker.Name = "dateExpirationPicker";
            this.dateExpirationPicker.Size = new System.Drawing.Size(403, 35);
            this.dateExpirationPicker.TabIndex = 219;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(186, 882);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 21);
            this.label7.TabIndex = 220;
            this.label7.Text = "Brand:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label10.Location = new System.Drawing.Point(902, 194);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 21);
            this.label10.TabIndex = 221;
            this.label10.Text = "Product Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(911, 448);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 21);
            this.label1.TabIndex = 222;
            this.label1.Text = "Description";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label11.Location = new System.Drawing.Point(1311, 357);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 21);
            this.label11.TabIndex = 223;
            this.label11.Text = "Expiration Date";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label12.Location = new System.Drawing.Point(902, 357);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 21);
            this.label12.TabIndex = 224;
            this.label12.Text = "Date Added";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label13.Location = new System.Drawing.Point(1311, 278);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 21);
            this.label13.TabIndex = 225;
            this.label13.Text = "Unit / Weight";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label14.Location = new System.Drawing.Point(902, 278);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(142, 21);
            this.label14.TabIndex = 226;
            this.label14.Text = "Quantity in Stock";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(902, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 21);
            this.label2.TabIndex = 227;
            this.label2.Text = "Price";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Location = new System.Drawing.Point(190, 507);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(489, 212);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 200;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(190, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(489, 386);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 80;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Save_Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dateExpirationPicker);
            this.Controls.Add(this.dateAddedPicker);
            this.Controls.Add(this.comboBoxCategory);
            this.Controls.Add(this.brandtextBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.descriptionTxtbox1);
            this.Controls.Add(this.unitTxtbox1);
            this.Controls.Add(this.priceTxtbox1);
            this.Controls.Add(this.quantityTxtbox1);
            this.Controls.Add(this.barcodeTxtbox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.picpathTxtbox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.Barcode_Combobox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.nameTxtbox1);
            this.Name = "Save_Product";
            this.Text = "POS_Admin";
            this.Load += new System.EventHandler(this.POS_Admin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox nameTxtbox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox Barcode_Combobox;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox picpathTxtbox1;
        private System.Windows.Forms.TextBox barcodeTxtbox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox quantityTxtbox1;
        private System.Windows.Forms.TextBox priceTxtbox1;
        private System.Windows.Forms.TextBox unitTxtbox1;
        private System.Windows.Forms.TextBox descriptionTxtbox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox brandtextBox1;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.DateTimePicker dateAddedPicker;
        private System.Windows.Forms.DateTimePicker dateExpirationPicker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label2;
    }
}