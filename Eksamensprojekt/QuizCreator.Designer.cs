namespace Eksamensprojekt
{
    partial class QuizCreator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.questionListBox = new System.Windows.Forms.ListBox();
            this.addQuestionButton = new System.Windows.Forms.Button();
            this.removeQuestionButton = new System.Windows.Forms.Button();
            this.questionGroupBox = new System.Windows.Forms.GroupBox();
            this.removeAnswerButton = new System.Windows.Forms.Button();
            this.addAnswerButton = new System.Windows.Forms.Button();
            this.answersListBox = new System.Windows.Forms.ListBox();
            this.answersLabel = new System.Windows.Forms.Label();
            this.selectImageButton = new System.Windows.Forms.Button();
            this.questionTextBox = new System.Windows.Forms.TextBox();
            this.questionLabel = new System.Windows.Forms.Label();
            this.questionTypeComboBox = new System.Windows.Forms.ComboBox();
            this.questionTypeLabel = new System.Windows.Forms.Label();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editAnswerButton = new System.Windows.Forms.Button();
            this.questionGroupBox.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // questionListBox
            // 
            this.questionListBox.FormattingEnabled = true;
            this.questionListBox.Location = new System.Drawing.Point(12, 27);
            this.questionListBox.Name = "questionListBox";
            this.questionListBox.Size = new System.Drawing.Size(120, 277);
            this.questionListBox.TabIndex = 0;
            this.questionListBox.SelectedIndexChanged += new System.EventHandler(this.questionListBox_SelectedIndexChanged);
            // 
            // addQuestionButton
            // 
            this.addQuestionButton.Location = new System.Drawing.Point(12, 310);
            this.addQuestionButton.Name = "addQuestionButton";
            this.addQuestionButton.Size = new System.Drawing.Size(56, 23);
            this.addQuestionButton.TabIndex = 1;
            this.addQuestionButton.Text = "+";
            this.addQuestionButton.UseVisualStyleBackColor = true;
            this.addQuestionButton.Click += new System.EventHandler(this.addQuestionButton_Click);
            // 
            // removeQuestionButton
            // 
            this.removeQuestionButton.Location = new System.Drawing.Point(76, 310);
            this.removeQuestionButton.Name = "removeQuestionButton";
            this.removeQuestionButton.Size = new System.Drawing.Size(56, 23);
            this.removeQuestionButton.TabIndex = 2;
            this.removeQuestionButton.Text = "-";
            this.removeQuestionButton.UseVisualStyleBackColor = true;
            this.removeQuestionButton.Click += new System.EventHandler(this.removeQuestionButton_Click);
            // 
            // questionGroupBox
            // 
            this.questionGroupBox.Controls.Add(this.editAnswerButton);
            this.questionGroupBox.Controls.Add(this.removeAnswerButton);
            this.questionGroupBox.Controls.Add(this.addAnswerButton);
            this.questionGroupBox.Controls.Add(this.answersListBox);
            this.questionGroupBox.Controls.Add(this.answersLabel);
            this.questionGroupBox.Controls.Add(this.selectImageButton);
            this.questionGroupBox.Controls.Add(this.questionTextBox);
            this.questionGroupBox.Controls.Add(this.questionLabel);
            this.questionGroupBox.Controls.Add(this.questionTypeComboBox);
            this.questionGroupBox.Controls.Add(this.questionTypeLabel);
            this.questionGroupBox.Location = new System.Drawing.Point(140, 27);
            this.questionGroupBox.Name = "questionGroupBox";
            this.questionGroupBox.Size = new System.Drawing.Size(387, 302);
            this.questionGroupBox.TabIndex = 3;
            this.questionGroupBox.TabStop = false;
            // 
            // removeAnswerButton
            // 
            this.removeAnswerButton.Location = new System.Drawing.Point(305, 272);
            this.removeAnswerButton.Name = "removeAnswerButton";
            this.removeAnswerButton.Size = new System.Drawing.Size(75, 23);
            this.removeAnswerButton.TabIndex = 8;
            this.removeAnswerButton.Text = "Remove";
            this.removeAnswerButton.UseVisualStyleBackColor = true;
            this.removeAnswerButton.Click += new System.EventHandler(this.removeAnswerButton_Click);
            // 
            // addAnswerButton
            // 
            this.addAnswerButton.Location = new System.Drawing.Point(9, 272);
            this.addAnswerButton.Name = "addAnswerButton";
            this.addAnswerButton.Size = new System.Drawing.Size(75, 23);
            this.addAnswerButton.TabIndex = 7;
            this.addAnswerButton.Text = "Add";
            this.addAnswerButton.UseVisualStyleBackColor = true;
            this.addAnswerButton.Click += new System.EventHandler(this.addAnswerButton_Click);
            // 
            // answersListBox
            // 
            this.answersListBox.FormattingEnabled = true;
            this.answersListBox.Location = new System.Drawing.Point(6, 92);
            this.answersListBox.Name = "answersListBox";
            this.answersListBox.Size = new System.Drawing.Size(375, 173);
            this.answersListBox.TabIndex = 6;
            this.answersListBox.SelectedIndexChanged += new System.EventHandler(this.answersListBox_SelectedIndexChanged);
            // 
            // answersLabel
            // 
            this.answersLabel.AutoSize = true;
            this.answersLabel.Location = new System.Drawing.Point(6, 76);
            this.answersLabel.Name = "answersLabel";
            this.answersLabel.Size = new System.Drawing.Size(50, 13);
            this.answersLabel.TabIndex = 5;
            this.answersLabel.Text = "Answers:";
            // 
            // selectImageButton
            // 
            this.selectImageButton.Location = new System.Drawing.Point(218, 45);
            this.selectImageButton.Name = "selectImageButton";
            this.selectImageButton.Size = new System.Drawing.Size(84, 23);
            this.selectImageButton.TabIndex = 4;
            this.selectImageButton.Text = "Select Image";
            this.selectImageButton.UseVisualStyleBackColor = true;
            // 
            // questionTextBox
            // 
            this.questionTextBox.Location = new System.Drawing.Point(91, 47);
            this.questionTextBox.Name = "questionTextBox";
            this.questionTextBox.Size = new System.Drawing.Size(121, 20);
            this.questionTextBox.TabIndex = 3;
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Location = new System.Drawing.Point(6, 50);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(52, 13);
            this.questionLabel.TabIndex = 2;
            this.questionLabel.Text = "Question:";
            // 
            // questionTypeComboBox
            // 
            this.questionTypeComboBox.FormattingEnabled = true;
            this.questionTypeComboBox.Location = new System.Drawing.Point(91, 19);
            this.questionTypeComboBox.Name = "questionTypeComboBox";
            this.questionTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.questionTypeComboBox.TabIndex = 1;
            // 
            // questionTypeLabel
            // 
            this.questionTypeLabel.AutoSize = true;
            this.questionTypeLabel.Location = new System.Drawing.Point(6, 22);
            this.questionTypeLabel.Name = "questionTypeLabel";
            this.questionTypeLabel.Size = new System.Drawing.Size(79, 13);
            this.questionTypeLabel.TabIndex = 0;
            this.questionTypeLabel.Text = "Question Type:";
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(539, 24);
            this.mainMenuStrip.TabIndex = 4;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editAnswerButton
            // 
            this.editAnswerButton.Enabled = false;
            this.editAnswerButton.Location = new System.Drawing.Point(91, 272);
            this.editAnswerButton.Name = "editAnswerButton";
            this.editAnswerButton.Size = new System.Drawing.Size(75, 23);
            this.editAnswerButton.TabIndex = 9;
            this.editAnswerButton.Text = "Edit";
            this.editAnswerButton.UseVisualStyleBackColor = true;
            this.editAnswerButton.Click += new System.EventHandler(this.editAnswerButton_Click);
            // 
            // QuizCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 341);
            this.Controls.Add(this.questionGroupBox);
            this.Controls.Add(this.removeQuestionButton);
            this.Controls.Add(this.addQuestionButton);
            this.Controls.Add(this.questionListBox);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "QuizCreator";
            this.Text = "QuizCreator";
            this.Load += new System.EventHandler(this.QuizCreator_Load);
            this.questionGroupBox.ResumeLayout(false);
            this.questionGroupBox.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox questionListBox;
        private System.Windows.Forms.Button addQuestionButton;
        private System.Windows.Forms.Button removeQuestionButton;
        private System.Windows.Forms.GroupBox questionGroupBox;
        private System.Windows.Forms.ComboBox questionTypeComboBox;
        private System.Windows.Forms.Label questionTypeLabel;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Button selectImageButton;
        private System.Windows.Forms.TextBox questionTextBox;
        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.Button removeAnswerButton;
        private System.Windows.Forms.Button addAnswerButton;
        private System.Windows.Forms.ListBox answersListBox;
        private System.Windows.Forms.Label answersLabel;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.Button editAnswerButton;
    }
}