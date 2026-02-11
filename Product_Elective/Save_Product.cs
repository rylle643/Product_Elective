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
        ProductDatabase productdb_connect = new ProductDatabase();
        private string picpath;
        private Image pic;
        OpenFileDialog OpenFileDialog1 = new OpenFileDialog();

        public Save_Product()
        {
            productdb_connect.product_connString();
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
                
            }
            catch
            {

            }
        }

        // Load all products to DataGridView
        //private void LoadAllProducts()
        //{
        //    try
        //    {
        //        productdb_connect.product_sql = "SELECT * FROM productTbl";
        //        productdb_connect.product_cmd();
        //        productdb_connect.product_sqladapterSelect();
        //        productdb_connect.product_sqldatasetSELECT();
        //        dataGridView1.DataSource = productdb_connect.product_sql_dataset.Tables[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error loading products: " + ex.Message);
        //    }
        //}

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

                productdb_connect.product_sql = "INSERT INTO productTbl (product_name, productid, quantity, price, unit, description, product_pic_path, barcode_pic_path) " +
                                                 "VALUES ('" + nameTxtbox1.Text + "', '" + Barcode_Combobox.Text + "', " +
                                                 (string.IsNullOrWhiteSpace(quantityTxtbox1.Text) ? "0" : quantityTxtbox1.Text) + ", " +
                                                 (string.IsNullOrWhiteSpace(priceTxtbox1.Text) ? "0" : priceTxtbox1.Text) + ", '" +
                                                 unitTxtbox1.Text + "', '" + descriptionTxtbox1.Text + "', '" +
                                                 picpathTxtbox1.Text + "', '" + barcodeTxtbox1.Text + "')";
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

                productdb_connect.product_sql = "SELECT * FROM productTbl WHERE productid = '" + Barcode_Combobox.Text + "'";
                productdb_connect.product_cmd();
                productdb_connect.product_sqladapterSelect();
                productdb_connect.product_sqldatasetSELECT();

                if (productdb_connect.product_sql_dataset.Tables[0].Rows.Count > 0)
                {
                    // Fill textboxes with product info
                    nameTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["product_name"].ToString();
                    quantityTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["quantity"].ToString();
                    priceTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["price"].ToString();
                    unitTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["unit"].ToString();
                    descriptionTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["description"].ToString();
                    picpathTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["product_pic_path"].ToString();
                    barcodeTxtbox1.Text = productdb_connect.product_sql_dataset.Tables[0].Rows[0]["barcode_pic_path"].ToString();

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

                productdb_connect.product_sql = "UPDATE productTbl SET product_name = '" + nameTxtbox1.Text + "', " +
                                                 "quantity = " + (string.IsNullOrWhiteSpace(quantityTxtbox1.Text) ? "0" : quantityTxtbox1.Text) + ", " +
                                                 "price = " + (string.IsNullOrWhiteSpace(priceTxtbox1.Text) ? "0" : priceTxtbox1.Text) + ", " +
                                                 "unit = '" + unitTxtbox1.Text + "', " +
                                                 "description = '" + descriptionTxtbox1.Text + "', " +
                                                 "product_pic_path = '" + picpathTxtbox1.Text + "', " +
                                                 "barcode_pic_path = '" + barcodeTxtbox1.Text + "' " +
                                                 "WHERE productid = '" + Barcode_Combobox.Text + "'";
                productdb_connect.product_cmd();
                productdb_connect.product_sqladapterUpdate();

                MessageBox.Show("Product updated successfully!"); 
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
                    productdb_connect.product_sql = "DELETE FROM productTbl WHERE productid = '" + Barcode_Combobox.Text + "'";
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

        private void barcodeTxtbox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}