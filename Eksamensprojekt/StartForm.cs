using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eksamensprojekt
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void makeOrEditButton_Click(object sender, EventArgs e)
        {
            // Hide this window when the make or edit button is clicked
            this.Visible = false;

            // Open the quiz creator
            new QuizCreator().ShowDialog();
            
            // Completely close this window
            this.Close();
        }

        private void takeQuizButton_Click(object sender, EventArgs e)
        {
            // Open a file dialog when the take quiz button is selected
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = StringResources.FILTER_QZI_DIALOG;
            dialog.FilterIndex = 0;

            // If a file is selected
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Load the file
                Quiz quiz = Quiz.LoadFromZip(dialog.FileName);

                // Hide this window
                this.Visible = false;

                // Open the quiz answerer window
                new QuizAnswerer(quiz).ShowDialog();

                // Completely close this window
                this.Close();
            }
        }
    }
}
