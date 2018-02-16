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
            this.cmb_ID = new System.Windows.Forms.ComboBox();
            this.Label_ProductionID = new System.Windows.Forms.Label();
            this.txt_ProductionID = new System.Windows.Forms.TextBox();
            this.dgv_Events = new System.Windows.Forms.DataGridView();
            this.dgv_AuditTrail = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Events)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AuditTrail)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Add.Location = new System.Drawing.Point(217, 533);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(111, 46);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Text = "Add/Submit";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // txt_Details
            // 
            this.txt_Details.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.txt_Details.Location = new System.Drawing.Point(12, 360);
            this.txt_Details.Multiline = true;
            this.txt_Details.Name = "txt_Details";
            this.txt_Details.Size = new System.Drawing.Size(903, 39);
            this.txt_Details.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 323);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Log";
            // 
            // cmb_Log
            // 
            this.cmb_Log.FormattingEnabled = true;
            this.cmb_Log.Location = new System.Drawing.Point(40, 320);
            this.cmb_Log.Name = "cmb_Log";
            this.cmb_Log.Size = new System.Drawing.Size(121, 21);
            this.cmb_Log.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 296);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Time";
            // 
            // txt_Time
            // 
            this.txt_Time.Location = new System.Drawing.Point(40, 294);
            this.txt_Time.Name = "txt_Time";
            this.txt_Time.Size = new System.Drawing.Size(121, 20);
            this.txt_Time.TabIndex = 6;
            // 
            // Label_EventID
            // 
            this.Label_EventID.AutoSize = true;
            this.Label_EventID.Location = new System.Drawing.Point(175, 323);
            this.Label_EventID.Name = "Label_EventID";
            this.Label_EventID.Size = new System.Drawing.Size(49, 13);
            this.Label_EventID.TabIndex = 4;
            this.Label_EventID.Text = "Event ID";
            // 
            // cmb_ID
            // 
            this.cmb_ID.FormattingEnabled = true;
            this.cmb_ID.Location = new System.Drawing.Point(253, 319);
            this.cmb_ID.Name = "cmb_ID";
            this.cmb_ID.Size = new System.Drawing.Size(121, 21);
            this.cmb_ID.TabIndex = 5;
            // 
            // Label_ProductionID
            // 
            this.Label_ProductionID.AutoSize = true;
            this.Label_ProductionID.Location = new System.Drawing.Point(175, 296);
            this.Label_ProductionID.Name = "Label_ProductionID";
            this.Label_ProductionID.Size = new System.Drawing.Size(72, 13);
            this.Label_ProductionID.TabIndex = 4;
            this.Label_ProductionID.Text = "Production ID";
            // 
            // txt_ProductionID
            // 
            this.txt_ProductionID.Location = new System.Drawing.Point(253, 293);
            this.txt_ProductionID.Name = "txt_ProductionID";
            this.txt_ProductionID.Size = new System.Drawing.Size(121, 20);
            this.txt_ProductionID.TabIndex = 6;
            this.txt_ProductionID.TextChanged += new System.EventHandler(this.txt_ProductionID_TextChanged);
            // 
            // dgv_Events
            // 
            this.dgv_Events.AllowUserToAddRows = false;
            this.dgv_Events.AllowUserToDeleteRows = false;
            this.dgv_Events.AllowUserToOrderColumns = true;
            this.dgv_Events.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Events.Location = new System.Drawing.Point(12, 22);
            this.dgv_Events.Name = "dgv_Events";
            this.dgv_Events.ReadOnly = true;
            this.dgv_Events.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Events.Size = new System.Drawing.Size(903, 110);
            this.dgv_Events.TabIndex = 7;
            this.dgv_Events.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DisplayEvent);
            this.dgv_Events.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            // 
            // dgv_AuditTrail
            // 
            this.dgv_AuditTrail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_AuditTrail.Location = new System.Drawing.Point(12, 405);
            this.dgv_AuditTrail.Name = "dgv_AuditTrail";
            this.dgv_AuditTrail.Size = new System.Drawing.Size(903, 111);
            this.dgv_AuditTrail.TabIndex = 8;
            this.dgv_AuditTrail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            // 
            // frm_Event
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1244, 634);
            this.Controls.Add(this.dgv_AuditTrail);
            this.Controls.Add(this.dgv_Events);
            this.Controls.Add(this.txt_ProductionID);
            this.Controls.Add(this.txt_Time);
            this.Controls.Add(this.Label_ProductionID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_ID);
            this.Controls.Add(this.Label_EventID);
            this.Controls.Add(this.cmb_Log);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Details);
            this.Controls.Add(this.btn_Add);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_Event";
            this.Text = "Events";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Events)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AuditTrail)).EndInit();
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
        private System.Windows.Forms.ComboBox cmb_ID;
        private System.Windows.Forms.Label Label_ProductionID;
        private System.Windows.Forms.TextBox txt_ProductionID;
        private System.Windows.Forms.DataGridView dgv_Events;
        private System.Windows.Forms.DataGridView dgv_AuditTrail;
    }
}