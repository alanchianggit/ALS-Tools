namespace RunLoader
{
    partial class Events
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
            this.btn_Add = new System.Windows.Forms.Button();
            this.txt_Details = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Log = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(229, 307);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(111, 46);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Text = "Add Event";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // txt_Details
            // 
            this.txt_Details.Location = new System.Drawing.Point(16, 39);
            this.txt_Details.Multiline = true;
            this.txt_Details.Name = "txt_Details";
            this.txt_Details.Size = new System.Drawing.Size(324, 262);
            this.txt_Details.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Log";
            // 
            // cmb_Log
            // 
            this.cmb_Log.FormattingEnabled = true;
            this.cmb_Log.Location = new System.Drawing.Point(219, 12);
            this.cmb_Log.Name = "cmb_Log";
            this.cmb_Log.Size = new System.Drawing.Size(121, 21);
            this.cmb_Log.TabIndex = 5;
            // 
            // Events
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 366);
            this.Controls.Add(this.cmb_Log);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Details);
            this.Controls.Add(this.btn_Add);
            this.Name = "Events";
            this.Text = "Events";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.TextBox txt_Details;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Log;
    }
}