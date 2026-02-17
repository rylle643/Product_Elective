using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Product_Elective
{
    public partial class BarcodeGenerator : Form
    {
        public BarcodeGenerator()
        {
            InitializeComponent();

        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBarcodeNumber.Text))
                {
                    MessageBox.Show("Please enter a barcode number!");
                    return;
                }

                // Combine barcode number and description with a delimiter
                string barcodeData = txtBarcodeNumber.Text;
                if (!string.IsNullOrWhiteSpace(txtDescription.Text))
                {
                    barcodeData += "|" + txtDescription.Text;
                }

                // Generate the original barcode
                Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                Image originalBarcode = barcode.Draw(barcodeData, 50);

                // ===== ADD MARGIN HERE =====
                int margin = 15; // Pixels of white space on all sides
                int textAreaHeight = 40; // Space for text below barcode
                int totalWidth = originalBarcode.Width + (margin * 2);
                int totalHeight = originalBarcode.Height + textAreaHeight + (margin * 2);

                Bitmap finalImage = new Bitmap(totalWidth, totalHeight);

                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    // White background
                    g.Clear(Color.White);

                    // Draw barcode with margin offset
                    g.DrawImage(originalBarcode, margin, margin);

                    // Draw barcode number centered, below barcode + margin
                    using (Font font = new Font("Arial", 10, FontStyle.Bold))
                    {
                        SizeF textSize = g.MeasureString(txtBarcodeNumber.Text, font);
                        float x = margin + (originalBarcode.Width - textSize.Width) / 2;
                        float y = margin + originalBarcode.Height + 5;

                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        g.DrawString(txtBarcodeNumber.Text, font, Brushes.Black, x, y);
                    }
                }

                pictureBox1.Image = finalImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

                MessageBox.Show("Barcode generated!\nEncoded: " + barcodeData +
                    "\nVisible: " + txtBarcodeNumber.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void PrintBCPic(object sender, PrintPageEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                e.Graphics.DrawImage(pictureBox1.Image, 0, 0);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Generate barcode first!");
                return;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PNG Image|*.png";
            save.FileName = "barcode.png";

            if (save.ShowDialog() == DialogResult.OK)
            {
                // Get the actual bitmap from the PictureBox
                Bitmap bmp = (Bitmap)pictureBox1.Image;
                // Save it – no cropping, full image
                bmp.Save(save.FileName, ImageFormat.Png);
                MessageBox.Show("Barcode saved!");
            }
        }

        private void BarcodeGenerator_Load(object sender, EventArgs e)
        {

        }
    }
}