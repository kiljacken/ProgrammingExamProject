﻿using Microsoft.VisualBasic;
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
        private int lastIndex = -1;

        public QuizCreator()
        {
            InitializeComponent();
        }

        #region Setup
        private void QuizCreator_Load(object sender, EventArgs e)
        {
            setupNew();

            questionTypeComboBox.DataSource = Enum.GetValues(typeof(QuestionType));
            questionTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
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

            previouslySelectedQuestion = null;

            setupList();

            addQuestionButton_Click(null, null);
        }

        private void setupList()
        {
            questionListBox.DataSource = null;
            questionListBox.DataSource = quiz.Questions;
        }

        private void setupAnswers()
        {
            answersListBox.DataSource = null;
            answersListBox.DataSource = previouslySelectedQuestion.Answers;
        }
        #endregion

        #region Tool Strip Menu
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
                if (quiz != null)
                    setupList();    
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savePreviousQuestion();

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
        #endregion

        #region Question Selection
        private void questionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (questionListBox.SelectedIndex == lastIndex)
                return;

            int nil;
            if ((QuestionType) questionTypeComboBox.SelectedValue == QuestionType.IMAGE &&
                !Int32.TryParse(questionTextBox.Text, out nil))
            {
                MessageBox.Show("Please select a image for the question!");

                questionListBox.SelectedIndex = lastIndex;
                return;
            }

            savePreviousQuestion();
            loadSelectedQuestion();
            lastIndex = questionListBox.SelectedIndex;
        }

        private void savePreviousQuestion()
        {
            if (previouslySelectedQuestion == null)
                return;

            previouslySelectedQuestion.Type = (QuestionType)questionTypeComboBox.SelectedValue;

            if (previouslySelectedQuestion.Type == QuestionType.TEXT)
            {
                previouslySelectedQuestion.Text = questionTextBox.Text;
            }
            else
            {
                previouslySelectedQuestion.ImageIndex = Int32.Parse(questionTextBox.Text);
            }
            
        }

        private void loadSelectedQuestion()
        {
            if (questionListBox.SelectedItem == null)
                return;

            Question question = (Question)questionListBox.SelectedItem;

            questionTypeComboBox.SelectedItem = question.Type;

            if (question.Type == QuestionType.TEXT)
            {
                questionTextBox.Text = question.Text;
            }
            else
            {
                questionTextBox.Text = question.ImageIndex.ToString();
            }

            previouslySelectedQuestion = question;
            setupAnswers();
        }
        #endregion

        #region Question List
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
        #endregion

        #region Answer List
        private void addAnswerButton_Click(object sender, EventArgs e)
        {
            String answer = Interaction.InputBox("Input answer here", "Answer creation");
            if (answer.Length > 0)
            {
                previouslySelectedQuestion.Answers.Add(answer);
                setupAnswers();
            }
        }

        private void editAnswerButton_Click(object sender, EventArgs e)
        {
            int index = answersListBox.SelectedIndex;

            String answer = Interaction.InputBox("Input answer here", "Answer creation", (String)answersListBox.Items[index]);
            if (answer.Length > 0)
            {
                previouslySelectedQuestion.Answers.RemoveAt(index);
                previouslySelectedQuestion.Answers.Insert(index, answer);
                setupAnswers();
            }
        }

        private void removeAnswerButton_Click(object sender, EventArgs e)
        {
            if (answersListBox.SelectedIndex > -1)
            {
                previouslySelectedQuestion.Answers.RemoveAt(answersListBox.SelectedIndex);
                setupAnswers();
            }
        }

        private void answersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            editAnswerButton.Enabled = answersListBox.SelectedIndex > -1;
        }
        #endregion

        private void selectImageButton_Click(object sender, EventArgs e)
        {
            ImageDialog dialog = new ImageDialog(quiz);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                questionTextBox.Text = dialog.selectedIndex.ToString();
            }
        }

        private void questionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            questionTextBox.Text = "";
            if ((QuestionType) questionTypeComboBox.SelectedValue == QuestionType.TEXT)
            {
                questionTextBox.Enabled = true;
                selectImageButton.Enabled = false;
            }
            else
            {
                questionTextBox.Enabled = false;
                selectImageButton.Enabled = true;
            }
        }
    }
}
