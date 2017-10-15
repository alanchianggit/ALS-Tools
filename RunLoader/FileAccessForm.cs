using System;
using System.Data;
using System.Windows.Forms;
using DAL;
using Entity;
using BusinessLayer;
using System.Collections.Generic;
using System.IO;

namespace RunLoader
{
    public partial class FileAccessForm : Form
    {
        private List<string> OutputCheckedNodes = new List<string>();
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
        private Dictionary<string, FilesEntity> ListofFileEntity = new Dictionary<string, FilesEntity>();

        private const string FileDialogFilter = "Microsoft Access (*.accdb , *.mdb)|*.accdb;*.mdb|SQLite (*.db)|*.db|All Files (*.*)|*.*";
        private const string DATA_FILE_FILTER = "All Files (*.*)|*.*";

        private void btn_BrowseAccess_Click(object sender, EventArgs e)
        {
            //Instantiate dialog
            OpenFileDialog dlg = new OpenFileDialog();
            //Enable upgrade for new Vista framework
            dlg.AutoUpgradeEnabled = true;
            //Initial directory is personal documents
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //Filter using database extensions
            dlg.Filter = FileDialogFilter;
            //dlg.ShowDialog();
            //If textbox is empty, then fil
            if (dlg.ShowDialog() == DialogResult.OK && txt_FileLocation.Text.Length == 0)
            {
                txt_FileLocation.Text = dlg.FileName;
            }
            else 
            {
                return;
            }
            
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
            try
            {

            
            //Test connection and store in datafactory
            conn = DataFactory.CreateConnection(this.txt_FileLocation.Text);
            if (conn.State == ConnectionState.Open)
            {
                UpdateStatusConsole(string.Format("Connection opened with {0}", this.txt_FileLocation.Text));
            }

            }
            catch (Exception ex)
            {
                UpdateStatusConsole(string.Format("Connection Failed due to error '{0}'",ex.Message));
            }
            //conn = DataFactory.CreateConnection(DatabaseType.Oracle,"192.168.1.252:1521/ORCL");
        }


        private void btn_SelectOutput_Click(object sender, EventArgs e)
        {
            //Instantiate folder browser
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            //Show dialog
            fbd.ShowDialog();
            //If selectedpath is not 0
            if (fbd.SelectedPath.Length > 0 )
            {
                this.txt_Output.Text = fbd.SelectedPath;
            }
        }
        
        private void btn_LoadFileTable_Click(object sender, EventArgs e)
        {
            if (conn != null && DataFactory.ActiveConn != null && DataFactory.ActiveConn.State == ConnectionState.Open)
            {
              
            }
            else
            {
                UpdateStatusConsole("Connection is not open.");
                UpdateStatusConsole("Attempting to re-open Connection now.");
                btn_ConnectDB.PerformClick();
            }

            try
            {
                //Clear list of working File Entitiy
                ListofFileEntity.Clear();
                //Create new Files Entity through mid-tier
                Files obj = new Files();
                //get datatable from DB through middle tier
                DataTable oFilesdt = obj.getFileDataTable();
                //Get new list through DB
                ListofFileEntity = obj.getFilesList();
                PopulateTreeView();
            }
            catch (Exception ex)
            {
                UpdateStatusConsole(string.Format("Load table failed due to {0}.", ex.Message));
            }
            

        }
        private void ListFiles(TreeView treeView, object[] objectIDs)
        {
            if (objectIDs.Length == 0)
            {
                UpdateStatusConsole(string.Format("No Files selected"));
            }
            else
            {
                foreach (string id in objectIDs)
                {
                    TreeNode fileNode = new TreeNode(id);

                    fileNode.Name = id;
                    treeView.Nodes.Add(fileNode);
                    fileNode.Checked = true;
                }
            }
        }

        private void PopulateTreeView()
        {
            if (tv_OutputFiles.Nodes.ContainsKey("Select All") != true)
            {
                
                tv_OutputFiles.Nodes.Add(new TreeNode("Select All") { Name = "Select All" });
                TreeNode topNode = tv_OutputFiles.Nodes["Select All"];
                foreach (FilesEntity obj in ListofFileEntity.Values)
                {
                    topNode.Nodes.Add(obj.FileName);
                }
            }
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {

            try
            {
                foreach (TreeNode node in tv_OutputFiles.Nodes["Select All"].Nodes)
                {
                    if (node.Checked == true)
                    {
                        Files fs = new Files();
                        
                        //Need to find list of checked output files
                        byte[] bindata = fs[node.Text].FileContent;
                        //byte[] bindata = ListofFileEntity[node.Name].FileContent;
                        File.WriteAllBytes(string.Format(@"{0}\{1}", this.txt_Output.Text, fs[node.Text].FileName), bindata);
                    }

                }
            }
            catch (Exception ex)
            {
                UpdateStatusConsole(ex.Message);
            }
            
        }

        private void btn_SelectInputFiles_Click(object sender, EventArgs e)
        {
            ListFiles(tv_InputFiles, SelectFiles());
        }

        private string[] SelectFiles()
        {
            //Instantiate file dialog
            OpenFileDialog opd = new OpenFileDialog();
            //
            opd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            //Allow multiple files
            opd.Multiselect = true;
            opd.ShowDialog();
            return opd.FileNames;
        }



        private void btn_Upload_Click(object sender, EventArgs e)
        {
            foreach (string checkednode in OutputCheckedNodes)
            {
                FileStream fileStream = new System.IO.FileStream(checkednode, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                BinaryReader reader = new System.IO.BinaryReader(fileStream);
                byte[] data = reader.ReadBytes((int)fileStream.Length);

                Files FE = new Files();
                FileInfo fi = new FileInfo(tv_InputFiles.Nodes[checkednode].Text);

                FE.FileName = fi.Name;
                FE.FileContent = data;
                //FE.DateUploaded = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") ;
                FE.DateUploaded = GetDateWithoutMilliseconds(DateTime.Now) ;

                FE.Type = fi.Extension;
                FE.Size = data.Length;

                FE.Add();
            }
        }

        private DateTime GetDateWithoutMilliseconds(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);
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
                    OutputCheckedNodes.Add(e.Node.Text);

                }
                else if (e.Node.Checked == false)
                {
                    OutputCheckedNodes.Remove(e.Node.Text);

                }

            }
        }

        private void txt_FileLocation_TextChanged(object sender, EventArgs e)
        {
            if (this.txt_FileLocation.Text.Length > 0)
            {
                this.btn_ConnectDB.Enabled = true;
            }
            else
            {
                this.btn_ConnectDB.Enabled = true;
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
