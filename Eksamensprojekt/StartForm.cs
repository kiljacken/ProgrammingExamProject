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
            this.Visible = false;

            new QuizCreator().ShowDialog();
            
            this.Close();
        }

        private void takeQuizButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = StringResources.QZI_DIALOG_FILTER;
            dialog.FilterIndex = 0;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Quiz quiz = Quiz.LoadFromZip(dialog.FileName);

                this.Visible = false;
                new QuizAnswerer(quiz).ShowDialog();

                this.Close();
            }
        }
    }
}
