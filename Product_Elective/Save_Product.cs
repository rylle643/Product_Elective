using Product_Elective.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Product_Elective
{
    public partial class Save_Product : Form
    {
        ProductDatabase productdb_connect = new ProductDatabase();
        private string picpath;
        private Image pic;
        OpenFileDialog OpenFileDialog1 = new OpenFileDialog();

        private Timer searchTimer = new Timer();

        private string GetCleanPrice()
        {
            return priceTxtbox1.Text.Replace("₱", "").Trim();
        }

        private void SetPriceDisplay(string value)
        {
            string clean = value.Replace("₱", "").Trim();
            priceTxtbox1.Text = string.IsNullOrWhiteSpace(clean) ? "" : "₱" + clean;
        }

        public Save_Product()
        {
            productdb_connect.product_connString();
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            searchTimer.Interval = 100;
            searchTimer.Tick += SearchTimer_Tick;
        }

        private void POS_Admin_Load(object sender, EventArgs e)
        {
            
            try
            {
                picpathTxtbox1.Hide();
                barcodeTxtbox1.Hide();

                comboBoxCategory.Items.Add("Smartphones");
                comboBoxCategory.Items.Add("Laptops");
                comboBoxCategory.Items.Add("Desktop Computers");
                comboBoxCategory.Items.Add("Printers");
                comboBoxCategory.Items.Add("Gaming Consoles");
                comboBoxCategory.Items.Add("Audio Devices");
                comboBoxCategory.Items.Add("Televisions");
                comboBoxCategory.Items.Add("Cameras");
                comboBoxCategory.Items.Add("Smartwatches");
                comboBoxCategory.Items.Add("Networking Devices");
                comboBoxCategory.Items.Add("Storage Devices");
                comboBoxCategory.Items.Add("Smart Home");
                comboBoxCategory.Items.Add("Accessories");
                comboBoxCategory.Items.Add("Security Devices");

                Barcode_textBox.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }

        private void Barcode_textBox_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();

            if (Barcode_textBox.Text.Length >= 3)
                searchTimer.Start();
            else
                ClearFieldsOnly();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            SearchProduct(showWarning: false); 
        }

        private void Barcode_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchTimer.Stop();
                SearchProduct(showWarning: true); 
                e.SuppressKeyPress = true;
            }
        }


        private void SearchProduct(bool showWarning)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Barcode_textBox.Text))
                    return;

                productdb_connect.product_sql = "SELECT * FROM productTbl WHERE productid = '" + Barcode_textBox.Text + "'";
                productdb_connect.product_cmd();
                productdb_connect.product_sqladapterSelect();
                productdb_connect.product_sqldatasetSELECT();

                if (productdb_connect.product_sql_dataset.Tables[0].Rows.Count > 0)
                {
                    nameTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["product_name"].ToString();
                    quantityTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["quantity"].ToString();
                    unitTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["unit"].ToString();
                    descriptionTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["description"].ToString();
                    picpathTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["product_pic_path"].ToString();
                    barcodeTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["barcode_pic_path"].ToString();
                    brandtextBox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["brand"].ToString();
                    comboBoxCategory.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["category"].ToString();

                    SetPriceDisplay(productdb_connect.product_sql_dataset.Tables[0].Rows[0]["price"].ToString());

                    try
                    {
                        if (!string.IsNullOrEmpty(productdb_connect.product_sql_dataset.Tables[0].Rows[0]["date_added"].ToString()))
                            dateAddedPicker.Value = Convert.ToDateTime(productdb_connect.product_sql_dataset.Tables[0].Rows[0]["date_added"]);
                    }
                    catch { dateAddedPicker.Value = DateTime.Now; }

                    try
                    {
                        if (!string.IsNullOrEmpty(productdb_connect.product_sql_dataset.Tables[0].Rows[0]["date_expiration"].ToString()))
                            dateExpirationPicker.Value = Convert.ToDateTime(productdb_connect.product_sql_dataset.Tables[0].Rows[0]["date_expiration"]);
                    }
                    catch { dateExpirationPicker.Value = DateTime.Now; }

                    try
                    {
                        if (!string.IsNullOrEmpty(picpathTxtbox1.Text))
                            pictureBox1.Image = Image.FromFile(picpathTxtbox1.Text);
                    }
                    catch { pictureBox1.Image = Resources.no_photo; }

                    try
                    {
                        if (!string.IsNullOrEmpty(barcodeTxtbox1.Text))
                            pictureBox2.Image = Image.FromFile(barcodeTxtbox1.Text);
                    }
                    catch { pictureBox2.Image = Resources.no_photo; }

                    int qty = 0;
                    int.TryParse(quantityTxtbox1.Text, out qty);

                    if (qty == 0)
                        quantityTxtbox1.ForeColor = Color.Red;  
                    else if (qty < 5)
                        quantityTxtbox1.ForeColor = Color.Brown;  
                    else
                        quantityTxtbox1.ForeColor = Color.Green; 
                }
                else
                {
                    
                    if (showWarning)
                    {
                        
                        MessageBox.Show("Product not found! You can add it as a new product.");
                    }
                    ClearFieldsOnly(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching product: ");
            }
        }

        private void ClearFieldsOnly()
        {
            try
            {
                pic = Resources.no_photo;
                picpathTxtbox1.Clear();
                barcodeTxtbox1.Clear();
                pictureBox1.Image = pic;
                pictureBox2.Image = pic;
                priceTxtbox1.Clear();
                quantityTxtbox1.Clear();
                unitTxtbox1.Clear();
                descriptionTxtbox1.Clear();
                nameTxtbox1.Clear();
                brandtextBox1.Clear();
                comboBoxCategory.SelectedIndex = -1;
                dateAddedPicker.Value = DateTime.Now;
                dateExpirationPicker.Value = DateTime.Now;
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }


        private void priceTxtbox1_TextChanged(object sender, EventArgs e)
        {
            priceTxtbox1.TextChanged -= priceTxtbox1_TextChanged;

            string clean = priceTxtbox1.Text.Replace("₱", "").Trim();

            if (!string.IsNullOrWhiteSpace(clean))
            {
                priceTxtbox1.Text = "₱" + clean;
                priceTxtbox1.SelectionStart = priceTxtbox1.Text.Length;
            }
            else
            {
                priceTxtbox1.Text = "";
            }

            priceTxtbox1.TextChanged += priceTxtbox1_TextChanged;
        }

        private void priceTxtbox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back && priceTxtbox1.SelectionStart <= 1 && priceTxtbox1.Text.StartsWith("₱"))
            {
                if (priceTxtbox1.Text.Length <= 1 || priceTxtbox1.SelectionStart <= 1)
                    e.SuppressKeyPress = true;
            }

            if (e.KeyCode == Keys.Delete && priceTxtbox1.SelectionStart == 0)
                e.SuppressKeyPress = true;
        }

        private void priceTxtbox1_Click(object sender, EventArgs e)
        {
            if (priceTxtbox1.Text.StartsWith("₱") && priceTxtbox1.SelectionStart == 0)
                priceTxtbox1.SelectionStart = 1;
        }

        private void cleartextboxes()
        {
            try
            {
                pic = Resources.no_photo;
                Barcode_textBox.Text = ""; 
                picpathTxtbox1.Clear();
                barcodeTxtbox1.Clear();
                pictureBox1.Image = pic;
                pictureBox2.Image = pic;
                priceTxtbox1.Clear();
                quantityTxtbox1.Clear();
                unitTxtbox1.Clear();
                descriptionTxtbox1.Clear();
                nameTxtbox1.Clear();
                brandtextBox1.Clear();
                comboBoxCategory.SelectedIndex = -1;
                dateAddedPicker.Value = DateTime.Now;
                dateExpirationPicker.Value = DateTime.Now;
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cleartextboxes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Barcode_textBox.Text)) 
                {
                    MessageBox.Show("Please enter a barcode/product ID!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(nameTxtbox1.Text))
                {
                    MessageBox.Show("Please enter a product name!");
                    return;
                }

                string cleanPrice = GetCleanPrice();

                productdb_connect.product_sql = "INSERT INTO productTbl " +
                                                "(product_name, productid, quantity, price, unit, description, product_pic_path, barcode_pic_path, brand, category, date_added, date_expiration) " +
                                                "VALUES ('" + nameTxtbox1.Text + "', " +
                                                "'" + Barcode_textBox.Text + "', " +  
                                                (string.IsNullOrWhiteSpace(quantityTxtbox1.Text) ? "0" : quantityTxtbox1.Text) + ", " +
                                                (string.IsNullOrWhiteSpace(cleanPrice) ? "0" : cleanPrice) + ", " +
                                                "'" + unitTxtbox1.Text + "', " +
                                                "'" + descriptionTxtbox1.Text + "', " +
                                                "'" + picpathTxtbox1.Text + "', " +
                                                "'" + barcodeTxtbox1.Text + "', " +
                                                "'" + brandtextBox1.Text + "', " +
                                                "'" + comboBoxCategory.Text + "', " +
                                                "'" + dateAddedPicker.Value.ToString("yyyy-MM-dd") + "', " +
                                                "'" + dateExpirationPicker.Value.ToString("yyyy-MM-dd") + "')";
                productdb_connect.product_cmd();
                productdb_connect.product_sqladapterInsert();

                MessageBox.Show("Product saved successfully!");
                cleartextboxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving product: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchTimer.Stop();
            SearchProduct(showWarning: true); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Barcode_textBox.Text))  
                {
                    MessageBox.Show("Please enter a barcode!");
                    return;
                }

                string cleanPrice = GetCleanPrice();

                productdb_connect.product_sql = "UPDATE productTbl SET " +
                                                "product_name = '" + nameTxtbox1.Text + "', " +
                                                "quantity = " + (string.IsNullOrWhiteSpace(quantityTxtbox1.Text) ? "0" : quantityTxtbox1.Text) + ", " +
                                                "price = " + (string.IsNullOrWhiteSpace(cleanPrice) ? "0" : cleanPrice) + ", " +
                                                "unit = '" + unitTxtbox1.Text + "', " +
                                                "description = '" + descriptionTxtbox1.Text + "', " +
                                                "product_pic_path = '" + picpathTxtbox1.Text + "', " +
                                                "barcode_pic_path = '" + barcodeTxtbox1.Text + "', " +
                                                "brand = '" + brandtextBox1.Text + "', " +
                                                "category = '" + comboBoxCategory.Text + "', " +
                                                "date_added = '" + dateAddedPicker.Value.ToString("yyyy-MM-dd") + "', " +
                                                "date_expiration = '" + dateExpirationPicker.Value.ToString("yyyy-MM-dd") + "' " +
                                                "WHERE productid = '" + Barcode_textBox.Text + "'";  
                productdb_connect.product_cmd();
                productdb_connect.product_sqladapterUpdate();

                MessageBox.Show("Product updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Barcode_textBox.Text))  
                {
                    MessageBox.Show("Please enter a barcode to delete!");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this product?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    productdb_connect.product_sql = "DELETE FROM productTbl WHERE productid = '" + Barcode_textBox.Text + "'"; 
                    productdb_connect.product_cmd();
                    productdb_connect.product_sqladapterDelete();

                    MessageBox.Show("Product deleted successfully!");
                    cleartextboxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog1.Filter = "Image Files | *.jpg; *.jpeg; *.png; *.bmp";
                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName);
                    picpath = OpenFileDialog1.FileName;
                    picpathTxtbox1.Text = picpath;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No image selected!");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog1.Filter = "Image Files | *.jpg; *.jpeg; *.png; *.bmp";
                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.Image = Image.FromFile(OpenFileDialog1.FileName);
                    picpath = OpenFileDialog1.FileName;
                    barcodeTxtbox1.Text = picpath;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No image selected!");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void quantityTxtbox1_TextChanged(object sender, EventArgs e)
        {
            int qty = 0;
            int.TryParse(quantityTxtbox1.Text, out qty);

            if (qty == 0)
                quantityTxtbox1.ForeColor = Color.Red;  
            else if (qty < 5)
                quantityTxtbox1.ForeColor = Color.Brown; 
            else
                quantityTxtbox1.ForeColor = Color.Green;  
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}