using System;
using DAL;

namespace RunLoader
{
    partial class FileAccessForm
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
            InputCheckedNodes.Clear();
            OutputCheckedNodes.Clear();
            ListofFileNames.Clear();
            DataFactory.Instance.Reset();
            GC.Collect();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_ConnectDB = new System.Windows.Forms.Button();
            this.btn_BrowseAccess = new System.Windows.Forms.Button();
            this.btn_SelectOutput = new System.Windows.Forms.Button();
            this.btn_LoadFileTable = new System.Windows.Forms.Button();
            this.txt_FileLocation = new System.Windows.Forms.TextBox();
            this.txt_Output = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_status = new System.Windows.Forms.TextBox();
            this.tv_OutputFiles = new System.Windows.Forms.TreeView();
            this.tv_InputFiles = new System.Windows.Forms.TreeView();
            this.btn_SelectInputFiles = new System.Windows.Forms.Button();
            this.btn_Download = new System.Windows.Forms.Button();
            this.btn_Upload = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ConnectDB
            // 
            this.btn_ConnectDB.Enabled = false;
            this.btn_ConnectDB.Location = new System.Drawing.Point(352, 42);
            this.btn_ConnectDB.Name = "btn_ConnectDB";
            this.btn_ConnectDB.Size = new System.Drawing.Size(75, 20);
            this.btn_ConnectDB.TabIndex = 1;
            this.btn_ConnectDB.Text = "Connect";
            this.btn_ConnectDB.UseVisualStyleBackColor = true;
            this.btn_ConnectDB.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_BrowseAccess
            // 
            this.btn_BrowseAccess.Location = new System.Drawing.Point(312, 42);
            this.btn_BrowseAccess.Name = "btn_BrowseAccess";
            this.btn_BrowseAccess.Size = new System.Drawing.Size(31, 20);
            this.btn_BrowseAccess.TabIndex = 1;
            this.btn_BrowseAccess.Text = "...";
            this.btn_BrowseAccess.UseVisualStyleBackColor = true;
            this.btn_BrowseAccess.Click += new System.EventHandler(this.btn_BrowseAccess_Click);
            // 
            // btn_SelectOutput
            // 
            this.btn_SelectOutput.Location = new System.Drawing.Point(312, 389);
            this.btn_SelectOutput.Name = "btn_SelectOutput";
            this.btn_SelectOutput.Size = new System.Drawing.Size(31, 20);
            this.btn_SelectOutput.TabIndex = 1;
            this.btn_SelectOutput.Text = "...";
            this.btn_SelectOutput.UseVisualStyleBackColor = true;
            this.btn_SelectOutput.Click += new System.EventHandler(this.btn_SelectOutput_Click);
            // 
            // btn_LoadFileTable
            // 
            this.btn_LoadFileTable.Location = new System.Drawing.Point(6, 345);
            this.btn_LoadFileTable.Name = "btn_LoadFileTable";
            this.btn_LoadFileTable.Size = new System.Drawing.Size(106, 23);
            this.btn_LoadFileTable.TabIndex = 1;
            this.btn_LoadFileTable.Text = "Load File Table";
            this.btn_LoadFileTable.UseVisualStyleBackColor = true;
            this.btn_LoadFileTable.Click += new System.EventHandler(this.btn_LoadFileTable_Click);
            // 
            // txt_FileLocation
            // 
            this.txt_FileLocation.Location = new System.Drawing.Point(6, 42);
            this.txt_FileLocation.Name = "txt_FileLocation";
            this.txt_FileLocation.Size = new System.Drawing.Size(300, 20);
            this.txt_FileLocation.TabIndex = 2;
            this.txt_FileLocation.TextChanged += new System.EventHandler(this.txt_FileLocation_TextChanged);
            // 
            // txt_Output
            // 
            this.txt_Output.Enabled = false;
            this.txt_Output.Location = new System.Drawing.Point(6, 389);
            this.txt_Output.Name = "txt_Output";
            this.txt_Output.Size = new System.Drawing.Size(300, 20);
            this.txt_Output.TabIndex = 5;
            this.txt_Output.TextChanged += new System.EventHandler(this.txt_Output_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Database (Access (*.accdb, *.db, *.mdb):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 373);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Download Folder:";
            // 
            // txt_status
            // 
            this.txt_status.Location = new System.Drawing.Point(6, 425);
            this.txt_status.Multiline = true;
            this.txt_status.Name = "txt_status";
            this.txt_status.Size = new System.Drawing.Size(754, 91);
            this.txt_status.TabIndex = 7;
            // 
            // tv_OutputFiles
            // 
            this.tv_OutputFiles.CheckBoxes = true;
            this.tv_OutputFiles.Location = new System.Drawing.Point(6, 68);
            this.tv_OutputFiles.Name = "tv_OutputFiles";
            this.tv_OutputFiles.Size = new System.Drawing.Size(374, 271);
            this.tv_OutputFiles.TabIndex = 8;
            this.tv_OutputFiles.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_OutputFiles_AfterCheck);
            // 
            // tv_InputFiles
            // 
            this.tv_InputFiles.CheckBoxes = true;
            this.tv_InputFiles.Location = new System.Drawing.Point(386, 68);
            this.tv_InputFiles.Name = "tv_InputFiles";
            this.tv_InputFiles.Size = new System.Drawing.Size(374, 271);
            this.tv_InputFiles.TabIndex = 8;
            this.tv_InputFiles.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_InputFiles_AfterCheck);
            // 
            // btn_SelectInputFiles
            // 
            this.btn_SelectInputFiles.Location = new System.Drawing.Point(386, 345);
            this.btn_SelectInputFiles.Name = "btn_SelectInputFiles";
            this.btn_SelectInputFiles.Size = new System.Drawing.Size(110, 23);
            this.btn_SelectInputFiles.TabIndex = 1;
            this.btn_SelectInputFiles.Text = "Select Input Files";
            this.btn_SelectInputFiles.UseVisualStyleBackColor = true;
            this.btn_SelectInputFiles.Click += new System.EventHandler(this.btn_SelectInputFiles_Click);
            // 
            // btn_Download
            // 
            this.btn_Download.Enabled = false;
            this.btn_Download.Location = new System.Drawing.Point(305, 345);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(75, 23);
            this.btn_Download.TabIndex = 9;
            this.btn_Download.Text = "Download Files";
            this.btn_Download.UseVisualStyleBackColor = true;
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // btn_Upload
            // 
            this.btn_Upload.Location = new System.Drawing.Point(685, 345);
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(75, 23);
            this.btn_Upload.TabIndex = 10;
            this.btn_Upload.Text = "Upload";
            this.btn_Upload.UseVisualStyleBackColor = true;
            this.btn_Upload.Click += new System.EventHandler(this.btn_Upload_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_LoadFileTable);
            this.groupBox1.Controls.Add(this.btn_Upload);
            this.groupBox1.Controls.Add(this.tv_OutputFiles);
            this.groupBox1.Controls.Add(this.btn_ConnectDB);
            this.groupBox1.Controls.Add(this.txt_status);
            this.groupBox1.Controls.Add(this.txt_Output);
            this.groupBox1.Controls.Add(this.btn_SelectOutput);
            this.groupBox1.Controls.Add(this.btn_Download);
            this.groupBox1.Controls.Add(this.btn_SelectInputFiles);
            this.groupBox1.Controls.Add(this.txt_FileLocation);
            this.groupBox1.Controls.Add(this.tv_InputFiles);
            this.groupBox1.Controls.Add(this.btn_BrowseAccess);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(772, 533);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Management";
            // 
            // FileAccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 536);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FileAccessForm";
            this.Text = "File Management";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_ConnectDB;
        private System.Windows.Forms.Button btn_BrowseAccess;
        private System.Windows.Forms.Button btn_SelectOutput;
        private System.Windows.Forms.Button btn_LoadFileTable;
        private System.Windows.Forms.TextBox txt_FileLocation;
        private System.Windows.Forms.TextBox txt_Output;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_status;
        private System.Windows.Forms.TreeView tv_OutputFiles;
        private System.Windows.Forms.TreeView tv_InputFiles;
        private System.Windows.Forms.Button btn_SelectInputFiles;
        private System.Windows.Forms.Button btn_Download;
        private System.Windows.Forms.Button btn_Upload;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

