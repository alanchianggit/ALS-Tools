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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmb_RunNum
            // 
            this.cmb_RunNum.FormattingEnabled = true;
            this.cmb_RunNum.Location = new System.Drawing.Point(67, 101);
            this.cmb_RunNum.Name = "cmb_RunNum";
            this.cmb_RunNum.Size = new System.Drawing.Size(276, 21);
            this.cmb_RunNum.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(67, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(276, 20);
            this.textBox1.TabIndex = 1;
            // 
            // Analysis_Management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1453, 590);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cmb_RunNum);
            this.Name = "Analysis_Management";
            this.Text = "Analysis_Management";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_RunNum;
        private System.Windows.Forms.TextBox textBox1;
    }
}