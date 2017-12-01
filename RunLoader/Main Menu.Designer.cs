namespace RunLoader
{
    partial class frm_MainMenu
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_AnalysisLoader = new System.Windows.Forms.Button();
            this.btn_FileManagement = new System.Windows.Forms.Button();
            this.btn_archiver = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btn_Operations = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btn_AnalysisLoader, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_FileManagement, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_archiver, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_Exit, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_Operations, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(190, 232);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_AnalysisLoader
            // 
            this.btn_AnalysisLoader.Location = new System.Drawing.Point(3, 48);
            this.btn_AnalysisLoader.Name = "btn_AnalysisLoader";
            this.btn_AnalysisLoader.Size = new System.Drawing.Size(184, 39);
            this.btn_AnalysisLoader.TabIndex = 0;
            this.btn_AnalysisLoader.Text = "Analysis Management";
            this.btn_AnalysisLoader.UseVisualStyleBackColor = true;
            this.btn_AnalysisLoader.Click += new System.EventHandler(this.btn_AnalysisLoader_Click);
            // 
            // btn_FileManagement
            // 
            this.btn_FileManagement.Location = new System.Drawing.Point(3, 3);
            this.btn_FileManagement.Name = "btn_FileManagement";
            this.btn_FileManagement.Size = new System.Drawing.Size(184, 39);
            this.btn_FileManagement.TabIndex = 0;
            this.btn_FileManagement.Text = "File Management";
            this.btn_FileManagement.UseVisualStyleBackColor = true;
            this.btn_FileManagement.Click += new System.EventHandler(this.btn_FileManagement_Click);
            // 
            // btn_archiver
            // 
            this.btn_archiver.Location = new System.Drawing.Point(3, 93);
            this.btn_archiver.Name = "btn_archiver";
            this.btn_archiver.Size = new System.Drawing.Size(184, 39);
            this.btn_archiver.TabIndex = 1;
            this.btn_archiver.Text = "Archiver";
            this.btn_archiver.UseVisualStyleBackColor = true;
            this.btn_archiver.Click += new System.EventHandler(this.btn_archiver_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(3, 184);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(184, 40);
            this.btn_Exit.TabIndex = 0;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_Operations
            // 
            this.btn_Operations.Location = new System.Drawing.Point(3, 138);
            this.btn_Operations.Name = "btn_Operations";
            this.btn_Operations.Size = new System.Drawing.Size(184, 40);
            this.btn_Operations.TabIndex = 2;
            this.btn_Operations.Text = "Operations";
            this.btn_Operations.UseVisualStyleBackColor = true;
            this.btn_Operations.Click += new System.EventHandler(this.btn_Operations_Click);
            // 
            // frm_MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 239);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_MainMenu";
            this.Text = "Main menu";
            this.TopMost = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_AnalysisLoader;
        private System.Windows.Forms.Button btn_FileManagement;
        private System.Windows.Forms.Button btn_archiver;
        private System.Windows.Forms.Button btn_Operations;
        private System.Windows.Forms.Button btn_Exit;
    }
}