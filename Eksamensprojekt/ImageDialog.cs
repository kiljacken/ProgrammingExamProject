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

            // Store the supplied quiz
            this.quiz = quiz;
        }

        private void ImageDialog_Load(object sender, EventArgs e)
        {
            setupList();

            // Update the picture box size mode to show all of the image
            previewPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        /// <summary>
        /// Sets up the list of images
        /// </summary>
        private void setupList()
        {
            // Clear the list of images, and add items representing the images in the quiz
            imageListBox.Items.Clear();
            for (int i = 0; i < quiz.Images.Count; i++)
            {
                imageListBox.Items.Add(String.Format(StringResources.MISC_IMAGE_ID, i));
            }
        }

        private void imageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Return if no image has been selected
            if (imageListBox.SelectedIndex < 0)
                return;

            // Update the local index
            selectedIndex = imageListBox.SelectedIndex;

            // Show the selected image
            previewPictureBox.Image = quiz.Images[selectedIndex];
            previewPictureBox.Refresh();
        }

        private void addImageButton_Click(object sender, EventArgs e)
        {
            // Open a file dialog if the add image button is clicked
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = StringResources.FILTER_IMAGE_DIALOG;
            dialog.FilterIndex = 0;

            // If an image is selected
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Load the image
                Image image = Image.FromFile(dialog.FileName);

                // Add it to the quiz
                quiz.Images.Add(image);

                // Update the image list
                setupList();
            }
        }

        private void removeImageButton_Click(object sender, EventArgs e)
        {
            // If an image has been selected, remove it when the remove image button is clicked
            if (imageListBox.SelectedIndex > -1)
            {
                quiz.RemoveImage(imageListBox.SelectedIndex);

                // Update the image list
                setupList();
            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            // If no image is selected when the select button is clicked, notify the user with a message box
            if (selectedIndex < 0)
            {
                MessageBox.Show(StringResources.ERROR_NO_IMAGE_SELECTED);
                return;
            }

            // Otherwise set the dialog result to OK and close the dialog
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Set the dialog result to cancel and close the dialog when the cancel button is clicked
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
