using System;
using System.Collections;
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
    public partial class QuizCreator : Form
    {
        private Quiz quiz;
        private Question previouslySelectedQuestion;

        public QuizCreator()
        {
            InitializeComponent();
        }

        private void QuizCreator_Load(object sender, EventArgs e)
        {
            setupNew();

            questionTypeComboBox.DataSource = Enum.GetValues(typeof(QuestionType));
            questionTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setupNew();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Quiz Files (.qzi)|*.qzi";
            dialog.FilterIndex = 0;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                quiz = Quiz.LoadFromZip(dialog.FileName);
                setupList();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "Quiz Files (.qzi)|*.qzi";
            dialog.FilterIndex = 0;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Quiz.StoreToZip(quiz, dialog.FileName);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void questionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            savePreviousQuestion();
            loadSelectedQuestion();
        }

        private void setupNew()
        {
            quiz = new Quiz();
            quiz.Name = "Unnamed Quiz";
            quiz.Description = "Descriptionless";
            quiz.QuestionReferences = new List<QuestionReference>();
            quiz.ImageReferences = new List<ImageReference>();
            quiz.Questions = new List<Question>();
            quiz.Images = new List<Image>();

            setupList();

            addQuestionButton_Click(null, null);
        }

        private void setupList()
        {
            questionListBox.DataSource = null;
            questionListBox.DataSource = quiz.Questions;
        }

        private void savePreviousQuestion()
        {
            if (previouslySelectedQuestion == null)
                return;

            previouslySelectedQuestion.Type = (QuestionType) questionTypeComboBox.SelectedValue;
            previouslySelectedQuestion.Text = questionTextBox.Text;
            previouslySelectedQuestion.Answers.Clear();

            foreach (String answer in answersListBox.Items)
            {
                previouslySelectedQuestion.Answers.Add(answer);
            }
        }

        private void loadSelectedQuestion()
        {
            if (questionListBox.SelectedItem == null)
                return;

            Question question = (Question) questionListBox.SelectedItem;

            questionTypeComboBox.SelectedItem = question.Type;
            questionTextBox.Text = question.Text;
            answersListBox.DataSource = question.Answers;

            previouslySelectedQuestion = question;
        }

        private void addQuestionButton_Click(object sender, EventArgs e)
        {
            Question question = new Question();
            question.QuestionId = "Question" + questionListBox.Items.Count;

            quiz.Questions.Add(question);

            setupList();
        }

        private void removeQuestionButton_Click(object sender, EventArgs e)
        {
            if (questionListBox.SelectedItem == null)
                return;

            Question question = (Question)questionListBox.SelectedItem;

            quiz.Questions.Remove(question);

            setupList();
        }
    }
}
