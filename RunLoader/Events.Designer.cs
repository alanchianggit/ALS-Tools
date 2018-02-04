namespace RunLoader
{
    partial class frm_Event
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
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Time = new System.Windows.Forms.TextBox();
            this.Label_EventID = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Label_ProductionID = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Add.Location = new System.Drawing.Point(225, 336);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(111, 46);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // txt_Details
            // 
            this.txt_Details.Location = new System.Drawing.Point(12, 68);
            this.txt_Details.Multiline = true;
            this.txt_Details.Name = "txt_Details";
            this.txt_Details.Size = new System.Drawing.Size(371, 262);
            this.txt_Details.TabIndex = 2;
            this.txt_Details.TextChanged += new System.EventHandler(this.txt_Details_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Log";
            // 
            // cmb_Log
            // 
            this.cmb_Log.FormattingEnabled = true;
            this.cmb_Log.Location = new System.Drawing.Point(49, 38);
            this.cmb_Log.Name = "cmb_Log";
            this.cmb_Log.Size = new System.Drawing.Size(121, 21);
            this.cmb_Log.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Time";
            // 
            // txt_Time
            // 
            this.txt_Time.Location = new System.Drawing.Point(49, 12);
            this.txt_Time.Name = "txt_Time";
            this.txt_Time.Size = new System.Drawing.Size(121, 20);
            this.txt_Time.TabIndex = 6;
            this.txt_Time.TextChanged += new System.EventHandler(this.txt_Time_TextChanged);
            // 
            // Label_EventID
            // 
            this.Label_EventID.AutoSize = true;
            this.Label_EventID.Location = new System.Drawing.Point(184, 41);
            this.Label_EventID.Name = "Label_EventID";
            this.Label_EventID.Size = new System.Drawing.Size(49, 13);
            this.Label_EventID.TabIndex = 4;
            this.Label_EventID.Text = "Event ID";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(262, 37);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // Label_ProductionID
            // 
            this.Label_ProductionID.AutoSize = true;
            this.Label_ProductionID.Location = new System.Drawing.Point(184, 14);
            this.Label_ProductionID.Name = "Label_ProductionID";
            this.Label_ProductionID.Size = new System.Drawing.Size(72, 13);
            this.Label_ProductionID.TabIndex = 4;
            this.Label_ProductionID.Text = "Production ID";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(262, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.txt_Time_TextChanged);
            // 
            // frm_Event
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 391);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txt_Time);
            this.Controls.Add(this.Label_ProductionID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Label_EventID);
            this.Controls.Add(this.cmb_Log);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Details);
            this.Controls.Add(this.btn_Add);
            this.Name = "frm_Event";
            this.Text = "Events";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.TextBox txt_Details;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_Log;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Time;
        private System.Windows.Forms.Label Label_EventID;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label Label_ProductionID;
        private System.Windows.Forms.TextBox textBox1;
    }
}