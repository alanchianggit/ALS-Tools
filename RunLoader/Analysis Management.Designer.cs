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
            this.label_RunNumber = new System.Windows.Forms.Label();
            this.cmd_LoadRun = new System.Windows.Forms.Button();
            this.btn_LoadRun = new System.Windows.Forms.Button();
            this.cmb_Method = new System.Windows.Forms.ComboBox();
            this.label_Method = new System.Windows.Forms.Label();
            this.txt_FileLocation = new System.Windows.Forms.TextBox();
            this.txt_Output = new System.Windows.Forms.TextBox();
            this.Label_Output = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_SaveChanges = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.cmd_LoadRun.Location = new System.Drawing.Point(181, 149);
            this.cmd_LoadRun.Name = "cmd_LoadRun";
            this.cmd_LoadRun.Size = new System.Drawing.Size(110, 68);
            this.cmd_LoadRun.TabIndex = 0;
            this.cmd_LoadRun.Text = "Load Run";
            this.cmd_LoadRun.Click += new System.EventHandler(this.cmd_LoadRun_Click);
            // 
            // btn_LoadRun
            // 
            this.btn_LoadRun.Location = new System.Drawing.Point(140, 105);
            this.btn_LoadRun.Name = "btn_LoadRun";
            this.btn_LoadRun.Size = new System.Drawing.Size(31, 20);
            this.btn_LoadRun.TabIndex = 3;
            this.btn_LoadRun.Text = "...";
            this.btn_LoadRun.UseVisualStyleBackColor = true;
            this.btn_LoadRun.Click += new System.EventHandler(this.btn_LoadRun_Click);
            // 
            // cmb_Method
            // 
            this.cmb_Method.FormattingEnabled = true;
            this.cmb_Method.Location = new System.Drawing.Point(12, 144);
            this.cmb_Method.Name = "cmb_Method";
            this.cmb_Method.Size = new System.Drawing.Size(145, 21);
            this.cmb_Method.TabIndex = 4;
            // 
            // label_Method
            // 
            this.label_Method.AutoSize = true;
            this.label_Method.Location = new System.Drawing.Point(9, 128);
            this.label_Method.Name = "label_Method";
            this.label_Method.Size = new System.Drawing.Size(43, 13);
            this.label_Method.TabIndex = 2;
            this.label_Method.Text = "Method";
            // 
            // txt_FileLocation
            // 
            this.txt_FileLocation.Location = new System.Drawing.Point(12, 26);
            this.txt_FileLocation.Name = "txt_FileLocation";
            this.txt_FileLocation.Size = new System.Drawing.Size(276, 20);
            this.txt_FileLocation.TabIndex = 1;
            this.txt_FileLocation.Text = @"\\alvncws008\groups\minerals\spectroscopy\icp-ms\ms logs\BackEnd2.accdb";
            // 
            // txt_Output
            // 
            this.txt_Output.Location = new System.Drawing.Point(15, 193);
            this.txt_Output.Name = "txt_Output";
            this.txt_Output.Size = new System.Drawing.Size(100, 20);
            this.txt_Output.TabIndex = 5;
            this.txt_Output.Text = "D:\\Data";
            // 
            // Label_Output
            // 
            this.Label_Output.AutoSize = true;
            this.Label_Output.Location = new System.Drawing.Point(12, 177);
            this.Label_Output.Name = "Label_Output";
            this.Label_Output.Size = new System.Drawing.Size(39, 13);
            this.Label_Output.TabIndex = 2;
            this.Label_Output.Text = "Output";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(390, 48);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(717, 383);
            this.dataGridView1.TabIndex = 6;
            // 
            // btn_SaveChanges
            // 
            this.btn_SaveChanges.Location = new System.Drawing.Point(181, 233);
            this.btn_SaveChanges.Name = "btn_SaveChanges";
            this.btn_SaveChanges.Size = new System.Drawing.Size(153, 97);
            this.btn_SaveChanges.TabIndex = 7;
            this.btn_SaveChanges.Text = "Save Changes";
            this.btn_SaveChanges.UseVisualStyleBackColor = true;
            this.btn_SaveChanges.Click += new System.EventHandler(this.btn_SaveChanges_Click);
            // 
            // Analysis_Management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1305, 549);
            this.Controls.Add(this.btn_SaveChanges);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txt_Output);
            this.Controls.Add(this.cmb_Method);
            this.Controls.Add(this.btn_LoadRun);
            this.Controls.Add(this.label_Method);
            this.Controls.Add(this.Label_Output);
            this.Controls.Add(this.label_RunNumber);
            this.Controls.Add(this.txt_FileLocation);
            this.Controls.Add(this.cmb_RunNum);
            this.Controls.Add(this.cmd_LoadRun);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Analysis_Management";
            this.Text = "Analysis Management";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_RunNum;
        private System.Windows.Forms.Label label_RunNumber;
        private System.Windows.Forms.Button cmd_LoadRun;
        private System.Windows.Forms.Button btn_LoadRun;
        private System.Windows.Forms.ComboBox cmb_Method;
        private System.Windows.Forms.Label label_Method;
        private System.Windows.Forms.TextBox txt_FileLocation;
        private System.Windows.Forms.TextBox txt_Output;
        private System.Windows.Forms.Label Label_Output;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_SaveChanges;
    }
}


