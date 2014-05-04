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
    public partial class QuizAnswerer : Form
    {
        private Quiz quiz;
        private bool[] correctAnswers;
        private int[] questionOrder;
        private int questionIndex;

        private Question currentQuestion;

        public QuizAnswerer(Quiz quiz)
        {
            InitializeComponent();
            this.quiz = quiz;

            correctAnswers = new bool[quiz.Questions.Count];

            Random rnd = new Random();
            questionOrder = Enumerable.Range(0, quiz.Questions.Count).OrderBy<int, int>((item) => rnd.Next()).ToArray<int>();

            questionIndex = -1;
        }

        private void QuizAnswerer_Load(object sender, EventArgs e)
        {
            questionPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            nextQuestionButton_Click(null, null);
        }

        private void nextQuestionButton_Click(object sender, EventArgs e)
        {
            if (questionIndex > -1)
            {
                if (answersComboBox.SelectedIndex == currentQuestion.CorrectAnswer)
                {
                    correctAnswers[questionOrder[questionIndex]] = true;
                }
                else
                {
                    correctAnswers[questionOrder[questionIndex]] = false;
                }
            }

            questionIndex++;

            if (questionIndex == quiz.Questions.Count)
            {
                Console.WriteLine(string.Join(", ", correctAnswers));
                // TODO: Stats time
                this.Close();
                return;
            }

            currentQuestion = quiz.Questions[questionOrder[questionIndex]];
            if (currentQuestion.Type == QuestionType.TEXT)
            {
                questionTextLabel.Text = currentQuestion.Text;
                questionPictureBox.Visible = false;
            }
            else
            {
                questionTextLabel.Text = StringResources.LABEL_PICTURE_QUESTION;
                questionPictureBox.Visible = true;
                questionPictureBox.Image = quiz.Images[currentQuestion.ImageIndex];
            }

            answersComboBox.DataSource = currentQuestion.Answers;

            Refresh();
        }
    }
}
