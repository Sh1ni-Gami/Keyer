using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Keyer
{
    public partial class Form1 : Form
    {
        private Color keyColor;
        private string imagePath;

        public Form1()
        {
            InitializeComponent();
            Text = "Keyer";
            Size = new Size(400, 300);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SelectImageButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Выбрать изображение",
                Filter = "PNG Images|*.png"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(imagePath);
                ProcessImageButton.Enabled = true;
            }
        }

        private void ProcessImageButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new ColorDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                keyColor = openFileDialog.Color;
                var processedImage = ProcessImage(Image.FromFile(imagePath), keyColor);
                SaveProcessedImage(processedImage);
            }
        }
        private Image ProcessImage(Image image, Color keyColor)
        {
            var processedImage = new Bitmap(image.Width, image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var pixelColor = ((Bitmap)image).GetPixel(x, y);

                    if (pixelColor == keyColor)
                    {
                        processedImage.SetPixel(x, y, Color.Transparent);
                    }
                    else
                    {
                        processedImage.SetPixel(x, y, pixelColor);
                    }
                }
            }

            return processedImage;
        }
        private void SaveProcessedImage(Image processedImage)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Сохранить изображение",
                Filter = "PNG Images|*.png"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                processedImage.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                MessageBox.Show("Image processed and saved successfully!");
            }
        }

    }
}

