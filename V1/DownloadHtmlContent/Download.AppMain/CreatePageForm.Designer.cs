namespace Download.AppMain
{
    partial class FrmCreatePage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreatePage));
            this.label1 = new System.Windows.Forms.Label();
            this.txtLinkName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.btnCreatePage = new System.Windows.Forms.Button();
            this.dgvListPage = new System.Windows.Forms.DataGridView();
            this.LinkName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Link = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPageName = new System.Windows.Forms.TextBox();
            this.btnAddLink = new System.Windows.Forms.Button();
            this.btnAddHtml = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Link name:";
            // 
            // txtLinkName
            // 
            this.txtLinkName.Location = new System.Drawing.Point(84, 29);
            this.txtLinkName.Name = "txtLinkName";
            this.txtLinkName.Size = new System.Drawing.Size(99, 20);
            this.txtLinkName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Link:";
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(225, 29);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(263, 20);
            this.txtLink.TabIndex = 1;
            // 
            // btnCreatePage
            // 
            this.btnCreatePage.Location = new System.Drawing.Point(395, 5);
            this.btnCreatePage.Name = "btnCreatePage";
            this.btnCreatePage.Size = new System.Drawing.Size(93, 20);
            this.btnCreatePage.TabIndex = 3;
            this.btnCreatePage.Text = "Create";
            this.btnCreatePage.UseVisualStyleBackColor = true;
            // 
            // dgvListPage
            // 
            this.dgvListPage.AllowUserToAddRows = false;
            this.dgvListPage.AllowUserToDeleteRows = false;
            this.dgvListPage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListPage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LinkName,
            this.Link,
            this.btnDelete});
            this.dgvListPage.Location = new System.Drawing.Point(12, 58);
            this.dgvListPage.Name = "dgvListPage";
            this.dgvListPage.Size = new System.Drawing.Size(575, 331);
            this.dgvListPage.TabIndex = 4;
            // 
            // LinkName
            // 
            this.LinkName.DataPropertyName = "LinkName";
            this.LinkName.Frozen = true;
            this.LinkName.HeaderText = "Name";
            this.LinkName.Name = "LinkName";
            this.LinkName.Width = 180;
            // 
            // Link
            // 
            this.Link.DataPropertyName = "Link";
            this.Link.HeaderText = "Link";
            this.Link.Name = "Link";
            this.Link.Width = 250;
            // 
            // btnDelete
            // 
            this.btnDelete.HeaderText = "Delete";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "Delete";
            this.btnDelete.ToolTipText = "Delete";
            this.btnDelete.UseColumnTextForButtonValue = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Page name:";
            // 
            // txtPageName
            // 
            this.txtPageName.Location = new System.Drawing.Point(84, 5);
            this.txtPageName.Name = "txtPageName";
            this.txtPageName.Size = new System.Drawing.Size(305, 20);
            this.txtPageName.TabIndex = 1;
            // 
            // btnAddLink
            // 
            this.btnAddLink.Location = new System.Drawing.Point(494, 28);
            this.btnAddLink.Name = "btnAddLink";
            this.btnAddLink.Size = new System.Drawing.Size(93, 21);
            this.btnAddLink.TabIndex = 5;
            this.btnAddLink.Text = "Add";
            this.btnAddLink.UseVisualStyleBackColor = true;
            // 
            // btnAddHtml
            // 
            this.btnAddHtml.Location = new System.Drawing.Point(494, 5);
            this.btnAddHtml.Name = "btnAddHtml";
            this.btnAddHtml.Size = new System.Drawing.Size(93, 21);
            this.btnAddHtml.TabIndex = 5;
            this.btnAddHtml.Text = "Add Html";
            this.btnAddHtml.UseVisualStyleBackColor = true;
            // 
            // FrmCreatePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(601, 398);
            this.Controls.Add(this.btnAddHtml);
            this.Controls.Add(this.btnAddLink);
            this.Controls.Add(this.dgvListPage);
            this.Controls.Add(this.btnCreatePage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.txtPageName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLinkName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmCreatePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Page";
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLinkName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Button btnCreatePage;
        private System.Windows.Forms.DataGridView dgvListPage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPageName;
        private System.Windows.Forms.Button btnAddLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn LinkName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Link;
        private System.Windows.Forms.DataGridViewButtonColumn btnDelete;
        private System.Windows.Forms.Button btnAddHtml;
    }
}