namespace Eksamensprojekt
{
    partial class StartForm
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
            this.makeOrEditLabel = new System.Windows.Forms.Label();
            this.makeOrEditButton = new System.Windows.Forms.Button();
            this.takeQuizButton = new System.Windows.Forms.Button();
            this.takeQuizLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // makeOrEditLabel
            // 
            this.makeOrEditLabel.AutoSize = true;
            this.makeOrEditLabel.Location = new System.Drawing.Point(12, 17);
            this.makeOrEditLabel.Name = "makeOrEditLabel";
            this.makeOrEditLabel.Size = new System.Drawing.Size(100, 13);
            this.makeOrEditLabel.TabIndex = 0;
            this.makeOrEditLabel.Text = "Make or edit a quiz:";
            // 
            // makeOrEditButton
            // 
            this.makeOrEditButton.Location = new System.Drawing.Point(118, 12);
            this.makeOrEditButton.Name = "makeOrEditButton";
            this.makeOrEditButton.Size = new System.Drawing.Size(75, 23);
            this.makeOrEditButton.TabIndex = 1;
            this.makeOrEditButton.Text = "Go";
            this.makeOrEditButton.UseVisualStyleBackColor = true;
            this.makeOrEditButton.Click += new System.EventHandler(this.makeOrEditButton_Click);
            // 
            // takeQuizButton
            // 
            this.takeQuizButton.Location = new System.Drawing.Point(118, 42);
            this.takeQuizButton.Name = "takeQuizButton";
            this.takeQuizButton.Size = new System.Drawing.Size(75, 23);
            this.takeQuizButton.TabIndex = 2;
            this.takeQuizButton.Text = "Go";
            this.takeQuizButton.UseVisualStyleBackColor = true;
            this.takeQuizButton.Click += new System.EventHandler(this.takeQuizButton_Click);
            // 
            // takeQuizLabel
            // 
            this.takeQuizLabel.AutoSize = true;
            this.takeQuizLabel.Location = new System.Drawing.Point(12, 47);
            this.takeQuizLabel.Name = "takeQuizLabel";
            this.takeQuizLabel.Size = new System.Drawing.Size(66, 13);
            this.takeQuizLabel.TabIndex = 3;
            this.takeQuizLabel.Text = "Take a quiz:";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 74);
            this.Controls.Add(this.takeQuizLabel);
            this.Controls.Add(this.takeQuizButton);
            this.Controls.Add(this.makeOrEditButton);
            this.Controls.Add(this.makeOrEditLabel);
            this.Name = "StartForm";
            this.Text = "FlashQuiz";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label makeOrEditLabel;
        private System.Windows.Forms.Button makeOrEditButton;
        private System.Windows.Forms.Button takeQuizButton;
        private System.Windows.Forms.Label takeQuizLabel;
    }
}

