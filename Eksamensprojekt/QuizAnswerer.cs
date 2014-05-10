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

            // Store the supplied quiz
            this.quiz = quiz;

            // Create an array to store question answer state in
            correctAnswers = new bool[quiz.Questions.Count];

            // Randomize question order
            // Makes use of LINQ to create a range of question indexes, and then order them according to a random number generator
            Random rnd = new Random();
            questionOrder = Enumerable.Range(0, quiz.Questions.Count).OrderBy<int, int>((item) => rnd.Next()).ToArray<int>();

            // Initialize the question index
            questionIndex = -1;
        }

        private void QuizAnswerer_Load(object sender, EventArgs e)
        {
            // Set the picture box zoom mode so all of the image is shown
            questionPictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            // Simulate a click of the next question button to facilitate loading the first question
            nextQuestionButton_Click(null, null);
        }

        private void nextQuestionButton_Click(object sender, EventArgs e)
        {
            // If this isn't the first question, validate the answer
            if (questionIndex > -1)
            {
                validateAnswer();
            }

            // Select next question
            questionIndex++;

            // If we've gone through all questions, end the quiz
            if (questionIndex == quiz.Questions.Count)
            {
                endQuiz();
                return;
            }

            // Load the current question
            currentQuestion = quiz.Questions[questionOrder[questionIndex]];

            // If the question is a text question hide the picture box and update the question text label
            if (currentQuestion.Type == QuestionType.TEXT)
            {
                questionTextLabel.Text = currentQuestion.Text;
                questionPictureBox.Visible = false;
            }
            // Otherwise show the questions image, and set the label to indicate that this is a picture question
            else
            {
                questionTextLabel.Text = StringResources.MISC_PICTURE_QUESTION_LABEL;
                questionPictureBox.Visible = true;
                questionPictureBox.Image = quiz.Images[currentQuestion.ImageIndex];
            }

            // Update answers
            answersComboBox.DataSource = currentQuestion.Answers;

            // Ask windows to re-render everything
            Refresh();
        }

        /// <summary>
        /// Validates the selected answer
        /// </summary>
        private void validateAnswer()
        {
            // If selected answer is correct, mark it as so and notify the user of this with a messagebox
            if (answersComboBox.SelectedIndex == currentQuestion.CorrectAnswer)
            {
                correctAnswers[questionOrder[questionIndex]] = true;
                MessageBox.Show(StringResources.MESSAGE_CORRECT_ANSWER);
            }
            // Otherwise mark a incorrect answer and notify the user of this with a messagebox
            else
            {
                correctAnswers[questionOrder[questionIndex]] = false;
                MessageBox.Show(String.Format(StringResources.MESSAGE_WRONG_ANSWER, currentQuestion.Answers[currentQuestion.CorrectAnswer]));
            }
        }

        /// <summary>
        /// Ends the quiz
        /// </summary>
        private void endQuiz()
        {
            // Use LINQ to count the amount of correct answers
            int numCorrect = correctAnswers.Where((item) => item).Count();

            // Tell the user how many correct answers they had with a messagebox
            MessageBox.Show(String.Format(StringResources.MESSAGE_NUM_CORRECT_ANSWERS, numCorrect));

            // Close the window
            this.Close();
        }
    }
}
