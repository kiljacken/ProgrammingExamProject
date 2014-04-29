using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eksamensprojekt
{
    public partial class ImageDialog : Form
    {
        public int selectedIndex = -1;
        Quiz quiz;

        public ImageDialog(Quiz quiz)
        {
            InitializeComponent();
            this.quiz = quiz;
        }

        private void ImageDialog_Load(object sender, EventArgs e)
        {
            setupList();
            previewPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void setupList()
        {
            imageListBox.Items.Clear();
            for (int i = 0; i < quiz.Images.Count; i++)
            {
                imageListBox.Items.Add("Image " + i);
            }
        }

        private void imageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (imageListBox.SelectedIndex < 0)
                return;

            selectedIndex = imageListBox.SelectedIndex;

            previewPictureBox.Image = quiz.Images[selectedIndex];
            previewPictureBox.Refresh();
        }

        private void addImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "PNG File (.png)|*.png|" + "JPG File (.jpg)|*.jpg|" + "BMP File (.bmp)|*.bmp|" + "GIF File (.gif)|*.gif";
            dialog.FilterIndex = 0;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(dialog.FileName);
                quiz.Images.Add(image);
                setupList();
            }
        }

        private void removeImageButton_Click(object sender, EventArgs e)
        {
            if (imageListBox.SelectedIndex > -1)
            {
                quiz.RemoveImage(imageListBox.SelectedIndex);
                setupList();
            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (selectedIndex < 0)
            {
                MessageBox.Show("Please select a image for the question!");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
