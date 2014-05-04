namespace Eksamensprojekt
{
    partial class QuizAnswerer
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
            this.nextQuestionButton = new System.Windows.Forms.Button();
            this.answersComboBox = new System.Windows.Forms.ComboBox();
            this.questionTextLabel = new System.Windows.Forms.Label();
            this.questionPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.questionPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // nextQuestionButton
            // 
            this.nextQuestionButton.Location = new System.Drawing.Point(12, 227);
            this.nextQuestionButton.Name = "nextQuestionButton";
            this.nextQuestionButton.Size = new System.Drawing.Size(75, 23);
            this.nextQuestionButton.TabIndex = 0;
            this.nextQuestionButton.Text = "Next";
            this.nextQuestionButton.UseVisualStyleBackColor = true;
            this.nextQuestionButton.Click += new System.EventHandler(this.nextQuestionButton_Click);
            // 
            // answersComboBox
            // 
            this.answersComboBox.FormattingEnabled = true;
            this.answersComboBox.Location = new System.Drawing.Point(12, 200);
            this.answersComboBox.Name = "answersComboBox";
            this.answersComboBox.Size = new System.Drawing.Size(260, 21);
            this.answersComboBox.TabIndex = 1;
            // 
            // questionTextLabel
            // 
            this.questionTextLabel.AutoSize = true;
            this.questionTextLabel.Location = new System.Drawing.Point(13, 13);
            this.questionTextLabel.Name = "questionTextLabel";
            this.questionTextLabel.Size = new System.Drawing.Size(35, 13);
            this.questionTextLabel.TabIndex = 2;
            this.questionTextLabel.Text = "label1";
            // 
            // questionPictureBox
            // 
            this.questionPictureBox.Location = new System.Drawing.Point(13, 30);
            this.questionPictureBox.Name = "questionPictureBox";
            this.questionPictureBox.Size = new System.Drawing.Size(259, 164);
            this.questionPictureBox.TabIndex = 3;
            this.questionPictureBox.TabStop = false;
            // 
            // QuizAnswerer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.questionPictureBox);
            this.Controls.Add(this.questionTextLabel);
            this.Controls.Add(this.answersComboBox);
            this.Controls.Add(this.nextQuestionButton);
            this.Name = "QuizAnswerer";
            this.Text = "QuizAnswerer";
            this.Load += new System.EventHandler(this.QuizAnswerer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.questionPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button nextQuestionButton;
        private System.Windows.Forms.ComboBox answersComboBox;
        private System.Windows.Forms.Label questionTextLabel;
        private System.Windows.Forms.PictureBox questionPictureBox;
    }
}