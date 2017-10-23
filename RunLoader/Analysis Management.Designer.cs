namespace RunLoader
{
    partial class Analysis_Management
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
            this.cmb_RunNum = new System.Windows.Forms.ComboBox();
            this.txt_Directory = new System.Windows.Forms.TextBox();
            this.Label_Directory = new System.Windows.Forms.Label();
            this.label_RunNumber = new System.Windows.Forms.Label();
            this.cmd_LoadRun = new System.Windows.Forms.Button();
            this.btn_LoadDirectory = new System.Windows.Forms.Button();
            this.btn_LoadRun = new System.Windows.Forms.Button();
            this.cmb_Method = new System.Windows.Forms.ComboBox();
            this.label_Method = new System.Windows.Forms.Label();
            this.txt_FileLocation = new System.Windows.Forms.TextBox();
            this.txt_Output = new System.Windows.Forms.TextBox();
            this.Label_Output = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmb_RunNum
            // 
            this.cmb_RunNum.FormattingEnabled = true;
            this.cmb_RunNum.Location = new System.Drawing.Point(12, 104);
            this.cmb_RunNum.Name = "cmb_RunNum";
            this.cmb_RunNum.Size = new System.Drawing.Size(122, 21);
            this.cmb_RunNum.TabIndex = 0;
            // 
            // txt_Directory
            // 
            this.txt_Directory.Location = new System.Drawing.Point(15, 65);
            this.txt_Directory.Name = "txt_Directory";
            this.txt_Directory.Size = new System.Drawing.Size(276, 20);
            this.txt_Directory.TabIndex = 1;
            this.txt_Directory.Text = "C:\\Agilent\\ICPMH\\1\\Sequence\\";
            // 
            // Label_Directory
            // 
            this.Label_Directory.AutoSize = true;
            this.Label_Directory.Location = new System.Drawing.Point(15, 49);
            this.Label_Directory.Name = "Label_Directory";
            this.Label_Directory.Size = new System.Drawing.Size(52, 13);
            this.Label_Directory.TabIndex = 2;
            this.Label_Directory.Text = "Directory:";
            // 
            // label_RunNumber
            // 
            this.label_RunNumber.AutoSize = true;
            this.label_RunNumber.Location = new System.Drawing.Point(12, 88);
            this.label_RunNumber.Name = "label_RunNumber";
            this.label_RunNumber.Size = new System.Drawing.Size(70, 13);
            this.label_RunNumber.TabIndex = 2;
            this.label_RunNumber.Text = "Run Number:";
            // 
            // cmd_LoadRun
            // 
            this.cmd_LoadRun.Location = new System.Drawing.Point(388, 170);
            this.cmd_LoadRun.Name = "cmd_LoadRun";
            this.cmd_LoadRun.Size = new System.Drawing.Size(70, 20);
            this.cmd_LoadRun.TabIndex = 0;
            this.cmd_LoadRun.Text = "Load Run";
            this.cmd_LoadRun.Click += new System.EventHandler(this.cmd_LoadRun_Click);
            // 
            // btn_LoadDirectory
            // 
            this.btn_LoadDirectory.Location = new System.Drawing.Point(298, 65);
            this.btn_LoadDirectory.Name = "btn_LoadDirectory";
            this.btn_LoadDirectory.Size = new System.Drawing.Size(31, 20);
            this.btn_LoadDirectory.TabIndex = 3;
            this.btn_LoadDirectory.Text = "...";
            this.btn_LoadDirectory.UseVisualStyleBackColor = true;
            // 
            // btn_LoadRun
            // 
            this.btn_LoadRun.Location = new System.Drawing.Point(140, 105);
            this.btn_LoadRun.Name = "btn_LoadRun";
            this.btn_LoadRun.Size = new System.Drawing.Size(31, 20);
            this.btn_LoadRun.TabIndex = 3;
            this.btn_LoadRun.Text = "...";
            this.btn_LoadRun.UseVisualStyleBackColor = true;
            // 
            // cmb_Method
            // 
            this.cmb_Method.FormattingEnabled = true;
            this.cmb_Method.Location = new System.Drawing.Point(177, 103);
            this.cmb_Method.Name = "cmb_Method";
            this.cmb_Method.Size = new System.Drawing.Size(145, 21);
            this.cmb_Method.TabIndex = 4;
            // 
            // label_Method
            // 
            this.label_Method.AutoSize = true;
            this.label_Method.Location = new System.Drawing.Point(174, 87);
            this.label_Method.Name = "label_Method";
            this.label_Method.Size = new System.Drawing.Size(43, 13);
            this.label_Method.TabIndex = 2;
            this.label_Method.Text = "Method";
            // 
            // txt_FileLocation
            // 
            this.txt_FileLocation.Enabled = false;
            this.txt_FileLocation.Location = new System.Drawing.Point(12, 26);
            this.txt_FileLocation.Name = "txt_FileLocation";
            this.txt_FileLocation.Size = new System.Drawing.Size(276, 20);
            this.txt_FileLocation.TabIndex = 1;
            this.txt_FileLocation.Text = "C:\\users\\alan\\documents\\Backend1.accdb";
            // 
            // txt_Output
            // 
            this.txt_Output.Enabled = false;
            this.txt_Output.Location = new System.Drawing.Point(12, 144);
            this.txt_Output.Name = "txt_Output";
            this.txt_Output.Size = new System.Drawing.Size(100, 20);
            this.txt_Output.TabIndex = 5;
            this.txt_Output.Text = "D:\\Data";
            // 
            // Label_Output
            // 
            this.Label_Output.AutoSize = true;
            this.Label_Output.Location = new System.Drawing.Point(9, 128);
            this.Label_Output.Name = "Label_Output";
            this.Label_Output.Size = new System.Drawing.Size(39, 13);
            this.Label_Output.TabIndex = 2;
            this.Label_Output.Text = "Output";
            // 
            // Analysis_Management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 202);
            this.Controls.Add(this.txt_Output);
            this.Controls.Add(this.cmb_Method);
            this.Controls.Add(this.btn_LoadRun);
            this.Controls.Add(this.btn_LoadDirectory);
            this.Controls.Add(this.label_Method);
            this.Controls.Add(this.Label_Output);
            this.Controls.Add(this.label_RunNumber);
            this.Controls.Add(this.Label_Directory);
            this.Controls.Add(this.txt_FileLocation);
            this.Controls.Add(this.txt_Directory);
            this.Controls.Add(this.cmb_RunNum);
            this.Controls.Add(this.cmd_LoadRun);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Analysis_Management";
            this.Text = "Analysis Management";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_RunNum;
        private System.Windows.Forms.TextBox txt_Directory;
        private System.Windows.Forms.Label Label_Directory;
        private System.Windows.Forms.Label label_RunNumber;
        private System.Windows.Forms.Button cmd_LoadRun;
        private System.Windows.Forms.Button btn_LoadDirectory;
        private System.Windows.Forms.Button btn_LoadRun;
        private System.Windows.Forms.ComboBox cmb_Method;
        private System.Windows.Forms.Label label_Method;
        private System.Windows.Forms.TextBox txt_FileLocation;
        private System.Windows.Forms.TextBox txt_Output;
        private System.Windows.Forms.Label Label_Output;
    }
}
