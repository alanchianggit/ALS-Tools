namespace RunLoader
{
    partial class Signin
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
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.label_Name = new System.Windows.Forms.Label();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.label_Password = new System.Windows.Forms.Label();
            this.btn_SignIn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(78, 5);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(100, 20);
            this.txt_Name.TabIndex = 0;
            this.txt_Name.TextChanged += new System.EventHandler(this.txt_Name_TextChanged);
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(12, 9);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(35, 13);
            this.label_Name.TabIndex = 1;
            this.label_Name.Text = "Name";
            // 
            // txt_Password
            // 
            this.txt_Password.Location = new System.Drawing.Point(78, 31);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(100, 20);
            this.txt_Password.TabIndex = 1;
            this.txt_Password.UseSystemPasswordChar = true;
            this.txt_Password.TextChanged += new System.EventHandler(this.txt_Password_TextChanged);
            // 
            // label_Password
            // 
            this.label_Password.AutoSize = true;
            this.label_Password.Location = new System.Drawing.Point(12, 35);
            this.label_Password.Name = "label_Password";
            this.label_Password.Size = new System.Drawing.Size(53, 13);
            this.label_Password.TabIndex = 1;
            this.label_Password.Text = "Password";
            // 
            // btn_SignIn
            // 
            this.btn_SignIn.Location = new System.Drawing.Point(103, 66);
            this.btn_SignIn.Name = "btn_SignIn";
            this.btn_SignIn.Size = new System.Drawing.Size(75, 23);
            this.btn_SignIn.TabIndex = 2;
            this.btn_SignIn.Text = "Sign In";
            this.btn_SignIn.UseVisualStyleBackColor = true;
            this.btn_SignIn.Click += new System.EventHandler(this.btn_SignIn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 99);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(189, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(88, 17);
            this.toolStripStatusLabel1.Text = "Not Connected";
            // 
            // Signin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(189, 121);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_SignIn);
            this.Controls.Add(this.label_Password);
            this.Controls.Add(this.label_Name);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.txt_Name);
            this.Name = "Signin";
            this.Text = "Signin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Signin_FormClosing);
            this.Load += new System.EventHandler(this.Signin_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Label label_Password;
        private System.Windows.Forms.Button btn_SignIn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}