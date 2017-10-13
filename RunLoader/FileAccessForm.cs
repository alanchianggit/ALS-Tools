using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Common;
using DAL;
using Entity;
using BusinessLayer;
using System.Collections.Generic;
using System.IO;

namespace RunLoader
{
    public partial class FileAccessForm : Form
    {
        private List<string> CheckedNodes = new List<string>();
        public FileAccessForm()
        {
            InitializeComponent();
        }

        private enum OrderBy
        {
            FileName = 1,
            Size = 2,
            Type = 3,
            DateUploaded = 4
        }

        public IDbConnection conn { get; private set; }

        private const string FileDialogFilter = "Microsoft Access (*.accdb , *.mdb)|*.accdb;*.mdb|SQLite (*.db)|*.db|All Files (*.*)|*.*";
        private const string DATA_FILE_FILTER = "All Files (*.*)|*.*";

        private void btn_BrowseAccess_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.AutoUpgradeEnabled = true;
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dlg.Filter = FileDialogFilter;
            if (txt_FileLocation.Text.Length > 0)
            {
                dlg.FileName = txt_FileLocation.Text;
            }
            if (dlg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            txt_FileLocation.Text = dlg.FileName;
            //PopulateListView();
        }

        private void UpdateStatusConsole(string message)
        {
            this.BeginInvoke
                        (new Action(() =>
                        {
                            //add message to status console
                            this.txt_status.AppendText(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + " - " + message);
                            //add new line to status console
                            this.txt_status.AppendText(Environment.NewLine);
                        }
                        ));

        }


        private void btn_Connect_Click(object sender, EventArgs e)
        {
            //Test connection and store in datafactory
            conn = DataFactory.CreateConnection(this.txt_FileLocation.Text);
            if (conn.State == ConnectionState.Open)
            {
                UpdateStatusConsole(string.Format("Connection opened with {0}", this.txt_FileLocation.Text));
            }

            //conn = DataFactory.CreateConnection(DatabaseType.Oracle,"192.168.1.252:1521/ORCL");
        }


        private void btn_SelectOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != "")
            {
                this.txt_Output.Text = fbd.SelectedPath;
            }
        }
        private List<FilesEntity> ListofFileEntity;
        private void btn_LoadFileTable_Click(object sender, EventArgs e)
        {
            Files obj = new Files();
            DataTable oFilesdt = obj.getFileDataTable();
            ListofFileEntity = obj.getFilesList();
            PopulateTreeView();


        }
        private void PopulateTreeView()
        {
            var topNode = new TreeNode("Select all");
            tv_OutputFiles.Nodes.Add(topNode);
            foreach (FilesEntity obj in ListofFileEntity)
            {
                topNode.Nodes.Add(obj.FileName);
            }

        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            byte[] bindata = ListofFileEntity[1].FileContent;
            File.WriteAllBytes(string.Format(@"{0}\{1}.zip",this.txt_Output.Text,ListofFileEntity[1].FileName), bindata);
        }

        private void btn_SelectInputFiles_Click(object sender, EventArgs e)
        {
            ListFiles(tv_InputFiles, SelectFiles());
        }

        private string[] SelectFiles()
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            opd.Multiselect = true;
            opd.ShowDialog();
            return opd.FileNames;
        }

        private void ListFiles(TreeView treeView, string[] paths)
        {
            if (paths.Length == 0)
            {
                UpdateStatusConsole(string.Format("No Files selected"));
            }
            else
            {
                foreach (string path in paths)
                {
                    //DirectoryInfo rootDirectoryInfo = new DirectoryInfo(path);
                    TreeNode fileNode = new TreeNode(path);
                    treeView.Nodes.Add(fileNode);
                    fileNode.Checked = true;
                    CheckedNodes.Add(fileNode.Name.ToString());
                }
            }
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            foreach (string checkednode in CheckedNodes)
            {
                FileStream fileStream = new System.IO.FileStream(checkednode, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                BinaryReader reader = new System.IO.BinaryReader(fileStream);
                byte[] data = reader.ReadBytes((int)fileStream.Length);

                FilesEntity FE = new FilesEntity();
                FE.FileName = tv_InputFiles.Nodes[checkednode].Name;
                FE.FileContent = data;
                FE.DateUploaded = DateTime.Today;
                FileInfo fi = new FileInfo(tv_InputFiles.Nodes[checkednode].Name);
                FE.FileSize = data.Length;

            }
        }

        private void tv_InputFiles_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            //if (e.Action != TreeViewAction.Unknown)
            //{
            if (e.Node.Nodes.Count > 0)
            {
                /* Calls the CheckAllChildNodes method, passing in the current 
                Checked value of the TreeNode whose checked state changed. */
                //this.CheckAllChildNodes(e.Node, e.Node.Checked);
            }
            else if (e.Node.Nodes.Count == 0)
            {
                if (e.Node.Checked == true)
                {
                    CheckedNodes.Add(e.Node.Text);

                }
                else if (e.Node.Checked == false)
                {
                    CheckedNodes.Remove(e.Node.Text);

                }

            }
        }

        //private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        //{
        //    TreeNode directoryNode = new TreeNode(directoryInfo.FullName);
        //    directoryNode.Checked = true;
        //    foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
        //    {
        //        directoryNode.Nodes.Add(directory.FullName, directory.FullName);
        //        directoryNode.Nodes[directory.FullName].Checked = true;
        //        CheckedNodes.Add(directoryNode.Nodes[directory.FullName].Text.ToString());
        //    }
        //    //recursive sub-directories
        //    //foreach (var directory in directoryInfo.GetDirectories())
        //    //    directoryNode.Nodes.Add(CreateDirectoryNode(directory));
        //    //files 
        //    //foreach (var file in directoryInfo.GetFiles())
        //    //    directoryNode.Nodes.Add(new TreeNode(file.Name));
        //    return directoryNode;
        //}
    }
}
