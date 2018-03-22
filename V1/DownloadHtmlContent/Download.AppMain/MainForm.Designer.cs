using Download.AppMain.Extensions;

namespace Download.AppMain
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.btnMoveLocation = new System.Windows.Forms.Button();
            this.btnCreatePage = new System.Windows.Forms.Button();
            this.btnClearData = new System.Windows.Forms.Button();
            this.btnAboutMe = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dgvPageList = new System.Windows.Forms.DataGridView();
            this.PageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPageStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.fbdMoveLocation = new System.Windows.Forms.FolderBrowserDialog();
            this.pbProcess = new Download.AppMain.Extensions.TextProgressBar();
            this.btnOption = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPageList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(507, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(89, 64);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Process:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Location:";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(71, 12);
            this.txtLocation.Multiline = true;
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(363, 24);
            this.txtLocation.TabIndex = 5;
            // 
            // btnMoveLocation
            // 
            this.btnMoveLocation.Location = new System.Drawing.Point(440, 12);
            this.btnMoveLocation.Name = "btnMoveLocation";
            this.btnMoveLocation.Size = new System.Drawing.Size(51, 24);
            this.btnMoveLocation.TabIndex = 6;
            this.btnMoveLocation.Text = "Move";
            this.btnMoveLocation.UseVisualStyleBackColor = true;
            // 
            // btnCreatePage
            // 
            this.btnCreatePage.Location = new System.Drawing.Point(507, 93);
            this.btnCreatePage.Name = "btnCreatePage";
            this.btnCreatePage.Size = new System.Drawing.Size(89, 64);
            this.btnCreatePage.TabIndex = 0;
            this.btnCreatePage.Text = "Create";
            this.btnCreatePage.UseVisualStyleBackColor = true;
            // 
            // btnClearData
            // 
            this.btnClearData.Location = new System.Drawing.Point(507, 160);
            this.btnClearData.Name = "btnClearData";
            this.btnClearData.Size = new System.Drawing.Size(89, 64);
            this.btnClearData.TabIndex = 0;
            this.btnClearData.Text = "Clear Data";
            this.btnClearData.UseVisualStyleBackColor = true;
            // 
            // btnAboutMe
            // 
            this.btnAboutMe.Location = new System.Drawing.Point(507, 300);
            this.btnAboutMe.Name = "btnAboutMe";
            this.btnAboutMe.Size = new System.Drawing.Size(89, 46);
            this.btnAboutMe.TabIndex = 0;
            this.btnAboutMe.Text = "About";
            this.btnAboutMe.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(507, 352);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(89, 48);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // dgvPageList
            // 
            this.dgvPageList.AllowUserToAddRows = false;
            this.dgvPageList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPageList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PageName,
            this.TotalLink,
            this.txtPageStatus,
            this.btnDelete});
            this.dgvPageList.Location = new System.Drawing.Point(16, 93);
            this.dgvPageList.Name = "dgvPageList";
            this.dgvPageList.Size = new System.Drawing.Size(475, 307);
            this.dgvPageList.TabIndex = 7;
            // 
            // PageName
            // 
            this.PageName.DataPropertyName = "PageName";
            this.PageName.Frozen = true;
            this.PageName.HeaderText = "Page Name";
            this.PageName.Name = "PageName";
            this.PageName.Width = 150;
            // 
            // TotalLink
            // 
            this.TotalLink.DataPropertyName = "TotalLink";
            this.TotalLink.FillWeight = 80F;
            this.TotalLink.HeaderText = "TotalLink";
            this.TotalLink.Name = "TotalLink";
            this.TotalLink.ReadOnly = true;
            this.TotalLink.Width = 80;
            // 
            // txtPageStatus
            // 
            this.txtPageStatus.DataPropertyName = "PageStatus";
            this.txtPageStatus.HeaderText = "Page Status";
            this.txtPageStatus.Name = "txtPageStatus";
            this.txtPageStatus.ReadOnly = true;
            // 
            // btnDelete
            // 
            this.btnDelete.HeaderText = "Delete";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "Delete";
            this.btnDelete.ToolTipText = "Delete";
            this.btnDelete.UseColumnTextForButtonValue = true;
            // 
            // pbProcess
            // 
            this.pbProcess.Location = new System.Drawing.Point(71, 53);
            this.pbProcess.Name = "pbProcess";
            this.pbProcess.Size = new System.Drawing.Size(420, 23);
            this.pbProcess.TabIndex = 1;
            // 
            // btnOption
            // 
            this.btnOption.Location = new System.Drawing.Point(507, 230);
            this.btnOption.Name = "btnOption";
            this.btnOption.Size = new System.Drawing.Size(89, 64);
            this.btnOption.TabIndex = 0;
            this.btnOption.Text = "Option";
            this.btnOption.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(610, 415);
            this.Controls.Add(this.dgvPageList);
            this.Controls.Add(this.btnMoveLocation);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbProcess);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAboutMe);
            this.Controls.Add(this.btnOption);
            this.Controls.Add(this.btnClearData);
            this.Controls.Add(this.btnCreatePage);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Download Content Html ";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPageList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private TextProgressBar pbProcess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btnMoveLocation;
        private System.Windows.Forms.Button btnCreatePage;
        private System.Windows.Forms.Button btnClearData;
        private System.Windows.Forms.Button btnAboutMe;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dgvPageList;
        private System.Windows.Forms.FolderBrowserDialog fbdMoveLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn PageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtPageStatus;
        private System.Windows.Forms.DataGridViewButtonColumn btnDelete;
        private System.Windows.Forms.Button btnOption;
    }
}

