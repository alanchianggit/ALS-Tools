namespace RunLoader
{
    partial class FileAccessForm
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
            this.btn_ConnectDB = new System.Windows.Forms.Button();
            this.btn_BrowseAccess = new System.Windows.Forms.Button();
            this.btn_SelectOutput = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txt_FileLocation = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_status = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ConnectDB
            // 
            this.btn_ConnectDB.Location = new System.Drawing.Point(365, 29);
            this.btn_ConnectDB.Name = "btn_ConnectDB";
            this.btn_ConnectDB.Size = new System.Drawing.Size(75, 20);
            this.btn_ConnectDB.TabIndex = 1;
            this.btn_ConnectDB.Text = "Connect";
            this.btn_ConnectDB.UseVisualStyleBackColor = true;
            this.btn_ConnectDB.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_BrowseAccess
            // 
            this.btn_BrowseAccess.Location = new System.Drawing.Point(328, 29);
            this.btn_BrowseAccess.Name = "btn_BrowseAccess";
            this.btn_BrowseAccess.Size = new System.Drawing.Size(31, 20);
            this.btn_BrowseAccess.TabIndex = 1;
            this.btn_BrowseAccess.Text = "...";
            this.btn_BrowseAccess.UseVisualStyleBackColor = true;
            this.btn_BrowseAccess.Click += new System.EventHandler(this.btn_BrowseAccess_Click);
            // 
            // btn_SelectOutput
            // 
            this.btn_SelectOutput.Location = new System.Drawing.Point(342, 346);
            this.btn_SelectOutput.Name = "btn_SelectOutput";
            this.btn_SelectOutput.Size = new System.Drawing.Size(98, 20);
            this.btn_SelectOutput.TabIndex = 1;
            this.btn_SelectOutput.Text = "Select Output";
            this.btn_SelectOutput.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(632, 388);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "button1";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txt_FileLocation
            // 
            this.txt_FileLocation.Location = new System.Drawing.Point(19, 29);
            this.txt_FileLocation.Name = "txt_FileLocation";
            this.txt_FileLocation.Size = new System.Drawing.Size(300, 20);
            this.txt_FileLocation.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(19, 56);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(719, 271);
            this.dataGridView1.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(19, 346);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(300, 20);
            this.textBox1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Database (Access (*.accdb, *.db, *.mdb):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 330);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Download Folder:";
            // 
            // txt_status
            // 
            this.txt_status.Location = new System.Drawing.Point(758, 26);
            this.txt_status.Multiline = true;
            this.txt_status.Name = "txt_status";
            this.txt_status.Size = new System.Drawing.Size(239, 360);
            this.txt_status.TabIndex = 7;
            // 
            // FileAccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 433);
            this.Controls.Add(this.txt_status);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txt_FileLocation);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btn_SelectOutput);
            this.Controls.Add(this.btn_BrowseAccess);
            this.Controls.Add(this.btn_ConnectDB);
            this.Name = "FileAccessForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_ConnectDB;
        private System.Windows.Forms.Button btn_BrowseAccess;
        private System.Windows.Forms.Button btn_SelectOutput;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txt_FileLocation;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_status;
    }
}

