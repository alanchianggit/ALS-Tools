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
            this.Label_ProductionIDFilter.Location = new System.Drawing.Point(235, 35);
            this.Label_ProductionIDFilter.Name = "Label_ProductionIDFilter";
            this.Label_ProductionIDFilter.Size = new System.Drawing.Size(97, 13);
            this.Label_ProductionIDFilter.TabIndex = 4;
            this.Label_ProductionIDFilter.Text = "Production ID Filter";
            // 
            // txt_ProductionIDFilter
            // 
            this.txt_ProductionIDFilter.Location = new System.Drawing.Point(342, 31);
            this.txt_ProductionIDFilter.Name = "txt_ProductionIDFilter";
            this.txt_ProductionIDFilter.Size = new System.Drawing.Size(121, 20);
            this.txt_ProductionIDFilter.TabIndex = 6;
            this.txt_ProductionIDFilter.TextChanged += new System.EventHandler(this.txt_ProductionID_TextChanged);
            // 
            // dgv_Events
            // 
            this.dgv_Events.AllowUserToDeleteRows = false;
            this.dgv_Events.AllowUserToOrderColumns = true;
            this.dgv_Events.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_Events.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Events.Location = new System.Drawing.Point(12, 57);
            this.dgv_Events.Name = "dgv_Events";
            this.dgv_Events.Size = new System.Drawing.Size(612, 244);
            this.dgv_Events.TabIndex = 7;
            this.dgv_Events.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Events_CellClick);
            this.dgv_Events.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Events_CellEndEdit);
            this.dgv_Events.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_Events_CellValidating);
            // 
            // dgv_AuditTrail
            // 
            this.dgv_AuditTrail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_AuditTrail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_AuditTrail.Location = new System.Drawing.Point(12, 425);
            this.dgv_AuditTrail.Name = "dgv_AuditTrail";
            this.dgv_AuditTrail.Size = new System.Drawing.Size(612, 111);
            this.dgv_AuditTrail.TabIndex = 8;
            // 
            // txt_SearchPhrase
            // 
            this.txt_SearchPhrase.Location = new System.Drawing.Point(524, 31);
            this.txt_SearchPhrase.Name = "txt_SearchPhrase";
            this.txt_SearchPhrase.Size = new System.Drawing.Size(345, 20);
            this.txt_SearchPhrase.TabIndex = 9;
            this.txt_SearchPhrase.TextChanged += new System.EventHandler(this.txt_SearchPhrase_TextChanged);
            // 
            // Label_SearchPhrase
            // 
            this.Label_SearchPhrase.AutoSize = true;
            this.Label_SearchPhrase.Location = new System.Drawing.Point(473, 35);
            this.Label_SearchPhrase.Name = "Label_SearchPhrase";
            this.Label_SearchPhrase.Size = new System.Drawing.Size(41, 13);
            this.Label_SearchPhrase.TabIndex = 10;
            this.Label_SearchPhrase.Text = "Search";
            // 
            // cmb_InstrumentFilter
            // 
            this.cmb_InstrumentFilter.FormattingEnabled = true;
            this.cmb_InstrumentFilter.Location = new System.Drawing.Point(104, 31);
            this.cmb_InstrumentFilter.Name = "cmb_InstrumentFilter";
            this.cmb_InstrumentFilter.Size = new System.Drawing.Size(121, 21);
            this.cmb_InstrumentFilter.TabIndex = 11;
            this.cmb_InstrumentFilter.SelectedIndexChanged += new System.EventHandler(this.cmb_InstrumentFilter_SelectedIndexChanged);
            this.cmb_InstrumentFilter.TextChanged += new System.EventHandler(this.cmb_InstrumentFilter_SelectedIndexChanged);
            // 
            // Label_InstFilter
            // 
            this.Label_InstFilter.AutoSize = true;
            this.Label_InstFilter.Location = new System.Drawing.Point(13, 35);
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
            this.ClientSize = new System.Drawing.Size(1244, 634);
            this.Controls.Add(this.Label_InstFilter);
            this.Controls.Add(this.cmb_InstrumentFilter);
            this.Controls.Add(this.Label_SearchPhrase);
            this.Controls.Add(this.txt_SearchPhrase);
            this.Controls.Add(this.dgv_AuditTrail);
            this.Controls.Add(this.dgv_Events);
            this.Controls.Add(this.txt_ProductionIDFilter);
            this.Controls.Add(this.Label_ProductionIDFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frm_Event";
            this.Text = "Events";
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