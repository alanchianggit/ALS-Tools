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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txt_FileLocation = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ConnectDB
            // 
            this.btn_ConnectDB.Location = new System.Drawing.Point(355, 10);
            this.btn_ConnectDB.Name = "btn_ConnectDB";
            this.btn_ConnectDB.Size = new System.Drawing.Size(75, 23);
            this.btn_ConnectDB.TabIndex = 1;
            this.btn_ConnectDB.Text = "Connect";
            this.btn_ConnectDB.UseVisualStyleBackColor = true;
            this.btn_ConnectDB.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_BrowseAccess
            // 
            this.btn_BrowseAccess.Location = new System.Drawing.Point(318, 10);
            this.btn_BrowseAccess.Name = "btn_BrowseAccess";
            this.btn_BrowseAccess.Size = new System.Drawing.Size(31, 23);
            this.btn_BrowseAccess.TabIndex = 1;
            this.btn_BrowseAccess.Text = "...";
            this.btn_BrowseAccess.UseVisualStyleBackColor = true;
            this.btn_BrowseAccess.Click += new System.EventHandler(this.btn_BrowseAccess_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(398, 393);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "button1";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(479, 393);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "button1";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // txt_FileLocation
            // 
            this.txt_FileLocation.Location = new System.Drawing.Point(12, 11);
            this.txt_FileLocation.Name = "txt_FileLocation";
            this.txt_FileLocation.Size = new System.Drawing.Size(300, 20);
            this.txt_FileLocation.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(719, 271);
            this.dataGridView1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 423);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txt_FileLocation);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btn_BrowseAccess);
            this.Controls.Add(this.btn_ConnectDB);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_ConnectDB;
        private System.Windows.Forms.Button btn_BrowseAccess;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txt_FileLocation;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

