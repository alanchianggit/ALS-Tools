namespace ALSTools
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_AuditTrail = new System.Windows.Forms.DataGridView();
            this.dgv_Production = new System.Windows.Forms.DataGridView();
            this.btn_StartRun = new System.Windows.Forms.Button();
            this.btn_EndRun = new System.Windows.Forms.Button();
            this.btn_RedoRun = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AuditTrail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Production)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_AuditTrail
            // 
            this.dgv_AuditTrail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_AuditTrail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_AuditTrail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_AuditTrail.Location = new System.Drawing.Point(1008, 12);
            this.dgv_AuditTrail.Name = "dgv_AuditTrail";
            this.dgv_AuditTrail.ReadOnly = true;
            this.dgv_AuditTrail.Size = new System.Drawing.Size(488, 322);
            this.dgv_AuditTrail.TabIndex = 1;
            // 
            // dgv_Production
            // 
            this.dgv_Production.AllowUserToDeleteRows = false;
            this.dgv_Production.AllowUserToOrderColumns = true;
            this.dgv_Production.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgv_Production.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_Production.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_Production.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_Production.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Production.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Production.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.dgv_Production.Location = new System.Drawing.Point(12, 12);
            this.dgv_Production.Name = "dgv_Production";
            this.dgv_Production.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_Production.Size = new System.Drawing.Size(990, 322);
            this.dgv_Production.TabIndex = 0;
            this.dgv_Production.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Production_CellClick);
            this.dgv_Production.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Production_CellDoubleClick);
            this.dgv_Production.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Production_CellEndEdit);
            this.dgv_Production.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_Production_CellValidating);
            // 
            // btn_StartRun
            // 
            this.btn_StartRun.Location = new System.Drawing.Point(12, 340);
            this.btn_StartRun.Name = "btn_StartRun";
            this.btn_StartRun.Size = new System.Drawing.Size(190, 34);
            this.btn_StartRun.TabIndex = 2;
            this.btn_StartRun.Text = "Start";
            this.btn_StartRun.UseVisualStyleBackColor = true;
            //this.btn_StartRun.Click += new System.EventHandler(this.btn_StartRun_Click);
            this.btn_StartRun.Click += new System.EventHandler(this.buttonActions);
            // 
            // btn_EndRun
            // 
            this.btn_EndRun.Location = new System.Drawing.Point(208, 340);
            this.btn_EndRun.Name = "btn_EndRun";
            this.btn_EndRun.Size = new System.Drawing.Size(190, 34);
            this.btn_EndRun.TabIndex = 2;
            this.btn_EndRun.Text = "End";
            this.btn_EndRun.UseVisualStyleBackColor = true;
            // 
            // btn_RedoRun
            // 
            this.btn_RedoRun.Location = new System.Drawing.Point(404, 340);
            this.btn_RedoRun.Name = "btn_RedoRun";
            this.btn_RedoRun.Size = new System.Drawing.Size(190, 34);
            this.btn_RedoRun.TabIndex = 2;
            this.btn_RedoRun.Text = "Redo";
            this.btn_RedoRun.UseVisualStyleBackColor = true;
            // 
            // Production
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1508, 386);
            this.Controls.Add(this.btn_RedoRun);
            this.Controls.Add(this.btn_EndRun);
            this.Controls.Add(this.btn_StartRun);
            this.Controls.Add(this.dgv_AuditTrail);
            this.Controls.Add(this.dgv_Production);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Production";
            this.Text = "Production";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AuditTrail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Production)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgv_Production;
        private System.Windows.Forms.DataGridView dgv_AuditTrail;
        private System.Windows.Forms.Button btn_StartRun;
        private System.Windows.Forms.Button btn_EndRun;
        private System.Windows.Forms.Button btn_RedoRun;
    }
}