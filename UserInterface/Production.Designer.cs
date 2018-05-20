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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_Production = new System.Windows.Forms.DataGridView();
            this.dgv_AuditTrail = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Production)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AuditTrail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_Production
            // 
            this.dgv_Production.AllowUserToDeleteRows = false;
            this.dgv_Production.AllowUserToOrderColumns = true;
            this.dgv_Production.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_Production.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgv_Production.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_Production.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Production.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Production.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.dgv_Production.Location = new System.Drawing.Point(12, 52);
            this.dgv_Production.Name = "dgv_Production";
            this.dgv_Production.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgv_Production.Size = new System.Drawing.Size(990, 322);
            this.dgv_Production.TabIndex = 0;
            this.dgv_Production.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Production_CellClick);
            this.dgv_Production.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Production_CellDoubleClick);
            this.dgv_Production.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Production_CellEndEdit);
            this.dgv_Production.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_Production_CellValidating);
            this.dgv_Production.EditingControlShowing += base.DataGridViewAutoCompleteText;
            // 
            // dgv_AuditTrail
            // 
            this.dgv_AuditTrail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_AuditTrail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_AuditTrail.Location = new System.Drawing.Point(1008, 52);
            this.dgv_AuditTrail.Name = "dgv_AuditTrail";
            this.dgv_AuditTrail.Size = new System.Drawing.Size(488, 322);
            this.dgv_AuditTrail.TabIndex = 1;
            this.dgv_AuditTrail.ReadOnly = true;
            // 
            // Production
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1508, 386);
            this.Controls.Add(this.dgv_AuditTrail);
            this.Controls.Add(this.dgv_Production);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Production";
            this.Text = "Production";
            this.MouseDown += base.FormMouseDown;
            this.MouseMove +=base.FormMouseMove;
            this.MouseUp += base.FormMouseUp;
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Production)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AuditTrail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgv_Production;
        private System.Windows.Forms.DataGridView dgv_AuditTrail;
    }
}