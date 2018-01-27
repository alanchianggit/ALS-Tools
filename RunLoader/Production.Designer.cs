namespace RunLoader
{
    partial class Production
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
            this.Label_ProductionID = new System.Windows.Forms.Label();
            this.cmb_ProductionID = new System.Windows.Forms.ComboBox();
            this.Label_EqpName = new System.Windows.Forms.Label();
            this.cmb_EqpName = new System.Windows.Forms.ComboBox();
            this.Label_Type = new System.Windows.Forms.Label();
            this.cmb_Type = new System.Windows.Forms.ComboBox();
            this.Label_StartTime = new System.Windows.Forms.Label();
            this.dateTime_Start = new System.Windows.Forms.DateTimePicker();
            this.Label_Starter = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Label_Quantity = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Label_EqpFilter = new System.Windows.Forms.Label();
            this.cmb_EqpFilter = new System.Windows.Forms.ComboBox();
            this.Label_EndTime = new System.Windows.Forms.Label();
            this.Label_Ender = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dateTime_End = new System.Windows.Forms.DateTimePicker();
            this.btn_Create = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Discard = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_ProductionID
            // 
            this.Label_ProductionID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_ProductionID.AutoSize = true;
            this.Label_ProductionID.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_ProductionID.Location = new System.Drawing.Point(3, 34);
            this.Label_ProductionID.Name = "Label_ProductionID";
            this.Label_ProductionID.Size = new System.Drawing.Size(72, 13);
            this.Label_ProductionID.TabIndex = 0;
            this.Label_ProductionID.Text = "Production ID";
            // 
            // cmb_ProductionID
            // 
            this.cmb_ProductionID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_ProductionID.FormattingEnabled = true;
            this.cmb_ProductionID.Location = new System.Drawing.Point(91, 30);
            this.cmb_ProductionID.Name = "cmb_ProductionID";
            this.cmb_ProductionID.Size = new System.Drawing.Size(200, 21);
            this.cmb_ProductionID.TabIndex = 2;
            // 
            // Label_EqpName
            // 
            this.Label_EqpName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_EqpName.AutoSize = true;
            this.Label_EqpName.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_EqpName.Location = new System.Drawing.Point(297, 34);
            this.Label_EqpName.Name = "Label_EqpName";
            this.Label_EqpName.Size = new System.Drawing.Size(88, 13);
            this.Label_EqpName.TabIndex = 0;
            this.Label_EqpName.Text = "Equipment Name";
            // 
            // cmb_EqpName
            // 
            this.cmb_EqpName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_EqpName.FormattingEnabled = true;
            this.cmb_EqpName.Location = new System.Drawing.Point(391, 30);
            this.cmb_EqpName.Name = "cmb_EqpName";
            this.cmb_EqpName.Size = new System.Drawing.Size(205, 21);
            this.cmb_EqpName.TabIndex = 2;
            // 
            // Label_Type
            // 
            this.Label_Type.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_Type.AutoSize = true;
            this.Label_Type.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_Type.Location = new System.Drawing.Point(3, 61);
            this.Label_Type.Name = "Label_Type";
            this.Label_Type.Size = new System.Drawing.Size(31, 13);
            this.Label_Type.TabIndex = 0;
            this.Label_Type.Text = "Type";
            // 
            // cmb_Type
            // 
            this.cmb_Type.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_Type.FormattingEnabled = true;
            this.cmb_Type.Location = new System.Drawing.Point(91, 57);
            this.cmb_Type.Name = "cmb_Type";
            this.cmb_Type.Size = new System.Drawing.Size(200, 21);
            this.cmb_Type.TabIndex = 2;
            // 
            // Label_StartTime
            // 
            this.Label_StartTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_StartTime.AutoSize = true;
            this.Label_StartTime.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_StartTime.Location = new System.Drawing.Point(297, 87);
            this.Label_StartTime.Name = "Label_StartTime";
            this.Label_StartTime.Size = new System.Drawing.Size(55, 13);
            this.Label_StartTime.TabIndex = 0;
            this.Label_StartTime.Text = "Start Time";
            // 
            // dateTime_Start
            // 
            this.dateTime_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTime_Start.CustomFormat = "yyyy-MM-dd hh:mm:ss tt";
            this.dateTime_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTime_Start.Location = new System.Drawing.Point(391, 84);
            this.dateTime_Start.Name = "dateTime_Start";
            this.dateTime_Start.Size = new System.Drawing.Size(205, 20);
            this.dateTime_Start.TabIndex = 3;
            // 
            // Label_Starter
            // 
            this.Label_Starter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_Starter.AutoSize = true;
            this.Label_Starter.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_Starter.Location = new System.Drawing.Point(3, 87);
            this.Label_Starter.Name = "Label_Starter";
            this.Label_Starter.Size = new System.Drawing.Size(66, 13);
            this.Label_Starter.TabIndex = 0;
            this.Label_Starter.Text = "Start Analyst";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(91, 84);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 20);
            this.textBox1.TabIndex = 4;
            // 
            // Label_Quantity
            // 
            this.Label_Quantity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_Quantity.AutoSize = true;
            this.Label_Quantity.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_Quantity.Location = new System.Drawing.Point(297, 61);
            this.Label_Quantity.Name = "Label_Quantity";
            this.Label_Quantity.Size = new System.Drawing.Size(46, 13);
            this.Label_Quantity.TabIndex = 0;
            this.Label_Quantity.Text = "Quantity";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(391, 57);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(205, 20);
            this.textBox3.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.dateTime_End, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.dateTime_Start, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmb_EqpFilter, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.Label_ProductionID, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmb_ProductionID, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label_Ender, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cmb_Type, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Label_Type, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Label_Starter, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Label_StartTime, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.Label_EndTime, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.Label_EqpFilter, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Label_Quantity, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox3, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.Label_EqpName, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmb_EqpName, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(599, 137);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // Label_EqpFilter
            // 
            this.Label_EqpFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_EqpFilter.AutoSize = true;
            this.Label_EqpFilter.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_EqpFilter.Location = new System.Drawing.Point(3, 7);
            this.Label_EqpFilter.Name = "Label_EqpFilter";
            this.Label_EqpFilter.Size = new System.Drawing.Size(82, 13);
            this.Label_EqpFilter.TabIndex = 0;
            this.Label_EqpFilter.Text = "Equipment Filter";
            // 
            // cmb_EqpFilter
            // 
            this.cmb_EqpFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_EqpFilter.FormattingEnabled = true;
            this.cmb_EqpFilter.Location = new System.Drawing.Point(91, 3);
            this.cmb_EqpFilter.Name = "cmb_EqpFilter";
            this.cmb_EqpFilter.Size = new System.Drawing.Size(200, 21);
            this.cmb_EqpFilter.TabIndex = 2;
            // 
            // Label_EndTime
            // 
            this.Label_EndTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_EndTime.AutoSize = true;
            this.Label_EndTime.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_EndTime.Location = new System.Drawing.Point(297, 115);
            this.Label_EndTime.Name = "Label_EndTime";
            this.Label_EndTime.Size = new System.Drawing.Size(52, 13);
            this.Label_EndTime.TabIndex = 0;
            this.Label_EndTime.Text = "End Time";
            // 
            // Label_Ender
            // 
            this.Label_Ender.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_Ender.AutoSize = true;
            this.Label_Ender.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_Ender.Location = new System.Drawing.Point(3, 115);
            this.Label_Ender.Name = "Label_Ender";
            this.Label_Ender.Size = new System.Drawing.Size(63, 13);
            this.Label_Ender.TabIndex = 0;
            this.Label_Ender.Text = "End Analyst";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(91, 112);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 20);
            this.textBox2.TabIndex = 4;
            // 
            // dateTime_End
            // 
            this.dateTime_End.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTime_End.CustomFormat = "yyyy-MM-dd hh:mm:ss tt";
            this.dateTime_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTime_End.Location = new System.Drawing.Point(391, 112);
            this.dateTime_End.Name = "dateTime_End";
            this.dateTime_End.Size = new System.Drawing.Size(205, 20);
            this.dateTime_End.TabIndex = 3;
            // 
            // btn_Create
            // 
            this.btn_Create.Location = new System.Drawing.Point(110, 164);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(142, 51);
            this.btn_Create.TabIndex = 6;
            this.btn_Create.Text = "Create";
            this.btn_Create.UseVisualStyleBackColor = true;
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(255, 164);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(142, 51);
            this.btn_Update.TabIndex = 6;
            this.btn_Update.Text = "Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            // 
            // btn_Discard
            // 
            this.btn_Discard.Location = new System.Drawing.Point(400, 164);
            this.btn_Discard.Name = "btn_Discard";
            this.btn_Discard.Size = new System.Drawing.Size(142, 51);
            this.btn_Discard.TabIndex = 6;
            this.btn_Discard.Text = "Discard";
            this.btn_Discard.UseVisualStyleBackColor = true;
            // 
            // Production
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1239, 231);
            this.Controls.Add(this.btn_Discard);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.btn_Create);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Production";
            this.Text = "Production";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label Label_ProductionID;
        private System.Windows.Forms.ComboBox cmb_ProductionID;
        private System.Windows.Forms.Label Label_EqpName;
        private System.Windows.Forms.ComboBox cmb_EqpName;
        private System.Windows.Forms.Label Label_Type;
        private System.Windows.Forms.ComboBox cmb_Type;
        private System.Windows.Forms.Label Label_StartTime;
        private System.Windows.Forms.DateTimePicker dateTime_Start;
        private System.Windows.Forms.Label Label_Starter;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Label_Quantity;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmb_EqpFilter;
        private System.Windows.Forms.Label Label_EqpFilter;
        private System.Windows.Forms.DateTimePicker dateTime_End;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label Label_Ender;
        private System.Windows.Forms.Label Label_EndTime;
        private System.Windows.Forms.Button btn_Create;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Discard;
    }
}