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
            this.cmb_ProductionName = new System.Windows.Forms.ComboBox();
            this.Label_EqpName = new System.Windows.Forms.Label();
            this.cmb_EqpName = new System.Windows.Forms.ComboBox();
            this.Label_Type = new System.Windows.Forms.Label();
            this.cmb_Type = new System.Windows.Forms.ComboBox();
            this.Label_StartTime = new System.Windows.Forms.Label();
            this.Label_Starter = new System.Windows.Forms.Label();
            this.txt_Starter = new System.Windows.Forms.TextBox();
            this.Label_Quantity = new System.Windows.Forms.Label();
            this.txt_Quantity = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmb_EqpFilter = new System.Windows.Forms.ComboBox();
            this.txt_Ender = new System.Windows.Forms.TextBox();
            this.Label_Ender = new System.Windows.Forms.Label();
            this.Label_EndTime = new System.Windows.Forms.Label();
            this.Label_EqpFilter = new System.Windows.Forms.Label();
            this.Label_Method = new System.Windows.Forms.Label();
            this.cmb_Method = new System.Windows.Forms.ComboBox();
            this.txt_StartTime = new System.Windows.Forms.TextBox();
            this.txt_EndTime = new System.Windows.Forms.TextBox();
            this.btn_Create = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.listbox_Events = new System.Windows.Forms.ListBox();
            this.btn_AddEvent = new System.Windows.Forms.Button();
            this.btn_StartRun = new System.Windows.Forms.Button();
            this.btn_EndRun = new System.Windows.Forms.Button();
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
            // cmb_ProductionName
            // 
            this.cmb_ProductionName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_ProductionName.FormattingEnabled = true;
            this.cmb_ProductionName.Location = new System.Drawing.Point(91, 30);
            this.cmb_ProductionName.Name = "cmb_ProductionName";
            this.cmb_ProductionName.Size = new System.Drawing.Size(200, 21);
            this.cmb_ProductionName.TabIndex = 0;
            this.cmb_ProductionName.TextChanged += new System.EventHandler(this.ControlTextChanged);
            this.cmb_ProductionName.Leave += new System.EventHandler(this.cmb_ProductionName_Leave);
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
            this.cmb_EqpName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_EqpName.FormattingEnabled = true;
            this.cmb_EqpName.Location = new System.Drawing.Point(391, 30);
            this.cmb_EqpName.Name = "cmb_EqpName";
            this.cmb_EqpName.Size = new System.Drawing.Size(205, 21);
            this.cmb_EqpName.TabIndex = 5;
            this.cmb_EqpName.TextChanged += new System.EventHandler(this.ControlTextChanged);
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
            this.cmb_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Type.FormattingEnabled = true;
            this.cmb_Type.Items.AddRange(new object[] {
            "Online",
            "Offline"});
            this.cmb_Type.Location = new System.Drawing.Point(91, 57);
            this.cmb_Type.Name = "cmb_Type";
            this.cmb_Type.Size = new System.Drawing.Size(200, 21);
            this.cmb_Type.TabIndex = 1;
            this.cmb_Type.TextChanged += new System.EventHandler(this.ControlTextChanged);
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
            // txt_Starter
            // 
            this.txt_Starter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Starter.Location = new System.Drawing.Point(91, 84);
            this.txt_Starter.Name = "txt_Starter";
            this.txt_Starter.Size = new System.Drawing.Size(200, 20);
            this.txt_Starter.TabIndex = 2;
            this.txt_Starter.TextChanged += new System.EventHandler(this.ControlTextChanged);
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
            // txt_Quantity
            // 
            this.txt_Quantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Quantity.Location = new System.Drawing.Point(391, 57);
            this.txt_Quantity.Name = "txt_Quantity";
            this.txt_Quantity.Size = new System.Drawing.Size(205, 20);
            this.txt_Quantity.TabIndex = 6;
            this.txt_Quantity.TextChanged += new System.EventHandler(this.ControlTextChanged);
            this.txt_Quantity.Enter += new System.EventHandler(this.txtEnter);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cmb_EqpFilter, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_Ender, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.Label_ProductionID, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txt_Starter, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmb_ProductionName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label_Ender, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cmb_Type, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Label_Type, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Label_Starter, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Label_StartTime, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.Label_EndTime, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.Label_EqpFilter, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Label_Quantity, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txt_Quantity, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.Label_EqpName, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmb_EqpName, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label_Method, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmb_Method, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_StartTime, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.txt_EndTime, 3, 4);
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
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cmb_EqpFilter
            // 
            this.cmb_EqpFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_EqpFilter.FormattingEnabled = true;
            this.cmb_EqpFilter.Location = new System.Drawing.Point(91, 3);
            this.cmb_EqpFilter.Name = "cmb_EqpFilter";
            this.cmb_EqpFilter.Size = new System.Drawing.Size(200, 21);
            this.cmb_EqpFilter.TabIndex = 9;
            this.cmb_EqpFilter.TextChanged += new System.EventHandler(this.cmb_EqpFilter_TextChanged);
            // 
            // txt_Ender
            // 
            this.txt_Ender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Ender.Location = new System.Drawing.Point(91, 112);
            this.txt_Ender.Name = "txt_Ender";
            this.txt_Ender.Size = new System.Drawing.Size(200, 20);
            this.txt_Ender.TabIndex = 3;
            this.txt_Ender.TextChanged += new System.EventHandler(this.ControlTextChanged);
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
            // Label_Method
            // 
            this.Label_Method.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label_Method.AutoSize = true;
            this.Label_Method.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_Method.Location = new System.Drawing.Point(297, 7);
            this.Label_Method.Name = "Label_Method";
            this.Label_Method.Size = new System.Drawing.Size(43, 13);
            this.Label_Method.TabIndex = 0;
            this.Label_Method.Text = "Method";
            // 
            // cmb_Method
            // 
            this.cmb_Method.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_Method.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Method.FormattingEnabled = true;
            this.cmb_Method.Location = new System.Drawing.Point(391, 3);
            this.cmb_Method.Name = "cmb_Method";
            this.cmb_Method.Size = new System.Drawing.Size(205, 21);
            this.cmb_Method.TabIndex = 4;
            this.cmb_Method.TextChanged += new System.EventHandler(this.ControlTextChanged);
            // 
            // txt_StartTime
            // 
            this.txt_StartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_StartTime.Location = new System.Drawing.Point(391, 84);
            this.txt_StartTime.Name = "txt_StartTime";
            this.txt_StartTime.Size = new System.Drawing.Size(205, 20);
            this.txt_StartTime.TabIndex = 7;
            this.txt_StartTime.DoubleClick += new System.EventHandler(this.TimePicker);
            this.txt_StartTime.Enter += new System.EventHandler(this.txtEnter);
            this.txt_StartTime.Leave += new System.EventHandler(this.txtDateTimeLeave);
            // 
            // txt_EndTime
            // 
            this.txt_EndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_EndTime.Location = new System.Drawing.Point(391, 112);
            this.txt_EndTime.Name = "txt_EndTime";
            this.txt_EndTime.Size = new System.Drawing.Size(205, 20);
            this.txt_EndTime.TabIndex = 8;
            this.txt_EndTime.DoubleClick += new System.EventHandler(this.TimePicker);
            this.txt_EndTime.Enter += new System.EventHandler(this.txtEnter);
            this.txt_EndTime.Leave += new System.EventHandler(this.txtDateTimeLeave);
            // 
            // btn_Create
            // 
            this.btn_Create.Location = new System.Drawing.Point(110, 164);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(142, 51);
            this.btn_Create.TabIndex = 1;
            this.btn_Create.Text = "Create";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new System.EventHandler(this.btn_Create_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(255, 164);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(142, 51);
            this.btn_Update.TabIndex = 2;
            this.btn_Update.Text = "Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(400, 164);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(142, 51);
            this.btn_Clear.TabIndex = 3;
            this.btn_Clear.Text = "Clear/Load From ID";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // listbox_Events
            // 
            this.listbox_Events.FormattingEnabled = true;
            this.listbox_Events.Location = new System.Drawing.Point(646, 19);
            this.listbox_Events.MultiColumn = true;
            this.listbox_Events.Name = "listbox_Events";
            this.listbox_Events.Size = new System.Drawing.Size(559, 134);
            this.listbox_Events.TabIndex = 4;
            // 
            // btn_AddEvent
            // 
            this.btn_AddEvent.Location = new System.Drawing.Point(722, 164);
            this.btn_AddEvent.Name = "btn_AddEvent";
            this.btn_AddEvent.Size = new System.Drawing.Size(140, 37);
            this.btn_AddEvent.TabIndex = 5;
            this.btn_AddEvent.Text = "Add Event";
            this.btn_AddEvent.UseVisualStyleBackColor = true;
            this.btn_AddEvent.Click += new System.EventHandler(this.btn_AddEvent_Click);
            // 
            // btn_StartRun
            // 
            this.btn_StartRun.Location = new System.Drawing.Point(868, 164);
            this.btn_StartRun.Name = "btn_StartRun";
            this.btn_StartRun.Size = new System.Drawing.Size(140, 37);
            this.btn_StartRun.TabIndex = 5;
            this.btn_StartRun.Text = "Start Run";
            this.btn_StartRun.UseVisualStyleBackColor = true;
            // 
            // btn_EndRun
            // 
            this.btn_EndRun.Location = new System.Drawing.Point(1014, 164);
            this.btn_EndRun.Name = "btn_EndRun";
            this.btn_EndRun.Size = new System.Drawing.Size(140, 37);
            this.btn_EndRun.TabIndex = 5;
            this.btn_EndRun.Text = "End Run";
            this.btn_EndRun.UseVisualStyleBackColor = true;
            // 
            // Production
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1239, 231);
            this.Controls.Add(this.btn_EndRun);
            this.Controls.Add(this.btn_StartRun);
            this.Controls.Add(this.btn_AddEvent);
            this.Controls.Add(this.listbox_Events);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.btn_Create);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Production";
            this.Text = "Production";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label Label_ProductionID;
        private System.Windows.Forms.ComboBox cmb_ProductionName;
        private System.Windows.Forms.Label Label_EqpName;
        private System.Windows.Forms.ComboBox cmb_EqpName;
        private System.Windows.Forms.Label Label_Type;
        private System.Windows.Forms.ComboBox cmb_Type;
        private System.Windows.Forms.Label Label_StartTime;
        private System.Windows.Forms.Label Label_Starter;
        private System.Windows.Forms.TextBox txt_Starter;
        private System.Windows.Forms.Label Label_Quantity;
        private System.Windows.Forms.TextBox txt_Quantity;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmb_EqpFilter;
        private System.Windows.Forms.Label Label_EqpFilter;
        private System.Windows.Forms.TextBox txt_Ender;
        private System.Windows.Forms.Label Label_Ender;
        private System.Windows.Forms.Label Label_EndTime;
        private System.Windows.Forms.Button btn_Create;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Label Label_Method;
        private System.Windows.Forms.ComboBox cmb_Method;
        private System.Windows.Forms.TextBox txt_StartTime;
        private System.Windows.Forms.TextBox txt_EndTime;
        private System.Windows.Forms.ListBox listbox_Events;
        private System.Windows.Forms.Button btn_AddEvent;
        private System.Windows.Forms.Button btn_StartRun;
        private System.Windows.Forms.Button btn_EndRun;
    }
}