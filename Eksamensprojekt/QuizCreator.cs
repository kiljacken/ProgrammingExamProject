using Microsoft.VisualBasic;
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
        private Quiz _quiz;
        public Quiz SelectedQuiz
        {
            get
            {
                return _quiz;
            }

            set
            {
                _quiz = value;

                updateQuestionList();
            }
        }

        private Question _question;
        private Question SelectedQuestion
        {
            get
            {
                return _question;
            }

            set
            {
                _question = value;

                if (_question != null)
                {
                    questionGroupBox.Enabled = true;

                    questionTypeComboBox.SelectedItem = _question.Type;

                    updateType(_question.Type, false);
                    questionTextBox.Text = _question.Type == QuestionType.TEXT ? _question.Text : _question.ImageIndex.ToString();

                    updateAnswerList();
                }
                else
                {
                    questionGroupBox.Enabled = false;

                    questionTypeComboBox.SelectedItem = null;
                    questionTextBox.Text = "";
                    
                    answersListBox.DataSource = null;
                }
            }
        }

        public QuizCreator()
        {
            InitializeComponent();
        }

        private void QuizCreator_Load(object sender, EventArgs e)
        {
            questionTypeComboBox.DataSource = Enum.GetValues(typeof(QuestionType));
            questionTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            setupNewQuiz();
        }

        #region Helpers
        private void updateType(QuestionType newType, bool set = true)
        {
            if (SelectedQuestion == null)
                return;

            if (set)
                SelectedQuestion.Type = newType;

            if (newType == QuestionType.IMAGE)
            {
                questionTextBox.Enabled = false;
                selectImageButton.Enabled = true;

                questionTextBox.Text = SelectedQuestion.ImageIndex.ToString();
            }
            else
            {
                questionTextBox.Enabled = true;
                selectImageButton.Enabled = false;

                questionTextBox.Text = SelectedQuestion.Text;
            }
        }

        private void updateQuestionList()
        {
            questionListBox.DataSource = null;
            questionListBox.DataSource = SelectedQuiz.Questions;
            questionListBox.Refresh();
        }

        private void updateAnswerList()
        {
            answersListBox.DataSource = null;
            answersListBox.DataSource = _question.Answers;
            questionListBox.Refresh();
        }

        private void setupNewQuiz()
        {
            SelectedQuiz = new Quiz();
            SelectedQuestion = SelectedQuiz.AddQuestion();

            updateQuestionList();
            questionListBox.SelectedIndex = 0;
        }
        #endregion

        #region Tool Strip
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setupNewQuiz();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Quiz Files (.qzi)|*.qzi";
            dialog.FilterIndex = 0;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Quiz quiz = Quiz.LoadFromZip(dialog.FileName);

                if (quiz != null)
                    SelectedQuiz = quiz;
                else
                    MessageBox.Show("Error while opening quiz", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Quiz Files (.qzi)|*.qzi";
            dialog.FilterIndex = 0;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Quiz.StoreToZip(SelectedQuiz, dialog.FileName);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Question List
        private void questionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedQuestion = (Question)questionListBox.SelectedItem;
        }

        private void addQuestionButton_Click(object sender, EventArgs e)
        {
            SelectedQuiz.AddQuestion();

            updateQuestionList();
        }

        private void removeQuestionButton_Click(object sender, EventArgs e)
        {
            SelectedQuiz.RemoveQuestion(SelectedQuestion);
            SelectedQuestion = null;

            updateQuestionList();
        }
        #endregion

        #region Question Editing
        private void questionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (questionTypeComboBox.SelectedItem != null)
                updateType((QuestionType)questionTypeComboBox.SelectedItem);
        }

        private void questionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (SelectedQuestion == null)
                return;

            if (SelectedQuestion.Type == QuestionType.IMAGE)
            {
                Int32.TryParse(questionTextBox.Text, out SelectedQuestion.ImageIndex);
            }
            else
            {
                SelectedQuestion.Text = questionTextBox.Text;
            }
        }

        private void selectImageButton_Click(object sender, EventArgs e)
        {
            ImageDialog dialog = new ImageDialog(SelectedQuiz);

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                questionTextBox.Text = dialog.selectedIndex.ToString();
            }
            // TODO: Handle image selection
        }

        #region Answers List
        private void answersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (answersListBox.SelectedIndex <= -1)
            {
                editAnswerButton.Enabled = false;
            }
            else
            {
                editAnswerButton.Enabled = true;
            }
        }

        private void addAnswerButton_Click(object sender, EventArgs e)
        {
            String answer = Interaction.InputBox("Input answer here", "Answer creation");
            if (answer.Length > 0)
            {
                SelectedQuestion.Answers.Add(answer);

                updateAnswerList();
            }
        }

        private void editAnswerButton_Click(object sender, EventArgs e)
        {
            int index = answersListBox.SelectedIndex;

            if (index < 0)
                return;

            String answer = Interaction.InputBox("Input answer here", "Answer creation", (String)answersListBox.Items[index]);
            if (answer.Length > 0)
            {
                SelectedQuestion.Answers.RemoveAt(index);
                SelectedQuestion.Answers.Insert(index, answer);

                updateAnswerList();
            }
        }

        private void removeAnswerButton_Click(object sender, EventArgs e)
        {
            if (answersListBox.SelectedIndex > -1)
            {
                SelectedQuestion.Answers.RemoveAt(answersListBox.SelectedIndex);

                updateAnswerList();
            }
        }
        #endregion
        #endregion
    }
}
