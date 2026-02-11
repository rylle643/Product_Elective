using Product_Elective.Properties;
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

namespace ACOTIN_POS_APPLICATION
{
    public partial class Save_Product : Form
    {
        ProductDatabase db = new ProductDatabase();
        private string picpath;
        private Image pic;
        OpenFileDialog OpenFileDialog1 = new OpenFileDialog();

        public Save_Product()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void POS_Admin_Load(object sender, EventArgs e)
        {
            try
            {
                picpathTxtbox1.Hide(); // Product picture path
                barcodeTxtbox1.Hide(); // Barcode picture path
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }

            try
            {
                LoadAllProducts();
            }
            catch
            {

            }
        }

        // Load all products to DataGridView
        private void LoadAllProducts()
        {
            try
            {
                DataTable dt = db.GetAllProducts();
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private void cleartextboxes()
        {
            try
            {
                pic = Resources.no_photo;
                Barcode_Combobox.Text = ""; // Clear barcode input
                picpathTxtbox1.Clear(); // Product picture path
                barcodeTxtbox1.Clear(); // Barcode picture path
                pictureBox1.Image = pic;
                pictureBox2.Image = pic;
                priceTxtbox1.Clear();
                quantityTxtbox1.Clear();
                unitTxtbox1.Clear();
                descriptionTxtbox1.Clear();
                nameTxtbox1.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurs in this area. Please contact your administrator!");
            }
        }

        private void open_file_image()
        {
            OpenFileDialog1.Filter = "Image Files | *.jpg; *.jpeg; *.png; *.bmp";
            OpenFileDialog1.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cleartextboxes();
        }

        // SAVE/ADD PRODUCT - button2
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(Barcode_Combobox.Text))
                {
                    MessageBox.Show("Please enter a barcode/product ID!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(nameTxtbox1.Text))
                {
                    MessageBox.Show("Please enter a product name!");
                    return;
                }

                db.InsertProduct(
                    productName: nameTxtbox1.Text,
                    productId: Barcode_Combobox.Text,
                    quantity: string.IsNullOrWhiteSpace(quantityTxtbox1.Text) ? 0 : int.Parse(quantityTxtbox1.Text),
                    price: string.IsNullOrWhiteSpace(priceTxtbox1.Text) ? 0 : decimal.Parse(priceTxtbox1.Text),
                    unit: unitTxtbox1.Text,
                    description: descriptionTxtbox1.Text,
                    productPicPath: picpathTxtbox1.Text,
                    barcodePicPath: barcodeTxtbox1.Text
                );

                MessageBox.Show("Product saved successfully!");
                LoadAllProducts();
                cleartextboxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving product: " + ex.Message);
            }
        }

        // SEARCH PRODUCT - button1
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Barcode_Combobox.Text))
                {
                    MessageBox.Show("Please enter a barcode to search!");
                    return;
                }

                DataTable dt = db.GetProductByBarcode(Barcode_Combobox  .Text);

                if (dt.Rows.Count > 0)
                {
                    // Fill textboxes with product info
                    nameTxtbox1.Text = dt.Rows[0]["product_name"].ToString();
                    quantityTxtbox1.Text = dt.Rows[0]["quantity"].ToString();
                    priceTxtbox1.Text = dt.Rows[0]["price"].ToString();
                    unitTxtbox1.Text = dt.Rows[0]["unit"].ToString();
                    descriptionTxtbox1.Text = dt.Rows[0]["description"].ToString();
                    picpathTxtbox1.Text = dt.Rows[0]["product_pic_path"].ToString();
                    barcodeTxtbox1.Text = dt.Rows[0]["barcode_pic_path"].ToString();

                    // Load images
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
                }
                else
                {
                    MessageBox.Show("Product not found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching product: " + ex.Message);
            }
        }

        // UPDATE PRODUCT - button4
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Barcode_Combobox.Text))
                {
                    MessageBox.Show("Please enter a barcode!");
                    return;
                }

                db.UpdateProduct(
                    productName: nameTxtbox1.Text,
                    productId: Barcode_Combobox.Text,
                    quantity: string.IsNullOrWhiteSpace(quantityTxtbox1.Text) ? 0 : int.Parse(quantityTxtbox1.Text),
                    price: string.IsNullOrWhiteSpace(priceTxtbox1.Text) ? 0 : decimal.Parse(priceTxtbox1.Text),
                    unit: unitTxtbox1.Text,
                    description: descriptionTxtbox1.Text,
                    productPicPath: picpathTxtbox1.Text,
                    barcodePicPath: barcodeTxtbox1.Text
                );

                MessageBox.Show("Product updated successfully!");
                LoadAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product: " + ex.Message);
            }
        }

        // DELETE PRODUCT - button3
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Barcode_Combobox.Text))
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
                    db.DeleteProduct(Barcode_Combobox .Text);
                    MessageBox.Show("Product deleted successfully!");
                    LoadAllProducts();
                    cleartextboxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message);
            }
        }

        // PRODUCT PICTURE - pictureBox1
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

        // BARCODE PICTURE - pictureBox2
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

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }


    }
}