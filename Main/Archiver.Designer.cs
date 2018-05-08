namespace Archiver
{
    partial class ArchiverForm
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txt_status = new System.Windows.Forms.TextBox();
            this.Label_Exclusion = new System.Windows.Forms.Label();
            this.btn_SelectOutPutPath = new System.Windows.Forms.Button();
            this.Label_OutPut = new System.Windows.Forms.Label();
            this.txt_OutPutPath = new System.Windows.Forms.TextBox();
            this.treeview_Directories = new System.Windows.Forms.TreeView();
            this.btn_SelectDirectory = new System.Windows.Forms.Button();
            this.btn_Compress = new System.Windows.Forms.Button();
            this.chkbox_DeleteWhenComplete = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label_Progress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 480);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(739, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 5;
            // 
            // txt_status
            // 
            this.txt_status.Location = new System.Drawing.Point(12, 280);
            this.txt_status.Multiline = true;
            this.txt_status.Name = "txt_status";
            this.txt_status.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_status.Size = new System.Drawing.Size(739, 180);
            this.txt_status.TabIndex = 6;
            // 
            // Label_Exclusion
            // 
            this.Label_Exclusion.AutoSize = true;
            this.Label_Exclusion.Location = new System.Drawing.Point(12, 9);
            this.Label_Exclusion.Name = "Label_Exclusion";
            this.Label_Exclusion.Size = new System.Drawing.Size(185, 13);
            this.Label_Exclusion.TabIndex = 1;
            this.Label_Exclusion.Text = "Folders to Pack (de-select to exclude)";
            // 
            // btn_SelectOutPutPath
            // 
            this.btn_SelectOutPutPath.Location = new System.Drawing.Point(12, 234);
            this.btn_SelectOutPutPath.Name = "btn_SelectOutPutPath";
            this.btn_SelectOutPutPath.Size = new System.Drawing.Size(154, 40);
            this.btn_SelectOutPutPath.TabIndex = 0;
            this.btn_SelectOutPutPath.Text = "Select Output Directory";
            this.btn_SelectOutPutPath.UseVisualStyleBackColor = true;
            this.btn_SelectOutPutPath.Click += new System.EventHandler(this.btn_SelectOutPutPath_Click);
            // 
            // Label_OutPut
            // 
            this.Label_OutPut.AutoSize = true;
            this.Label_OutPut.Location = new System.Drawing.Point(172, 234);
            this.Label_OutPut.Name = "Label_OutPut";
            this.Label_OutPut.Size = new System.Drawing.Size(67, 13);
            this.Label_OutPut.TabIndex = 1;
            this.Label_OutPut.Text = "Output Path:";
            // 
            // txt_OutPutPath
            // 
            this.txt_OutPutPath.Enabled = false;
            this.txt_OutPutPath.Location = new System.Drawing.Point(172, 250);
            this.txt_OutPutPath.Name = "txt_OutPutPath";
            this.txt_OutPutPath.Size = new System.Drawing.Size(398, 20);
            this.txt_OutPutPath.TabIndex = 2;
            // 
            // treeview_Directories
            // 
            this.treeview_Directories.CheckBoxes = true;
            this.treeview_Directories.Location = new System.Drawing.Point(12, 25);
            this.treeview_Directories.Name = "treeview_Directories";
            this.treeview_Directories.Size = new System.Drawing.Size(558, 203);
            this.treeview_Directories.TabIndex = 10;
            this.treeview_Directories.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.node_AfterCheck);
            // 
            // btn_SelectDirectory
            // 
            this.btn_SelectDirectory.Location = new System.Drawing.Point(577, 25);
            this.btn_SelectDirectory.Name = "btn_SelectDirectory";
            this.btn_SelectDirectory.Size = new System.Drawing.Size(126, 30);
            this.btn_SelectDirectory.TabIndex = 0;
            this.btn_SelectDirectory.Text = "Add Directories";
            this.btn_SelectDirectory.UseVisualStyleBackColor = true;
            this.btn_SelectDirectory.Click += new System.EventHandler(this.btn_SelectDirectory_Click);
            // 
            // btn_Compress
            // 
            this.btn_Compress.Location = new System.Drawing.Point(576, 175);
            this.btn_Compress.Name = "btn_Compress";
            this.btn_Compress.Size = new System.Drawing.Size(159, 53);
            this.btn_Compress.TabIndex = 0;
            this.btn_Compress.Text = "Compress All folders in Selected Path";
            this.btn_Compress.UseVisualStyleBackColor = true;
            this.btn_Compress.Click += new System.EventHandler(this.btn_Compress_Click);
            // 
            // chkbox_DeleteWhenComplete
            // 
            this.chkbox_DeleteWhenComplete.AutoSize = true;
            this.chkbox_DeleteWhenComplete.Location = new System.Drawing.Point(577, 64);
            this.chkbox_DeleteWhenComplete.Name = "chkbox_DeleteWhenComplete";
            this.chkbox_DeleteWhenComplete.Size = new System.Drawing.Size(159, 17);
            this.chkbox_DeleteWhenComplete.TabIndex = 3;
            this.chkbox_DeleteWhenComplete.Text = "Delete Folder on Completion";
            this.chkbox_DeleteWhenComplete.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.AllowDrop = true;
            this.numericUpDown1.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(604, 111);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label_Progress
            // 
            this.label_Progress.AutoSize = true;
            this.label_Progress.Location = new System.Drawing.Point(369, 464);
            this.label_Progress.Name = "label_Progress";
            this.label_Progress.Size = new System.Drawing.Size(24, 13);
            this.label_Progress.TabIndex = 7;
            this.label_Progress.Text = "0/0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(576, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Skip folders greater than (KB):";
            // 
            // frm_dm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 509);
            this.Controls.Add(this.treeview_Directories);
            this.Controls.Add(this.label_Progress);
            this.Controls.Add(this.txt_status);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.chkbox_DeleteWhenComplete);
            this.Controls.Add(this.txt_OutPutPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Label_OutPut);
            this.Controls.Add(this.Label_Exclusion);
            this.Controls.Add(this.btn_Compress);
            this.Controls.Add(this.btn_SelectOutPutPath);
            this.Controls.Add(this.btn_SelectDirectory);
            this.MaximizeBox = false;
            this.Name = "frm_dm";
            this.Text = "Archiver";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txt_status;
        private System.Windows.Forms.Label Label_Exclusion;
        private System.Windows.Forms.Button btn_SelectOutPutPath;
        private System.Windows.Forms.Label Label_OutPut;
        private System.Windows.Forms.TextBox txt_OutPutPath;
        private System.Windows.Forms.TreeView treeview_Directories;
        private System.Windows.Forms.Button btn_SelectDirectory;
        private System.Windows.Forms.Button btn_Compress;
        private System.Windows.Forms.CheckBox chkbox_DeleteWhenComplete;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label_Progress;
        private System.Windows.Forms.Label label1;
    }
}

