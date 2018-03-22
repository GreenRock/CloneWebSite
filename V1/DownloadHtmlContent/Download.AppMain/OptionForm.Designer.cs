namespace Download.AppMain
{
    partial class OptionForm
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
            this.gbFolder = new System.Windows.Forms.GroupBox();
            this.txtScript = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFont = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtImage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCss = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbFolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFolder
            // 
            this.gbFolder.Controls.Add(this.txtScript);
            this.gbFolder.Controls.Add(this.label3);
            this.gbFolder.Controls.Add(this.txtFont);
            this.gbFolder.Controls.Add(this.label4);
            this.gbFolder.Controls.Add(this.txtImage);
            this.gbFolder.Controls.Add(this.label2);
            this.gbFolder.Controls.Add(this.txtCss);
            this.gbFolder.Controls.Add(this.label1);
            this.gbFolder.Location = new System.Drawing.Point(13, 13);
            this.gbFolder.Name = "gbFolder";
            this.gbFolder.Size = new System.Drawing.Size(191, 127);
            this.gbFolder.TabIndex = 0;
            this.gbFolder.TabStop = false;
            this.gbFolder.Text = "Folder";
            // 
            // txtScript
            // 
            this.txtScript.Location = new System.Drawing.Point(55, 43);
            this.txtScript.Name = "txtScript";
            this.txtScript.Size = new System.Drawing.Size(119, 20);
            this.txtScript.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Script:";
            // 
            // txtFont
            // 
            this.txtFont.Location = new System.Drawing.Point(55, 95);
            this.txtFont.Name = "txtFont";
            this.txtFont.Size = new System.Drawing.Size(119, 20);
            this.txtFont.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Font:";
            // 
            // txtImage
            // 
            this.txtImage.Location = new System.Drawing.Point(55, 69);
            this.txtImage.Name = "txtImage";
            this.txtImage.Size = new System.Drawing.Size(119, 20);
            this.txtImage.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Images:";
            // 
            // txtCss
            // 
            this.txtCss.Location = new System.Drawing.Point(55, 17);
            this.txtCss.Name = "txtCss";
            this.txtCss.Size = new System.Drawing.Size(119, 20);
            this.txtCss.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Css:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(13, 151);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(191, 34);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(217, 194);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbFolder);
            this.Name = "OptionForm";
            this.Text = "Option";
            this.gbFolder.ResumeLayout(false);
            this.gbFolder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCss;
        private System.Windows.Forms.TextBox txtScript;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtFont;
        private System.Windows.Forms.Label label4;
    }
}