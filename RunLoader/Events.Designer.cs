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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Label_ProductionIDFilter = new System.Windows.Forms.Label();
            this.txt_ProductionIDFilter = new System.Windows.Forms.TextBox();
            this.dgv_Events = new System.Windows.Forms.DataGridView();
            this.dgv_AuditTrail = new System.Windows.Forms.DataGridView();
            this.txt_SearchPhrase = new System.Windows.Forms.TextBox();
            this.Label_SearchPhrase = new System.Windows.Forms.Label();
            this.cmb_InstrumentFilter = new System.Windows.Forms.ComboBox();
            this.Label_InstFilter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Events)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AuditTrail)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_ProductionIDFilter
            // 
            this.Label_ProductionIDFilter.AutoSize = true;
            this.Label_ProductionIDFilter.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label_ProductionIDFilter.Location = new System.Drawing.Point(234, 9);
            this.Label_ProductionIDFilter.Name = "Label_ProductionIDFilter";
            this.Label_ProductionIDFilter.Size = new System.Drawing.Size(97, 13);
            this.Label_ProductionIDFilter.TabIndex = 4;
            this.Label_ProductionIDFilter.Text = "Production ID Filter";
            // 
            // txt_ProductionIDFilter
            // 
            this.txt_ProductionIDFilter.Location = new System.Drawing.Point(341, 5);
            this.txt_ProductionIDFilter.Name = "txt_ProductionIDFilter";
            this.txt_ProductionIDFilter.Size = new System.Drawing.Size(121, 20);
            this.txt_ProductionIDFilter.TabIndex = 6;
            this.txt_ProductionIDFilter.TextChanged += new System.EventHandler(this.txt_ProductionID_TextChanged);
            // 
            // dgv_Events
            // 
            this.dgv_Events.AllowUserToDeleteRows = false;
            this.dgv_Events.AllowUserToOrderColumns = true;
            this.dgv_Events.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Events.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_Events.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_Events.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_Events.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Events.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Events.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.dgv_Events.Location = new System.Drawing.Point(12, 63);
            this.dgv_Events.Name = "dgv_Events";
            this.dgv_Events.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_Events.Size = new System.Drawing.Size(775, 244);
            this.dgv_Events.TabIndex = 7;
            this.dgv_Events.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Events_CellClick);
            this.dgv_Events.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Events_CellEndEdit);
            this.dgv_Events.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_Events_CellValidating);
            this.dgv_Events.SelectionChanged += new System.EventHandler(this.dgv_Events_SelectionChanged);
            // 
            // dgv_AuditTrail
            // 
            this.dgv_AuditTrail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_AuditTrail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_AuditTrail.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_AuditTrail.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_AuditTrail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_AuditTrail.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_AuditTrail.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_AuditTrail.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.dgv_AuditTrail.Location = new System.Drawing.Point(12, 313);
            this.dgv_AuditTrail.Name = "dgv_AuditTrail";
            this.dgv_AuditTrail.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_AuditTrail.Size = new System.Drawing.Size(775, 236);
            this.dgv_AuditTrail.TabIndex = 8;
            // 
            // txt_SearchPhrase
            // 
            this.txt_SearchPhrase.Location = new System.Drawing.Point(63, 37);
            this.txt_SearchPhrase.Name = "txt_SearchPhrase";
            this.txt_SearchPhrase.Size = new System.Drawing.Size(724, 20);
            this.txt_SearchPhrase.TabIndex = 9;
            this.txt_SearchPhrase.TextChanged += new System.EventHandler(this.txt_SearchPhrase_TextChanged);
            // 
            // Label_SearchPhrase
            // 
            this.Label_SearchPhrase.AutoSize = true;
            this.Label_SearchPhrase.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label_SearchPhrase.Location = new System.Drawing.Point(12, 41);
            this.Label_SearchPhrase.Name = "Label_SearchPhrase";
            this.Label_SearchPhrase.Size = new System.Drawing.Size(41, 13);
            this.Label_SearchPhrase.TabIndex = 10;
            this.Label_SearchPhrase.Text = "Search";
            // 
            // cmb_InstrumentFilter
            // 
            this.cmb_InstrumentFilter.FormattingEnabled = true;
            this.cmb_InstrumentFilter.Location = new System.Drawing.Point(103, 5);
            this.cmb_InstrumentFilter.Name = "cmb_InstrumentFilter";
            this.cmb_InstrumentFilter.Size = new System.Drawing.Size(121, 21);
            this.cmb_InstrumentFilter.TabIndex = 11;
            this.cmb_InstrumentFilter.SelectedIndexChanged += new System.EventHandler(this.cmb_InstrumentFilter_SelectedIndexChanged);
            this.cmb_InstrumentFilter.TextChanged += new System.EventHandler(this.cmb_InstrumentFilter_SelectedIndexChanged);
            // 
            // Label_InstFilter
            // 
            this.Label_InstFilter.AutoSize = true;
            this.Label_InstFilter.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Label_InstFilter.Location = new System.Drawing.Point(12, 9);
            this.Label_InstFilter.Name = "Label_InstFilter";
            this.Label_InstFilter.Size = new System.Drawing.Size(81, 13);
            this.Label_InstFilter.TabIndex = 12;
            this.Label_InstFilter.Text = "Instrument Filter";
            // 
            // frm_Event
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(793, 561);
            this.Controls.Add(this.Label_InstFilter);
            this.Controls.Add(this.cmb_InstrumentFilter);
            this.Controls.Add(this.Label_SearchPhrase);
            this.Controls.Add(this.txt_SearchPhrase);
            this.Controls.Add(this.dgv_AuditTrail);
            this.Controls.Add(this.dgv_Events);
            this.Controls.Add(this.txt_ProductionIDFilter);
            this.Controls.Add(this.Label_ProductionIDFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_Event";
            this.Text = "Events";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMsDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMsMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmMsUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Events)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AuditTrail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Label_ProductionIDFilter;
        private System.Windows.Forms.TextBox txt_ProductionIDFilter;
        private System.Windows.Forms.DataGridView dgv_Events;
        private System.Windows.Forms.DataGridView dgv_AuditTrail;
        private System.Windows.Forms.TextBox txt_SearchPhrase;
        private System.Windows.Forms.Label Label_SearchPhrase;
        private System.Windows.Forms.ComboBox cmb_InstrumentFilter;
        private System.Windows.Forms.Label Label_InstFilter;
    }
}