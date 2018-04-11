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
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Production)).BeginInit();
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
            this.dgv_Production.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Production_CellEndEdit);
            // 
            // Production
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1014, 386);
            this.Controls.Add(this.dgv_Production);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Production";
            this.Text = "Production";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMsDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMsMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmMsUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Production)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgv_Production;
    }
}