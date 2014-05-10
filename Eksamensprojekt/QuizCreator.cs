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
        private bool programmaticSelect = false;

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

                // Update the question list automatically when we select a new quiz
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

                // If a question was selected
                if (_question != null)
                {
                    // Enable group box containing controls related to the selected question
                    questionGroupBox.Enabled = true;

                    // Update the question type
                    questionTypeComboBox.SelectedItem = _question.Type;
                    updateType(_question.Type, false);

                    // Update question text
                    // TODO: This might be unnessesary
                    if (_question.Type == QuestionType.TEXT)
                    {
                        questionTextBox.Text = _question.Text;
                    }
                    else
                    {
                        questionTextBox.Text = _question.ImageIndex.ToString();
                    }

                    updateAnswerList();
                }
                else
                {
                    // Disable group box containing controls related to the selected question
                    questionGroupBox.Enabled = false;

                    // Clear input fields
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
            // Limit the ComboBox choices to available question types
            questionTypeComboBox.DataSource = Enum.GetValues(typeof(QuestionType));

            // Only allow selection from the drop down, no text input
            questionTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            setupNewQuiz();
        }

        #region Helpers
        /// <summary>
        /// Updates the type of the selected question
        /// </summary>
        /// <param name="newType">The new question type</param>
        /// <param name="set">Whether or not to actually set the value, or only update interface elements. Defaults to true (e.g. set the value)</param>
        private void updateType(QuestionType newType, bool set = true)
        {
            // Return if no question is selected
            if (SelectedQuestion == null)
                return;

            // Only set type if requested
            if (set)
                SelectedQuestion.Type = newType;
            
            // If new type is equal to image type
            // Disable question text input
            // Enable selection of images
            // Update question text box to show image index
            if (newType == QuestionType.IMAGE)
            {
                questionTextBox.Enabled = false;
                selectImageButton.Enabled = true;

                questionTextBox.Text = SelectedQuestion.ImageIndex.ToString();
            }
            // Otherwise, enable question text input and disable image selection
            // Also update question text box to show question text
            else
            {
                questionTextBox.Enabled = true;
                selectImageButton.Enabled = false;

                questionTextBox.Text = SelectedQuestion.Text;
            }
        }

        /// <summary>
        /// Updates the list of questions
        /// </summary>
        private void updateQuestionList()
        {
            // C# data bindings is iffy, reloading the list requires setting DataSource to null
            // and then back to the list
            questionListBox.DataSource = null;
            questionListBox.DataSource = SelectedQuiz.Questions;

            // Ask windows re-render the list
            questionListBox.Refresh();
        }

        /// <summary>
        /// Updates the list of answers
        /// </summary>
        private void updateAnswerList()
        {
            // C# data bindings is iffy, reloading the list requires setting DataSource to null
            // and then back to the list
            answersListBox.DataSource = null;
            answersListBox.DataSource = _question.Answers;


            // Ask windows re-render the list
            answersListBox.Refresh();
        }

        /// <summary>
        /// Sets up a new quiz and adds a blank question to it
        /// </summary>
        private void setupNewQuiz()
        {
            SelectedQuiz = new Quiz();
            SelectedQuestion = SelectedQuiz.AddQuestion();

            updateQuestionList();
            selectQuestion(0);
        }

        /// <summary>
        /// Makes sure question is valid
        /// </summary>
        /// <returns>Whether or not the question is valid</returns>
        private bool doSanityChecks()
        {
            // If explicitly disabled, or no question is selected return true
            if (programmaticSelect || SelectedQuestion == null)
                return true;

            // Make sure a question text has been entered if we're dealing with a text question
            if (SelectedQuestion.Type == QuestionType.TEXT && SelectedQuestion.Text == "")
            {
                MessageBox.Show(StringResources.ERROR_INVALID_QUESTION_TEXT, StringResources.ERROR_INVALID_QUESTION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // Make sure a valid image has been selected if we're dealing with an image question
            if (SelectedQuestion.Type == QuestionType.IMAGE && (SelectedQuestion.ImageIndex < 0 || SelectedQuestion.ImageIndex >= SelectedQuiz.Images.Count))
            {
                MessageBox.Show(StringResources.ERROR_INVALID_QUESTION_IMAGE, StringResources.ERROR_INVALID_QUESTION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // Make sure we there is atleast one answer
            if (SelectedQuestion.Answers.Count < 1)
            {
                MessageBox.Show(StringResources.ERROR_INVALID_QUESTION_ANSWERS, StringResources.ERROR_INVALID_QUESTION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // Make sure a correct answer has been marked, and that it is still a valid answer
            if (SelectedQuestion.CorrectAnswer < 0 || SelectedQuestion.CorrectAnswer > SelectedQuestion.Answers.Count)
            {
                MessageBox.Show(StringResources.ERROR_INVALID_QUESTION_CORRECT_ANSWERS, StringResources.ERROR_INVALID_QUESTION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // If the above passes, we're a-okay
            return true;
        }

        /// <summary>
        /// Selects a question on the question list
        /// </summary>
        /// <param name="index">Index of the question to select</param>
        private void selectQuestion(int index)
        {
            // Explicitly disable sanity checks, the update index
            programmaticSelect = true;
            questionListBox.SelectedIndex = index;
            programmaticSelect = false;
        }
        #endregion

        #region Tool Strip
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Setup up a new quiz when the New was selected on the menu
            setupNewQuiz();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Start a file dialog when Open was selected on the menu
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = StringResources.FILTER_QZI_DIALOG;
            dialog.FilterIndex = 0;

            // If a file was selected proceed with loading it
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Quiz quiz = Quiz.LoadFromZip(dialog.FileName);

                // Update selected quiz if no errors occur, show a message otherwise
                if (quiz != null)
                {
                    // Explicitly disable sanity checks
                    programmaticSelect = true;
                    SelectedQuiz = quiz;
                    programmaticSelect = false;
                }
                else
                    MessageBox.Show(StringResources.ERROR_OPEN_QUIZ, StringResources.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show a file dialog when Save was selected on the menu
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = StringResources.FILTER_QZI_DIALOG;
            dialog.FilterIndex = 0;

            // If a file was selected proceed with saving to it
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Quiz.StoreToZip(SelectedQuiz, dialog.FileName);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exit the application when Exit was selected on the menu
            Application.Exit();
        }
        #endregion

        #region Question List
        private void questionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If the sanity check passes, select the new question.
            // If not, select the previously selected question
            if (doSanityChecks())
            {
                SelectedQuestion = (Question)questionListBox.SelectedItem;
            }
            else
            {
                selectQuestion(questionListBox.Items.IndexOf(SelectedQuestion));
            }
        }

        private void addQuestionButton_Click(object sender, EventArgs e)
        {
            // Add a question when the add question button is clicked
            SelectedQuiz.AddQuestion();

            updateQuestionList();
        }

        private void removeQuestionButton_Click(object sender, EventArgs e)
        {
            // Remove the selected question when the remove question button is clicked
            SelectedQuiz.RemoveQuestion(SelectedQuestion);
            SelectedQuestion = null;

            updateQuestionList();
        }
        #endregion

        #region Question Editing
        private void questionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If a question has been selected, update it's type
            if (questionTypeComboBox.SelectedItem != null)
                updateType((QuestionType)questionTypeComboBox.SelectedItem);
        }

        private void questionTextBox_TextChanged(object sender, EventArgs e)
        {
            // Return if no question has been selected
            if (SelectedQuestion == null)
                return;

            // If the question is an image question update the image index when text box changes
            if (SelectedQuestion.Type == QuestionType.IMAGE)
            {
                Int32.TryParse(questionTextBox.Text, out SelectedQuestion.ImageIndex);
            }
            // Otherwise update the question text
            else
            {
                SelectedQuestion.Text = questionTextBox.Text;
            }
        }

        private void selectImageButton_Click(object sender, EventArgs e)
        {
            // Open the image selection dialog when the select image button is clicked
            ImageDialog dialog = new ImageDialog(SelectedQuiz);

            // If an image was selected, update the text box
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                questionTextBox.Text = dialog.selectedIndex.ToString();
            }
        }

        #region Answers List
        private void answersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If no answer is selected, disable answer editing and correct answer selection
            if (answersListBox.SelectedIndex <= -1)
            {
                editAnswerButton.Enabled = false;
                setCorrectAnswerButton.Enabled = false;
            }
            // Otherwise enable answer editing, and enable marking correct answer if question isn't already the correct answer
            else
            {
                editAnswerButton.Enabled = true;

                if (answersListBox.SelectedIndex == SelectedQuestion.CorrectAnswer)
                {
                    setCorrectAnswerButton.Enabled = false;
                }
                else
                {
                    setCorrectAnswerButton.Enabled = true;
                }
            }
        }

        private void addAnswerButton_Click(object sender, EventArgs e)
        {
            // Show an input dialog when the user clicks the add answer button
            String answer = Interaction.InputBox(StringResources.DIALOG_ANSWER_INPUT_MESSAGE, StringResources.DIALOG_ANSWER_INPUT_TITLE);

            // If an answer was entered, add it to the list
            if (answer.Length > 0)
            {
                SelectedQuestion.Answers.Add(answer);

                updateAnswerList();
            }
        }

        private void editAnswerButton_Click(object sender, EventArgs e)
        {
            int index = answersListBox.SelectedIndex;

            // Return if no answer is selected
            if (index < 0)
                return;

            // Show an input dialog when the edit answer button is clicked
            String answer = Interaction.InputBox(StringResources.DIALOG_ANSWER_INPUT_MESSAGE, StringResources.DIALOG_ANSWER_INPUT_TITLE, (String)answersListBox.Items[index]);

            // If an answer is entered, update the selcted answer
            if (answer.Length > 0)
            {
                // There is no way to directly update the list item so we remove the previous value and insert the new one at it's position
                SelectedQuestion.Answers.RemoveAt(index);
                SelectedQuestion.Answers.Insert(index, answer);

                updateAnswerList();
            }
        }

        private void removeAnswerButton_Click(object sender, EventArgs e)
        {
            // If an answer is selected, remove it when the remove answer button is clicked
            if (answersListBox.SelectedIndex > -1)
            {
                SelectedQuestion.Answers.RemoveAt(answersListBox.SelectedIndex);

                // If the removed answer is the correct answer, reset the correct answer
                if (answersListBox.SelectedIndex == SelectedQuestion.CorrectAnswer)
                {
                    SelectedQuestion.CorrectAnswer = -1;
                }

                updateAnswerList();
            }
        }
        #endregion

        private void setCorrectAnswerButton_Click(object sender, EventArgs e)
        {
            // If an answer is selected, set it as the correct answer when the correct answer button is clicked
            // Deactivate the button afterwards
            if (answersListBox.SelectedIndex > -1)
            {
                SelectedQuestion.CorrectAnswer = answersListBox.SelectedIndex;
                setCorrectAnswerButton.Enabled = false;
            }
        }
        #endregion
    }
}
