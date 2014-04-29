namespace Eksamensprojekt
{
    partial class ImageDialog
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
            this.imageListBox = new System.Windows.Forms.ListBox();
            this.addImageButton = new System.Windows.Forms.Button();
            this.removeImageButton = new System.Windows.Forms.Button();
            this.previewGroupBox = new System.Windows.Forms.GroupBox();
            this.previewPictureBox = new System.Windows.Forms.PictureBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.previewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageListBox
            // 
            this.imageListBox.FormattingEnabled = true;
            this.imageListBox.Location = new System.Drawing.Point(13, 13);
            this.imageListBox.Name = "imageListBox";
            this.imageListBox.Size = new System.Drawing.Size(156, 160);
            this.imageListBox.TabIndex = 0;
            this.imageListBox.SelectedIndexChanged += new System.EventHandler(this.imageListBox_SelectedIndexChanged);
            // 
            // addImageButton
            // 
            this.addImageButton.Location = new System.Drawing.Point(12, 179);
            this.addImageButton.Name = "addImageButton";
            this.addImageButton.Size = new System.Drawing.Size(75, 23);
            this.addImageButton.TabIndex = 1;
            this.addImageButton.Text = "Add";
            this.addImageButton.UseVisualStyleBackColor = true;
            this.addImageButton.Click += new System.EventHandler(this.addImageButton_Click);
            // 
            // removeImageButton
            // 
            this.removeImageButton.Location = new System.Drawing.Point(93, 179);
            this.removeImageButton.Name = "removeImageButton";
            this.removeImageButton.Size = new System.Drawing.Size(75, 23);
            this.removeImageButton.TabIndex = 2;
            this.removeImageButton.Text = "Remove";
            this.removeImageButton.UseVisualStyleBackColor = true;
            this.removeImageButton.Click += new System.EventHandler(this.removeImageButton_Click);
            // 
            // previewGroupBox
            // 
            this.previewGroupBox.Controls.Add(this.previewPictureBox);
            this.previewGroupBox.Location = new System.Drawing.Point(176, 13);
            this.previewGroupBox.Name = "previewGroupBox";
            this.previewGroupBox.Size = new System.Drawing.Size(252, 219);
            this.previewGroupBox.TabIndex = 3;
            this.previewGroupBox.TabStop = false;
            this.previewGroupBox.Text = "Preview";
            // 
            // previewPictureBox
            // 
            this.previewPictureBox.Location = new System.Drawing.Point(6, 19);
            this.previewPictureBox.Name = "previewPictureBox";
            this.previewPictureBox.Size = new System.Drawing.Size(240, 194);
            this.previewPictureBox.TabIndex = 0;
            this.previewPictureBox.TabStop = false;
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(12, 209);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(75, 23);
            this.buttonSelect.TabIndex = 4;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(93, 209);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ImageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 240);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.previewGroupBox);
            this.Controls.Add(this.removeImageButton);
            this.Controls.Add(this.addImageButton);
            this.Controls.Add(this.imageListBox);
            this.Name = "ImageDialog";
            this.Text = "ImageDialog";
            this.Load += new System.EventHandler(this.ImageDialog_Load);
            this.previewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox imageListBox;
        private System.Windows.Forms.Button addImageButton;
        private System.Windows.Forms.Button removeImageButton;
        private System.Windows.Forms.GroupBox previewGroupBox;
        private System.Windows.Forms.PictureBox previewPictureBox;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Button buttonCancel;
    }
}